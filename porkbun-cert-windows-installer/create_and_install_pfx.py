import argparse
import os
import sys
import ctypes
import getpass
import subprocess
import secrets
from typing import List, Optional

try:
    from cryptography import x509
    from cryptography.hazmat.primitives import serialization
    from cryptography.hazmat.primitives.serialization import pkcs12
except ImportError:
    print("cryptography package not found. Install with: pip install cryptography")
    sys.exit(1)


def is_admin() -> bool:
    try:
        return ctypes.windll.shell32.IsUserAnAdmin() != 0  # type: ignore[attr-defined]
    except Exception:
        return False


def elevate():
    # Relaunch the script with admin rights
    params = " ".join([f'"{arg}"' if ' ' in arg else arg for arg in sys.argv[1:]])
    ret = ctypes.windll.shell32.ShellExecuteW(None, "runas", sys.executable, f'"{sys.argv[0]}" {params}', None, 1)
    if ret <= 32:
        print("Elevation failed (ShellExecuteW returned status <=32). Run this script from an elevated command prompt.")
    sys.exit(0)


def read_pem_certificates(path: str) -> List[x509.Certificate]:
    data = open(path, 'rb').read()
    certs: List[x509.Certificate] = []
    # Split by BEGIN CERTIFICATE boundaries
    parts = data.split(b"-----BEGIN CERTIFICATE-----")
    for part in parts:
        if b"-----END CERTIFICATE-----" in part:
            pem = b"-----BEGIN CERTIFICATE-----" + part.split(b"-----END CERTIFICATE-----")[0] + b"-----END CERTIFICATE-----\n"
            try:
                certs.append(x509.load_pem_x509_certificate(pem))
            except Exception:
                pass
    return certs


def read_private_key(path: str, password: Optional[bytes]):
    data = open(path, 'rb').read()
    return serialization.load_pem_private_key(data, password=password)


def classify_certs(all_certs: List[x509.Certificate], private_key) -> (x509.Certificate, List[x509.Certificate]):
    # End-entity cert should match private key public numbers
    pk_public = private_key.public_key().public_bytes(
        serialization.Encoding.DER,
        serialization.PublicFormat.SubjectPublicKeyInfo,
    )
    end_entity: Optional[x509.Certificate] = None
    chain: List[x509.Certificate] = []
    for c in all_certs:
        if c.public_key().public_bytes(
            serialization.Encoding.DER,
            serialization.PublicFormat.SubjectPublicKeyInfo,
        ) == pk_public:
            end_entity = c
        else:
            chain.append(c)
    if end_entity is None:
        raise ValueError("Could not find a certificate matching the private key. Ensure the correct cert/key files are provided.")
    # Sort chain (simple heuristic: issuer of end_entity first if present)
    ordered_chain: List[x509.Certificate] = []
    current = end_entity
    remaining = chain.copy()
    while remaining:
        next_cert = None
        for c in remaining:
            if current.issuer == c.subject:
                next_cert = c
                break
        if next_cert:
            ordered_chain.append(next_cert)
            remaining.remove(next_cert)
            current = next_cert
        else:
            # Append the rest arbitrarily
            ordered_chain.extend(remaining)
            break
    return end_entity, ordered_chain


def build_pfx(name: str, private_key, cert: x509.Certificate, cas: List[x509.Certificate], password: Optional[str], out_path: str):
    # Choose encryption algorithm: empty password => NoEncryption (creates unprotected PFX)
    if password:
        enc = serialization.BestAvailableEncryption(password.encode('utf-8'))
    else:
        enc = serialization.NoEncryption()
    pfx_bytes = pkcs12.serialize_key_and_certificates(
        name.encode('utf-8'),
        private_key,
        cert,
        cas if cas else None,
        enc,
    )
    with open(out_path, 'wb') as f:
        f.write(pfx_bytes)


def import_pfx(out_path: str, password: str, store: str, use_certutil: bool = True):
    if use_certutil:
        cmd = ["certutil", "-f", "-p", password, "-importpfx", out_path]
    else:
        # PowerShell fallback
        ps_script = (
            f"$pwd = ConvertTo-SecureString '{password}' -AsPlainText -Force; "
            f"Import-PfxCertificate -FilePath '{out_path}' -Password $pwd -CertStoreLocation {store}"
        )
        cmd = ["powershell", "-NoProfile", "-ExecutionPolicy", "Bypass", "-Command", ps_script]
    proc = subprocess.run(cmd, capture_output=True, text=True)
    if proc.returncode != 0:
        print("Certificate import failed. Output:")
        print(proc.stdout)
        print(proc.stderr)
        raise SystemExit(proc.returncode)
    print(proc.stdout)
    print("Import succeeded.")


def main():
    parser = argparse.ArgumentParser(description="Create PFX from PEM components and install into Windows LocalMachine store.")
    parser.add_argument("--cert", required=True, help="Path to domain.cert.pem (may contain leaf & chain).")
    parser.add_argument("--key", required=True, help="Path to private.key.pem.")
    parser.add_argument("--extra", nargs="*", help="Optional extra PEM files (e.g., public.key.pem or intermediates).")
    parser.add_argument("--out", default="certificate.pfx", help="Output PFX filename.")
    parser.add_argument("--password", help="Password to protect PFX (omit for empty / no password).")
    parser.add_argument("--store", default="Cert:\\LocalMachine\\My", help="PowerShell store path (if PowerShell import used).")
    parser.add_argument("--friendly-name", default="ServerCert", help="Friendly name for PFX.")
    parser.add_argument("--no-elevate", action="store_true", help="Do not auto-elevate; fail if not admin.")
    parser.add_argument("--powershell", action="store_true", help="Use PowerShell Import-PfxCertificate instead of certutil.")
    args = parser.parse_args()

    if not is_admin():
        if args.no_elevate:
            print("Administrator privileges required. Re-run in elevated shell.")
            sys.exit(1)
        print("Attempting to elevate...")
        elevate()

    for path in [args.cert, args.key, *(args.extra or [])]:
        if not os.path.isfile(path):
            print(f"File not found: {path}")
            sys.exit(1)

    password = args.password if args.password is not None else ""
    # No auto-generation; empty string if not supplied

    # Optional private key password prompt if encrypted
    key_password: Optional[bytes] = None
    try:
        # Try loading without password first
        private_key = read_private_key(args.key, None)
    except TypeError:
        pw = getpass.getpass("Enter password for encrypted private key: ")
        key_password = pw.encode('utf-8')
        private_key = read_private_key(args.key, key_password)

    all_certs: List[x509.Certificate] = []
    for pem_path in [args.cert, *(args.extra or [])]:
        all_certs.extend(read_pem_certificates(pem_path))

    if not all_certs:
        print("No certificates parsed. Check PEM files.")
        sys.exit(1)

    end_entity, chain = classify_certs(all_certs, private_key)

    build_pfx(args.friendly_name, private_key, end_entity, chain, password, args.out)
    print(f"PFX written to {args.out}")

    import_pfx(os.path.abspath(args.out), password, args.store, use_certutil=not args.powershell)

    # TODO: instruct the user to upload domain.cert.pem (the value of the --cert param) to the bot enterprise app
    # to be able to use the TLS certificate as an application certificate as well.


if __name__ == "__main__":
    main()
