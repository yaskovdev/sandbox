## Prerequisites

1. A Porkbun account with a TLS-enabled domain.
2. Python
3. Windows, if you are planning to import the PFX to the LocalMachine store.

## Usage

```shell
python ./create_and_install_pfx.py --porkbun-api-key API_KEY --porkbun-api-secret-key API_SECRET_KEY 
```

Take `API_KEY` and `API_SECRET_KEY` from https://porkbun.com/account/api.

Remember to enable API access for your domain.
