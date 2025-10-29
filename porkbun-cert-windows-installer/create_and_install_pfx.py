import argparse
import ctypes
import os
import subprocess
import sys
from typing import List, Optional

import requests

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


def read_pem_certificates(data: bytes) -> List[x509.Certificate]:
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


def read_private_key(data: bytes, password: Optional[bytes]):
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


def build_pfx(name: str, private_key, cert: x509.Certificate, cas: List[x509.Certificate], out_path: str):
    enc = serialization.NoEncryption()
    pfx_bytes = pkcs12.serialize_key_and_certificates(name.encode('utf-8'), private_key, cert, cas if cas else None, enc)
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


def download_bundle(porkbun_api_key, porkbun_secret_api_key):
    request_body = {"apikey": porkbun_api_key, "secretapikey": porkbun_secret_api_key}
    r = requests.post('https://api.porkbun.com/api/json/v3/ssl/retrieve/yaskov.dev', json=request_body)
    print(f"Porkbun response status code: {r.status_code}")
    response_body = r.json()
    return bytes(response_body['privatekey'], 'utf-8'), bytes(response_body['certificatechain'], 'utf-8')


def main():
    parser = argparse.ArgumentParser(description="Create PFX from PEM components downloaded from Porkbun and import into Windows LocalMachine store.")
    parser.add_argument("--porkbun-api-key", required=True, help="Porkbun API key.")
    parser.add_argument("--porkbun-api-secret-key", required=True, help="Porkbun API secret key.")
    parser.add_argument("--import-pfx", default=False, help="Import PFX into Windows LocalMachine store.")
    args = parser.parse_args()
    private_key, certificate_chain = download_bundle(args.porkbun_api_key, args.porkbun_api_secret_key)

    if args.import_pfx and not is_admin():
        print("To import PFX, administrator privileges are required. Re-run in elevated shell.")
        sys.exit(1)

    private_key = read_private_key(private_key, password=None)

    all_certs: List[x509.Certificate] = read_pem_certificates(certificate_chain)

    if not all_certs:
        print("No certificates parsed. Check PEM files.")
        sys.exit(1)

    end_entity, chain = classify_certs(all_certs, private_key)

    cert_out_path = "domain.cert.pem"
    with open(cert_out_path, "wb") as binary_file:
        binary_file.write(certificate_chain)
    print(f"Cert written to {cert_out_path}. Upload it to the bot Enterprise App to be able to use the TLS cert as an application cert as well")

    out_path = 'certificate.pfx'
    build_pfx('ServerCert', private_key, end_entity, chain, out_path)
    print(f"PFX written to {out_path}. {'Importing it to the LocalMachine store' if args.import_pfx else 'Import it to the LocalMachine store manually'}")

    if args.import_pfx:
        import_pfx(os.path.abspath(out_path), "", args.store, use_certutil=not args.powershell)

    # TODO: automatically upload the domain.cert.pem to the Enterprise App.

if __name__ == "__main__":
    main()
