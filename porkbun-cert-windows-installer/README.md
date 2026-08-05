## Prerequisites

1. A Porkbun account with a TLS-enabled domain.
2. Windows, if you are planning to import the PFX to the LocalMachine store.
3. Python 3 (CPython) for Windows. Install it from [python.org](https://www.python.org/downloads/windows/) and select **Add Python to PATH**, or use:
   ```powershell
   winget install --id Python.Python.3.13 --exact
   python --version
   ```

## Usage

```powershell
python -m venv .venv
.\.venv\Scripts\python.exe -m pip install requests cryptography
.\.venv\Scripts\python.exe .\create_and_install_pfx.py --porkbun-api-key API_KEY --porkbun-api-secret-key API_SECRET_KEY
```

Take `API_KEY` and `API_SECRET_KEY` from https://porkbun.com/account/api.

Remember to enable API access for your domain.

The script will create the certificate.pfx in the current folder, then it can be installed into the LocalMachine store.
