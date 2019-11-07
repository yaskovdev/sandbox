# Company A, aka Sender

## How To Create Public And Private Keys

`openssl req -x509 -newkey rsa:2048 -sha256 -keyout a_private.pem -out a_public.pem -days 365000`
1. Enter PEM pass phrase: secret
2. Verifying - Enter PEM pass phrase: secret
3. Country Name (2 letter code) [AU]:EE
4. State or Province Name (full name) [Some-State]:Harjumaa
5. Locality Name (eg, city) []:Tallinn
6. Organization Name (eg, company) [Internet Widgits Pty Ltd]:A
7. Organizational Unit Name (eg, section) []:
8. Common Name (e.g. server FQDN or YOUR name) []:
9. Email Address []:

`openssl req -x509 -newkey rsa:2048 -sha256 -keyout b_private.pem -out b_public.pem -days 365000`
1. Enter PEM pass phrase: secret
2. Verifying - Enter PEM pass phrase: secret
3. Country Name (2 letter code) [AU]:EE
4. State or Province Name (full name) [Some-State]:Harjumaa
5. Locality Name (eg, city) []:Tallinn
6. Organization Name (eg, company) [Internet Widgits Pty Ltd]:B
7. Organizational Unit Name (eg, section) []:
8. Common Name (e.g. server FQDN or YOUR name) []:
9. Email Address []:

## How To Create The Keystore

I created the `keystore.p12` file in this way:

1. Create a keystore with private and public keys for A using `openssl pkcs12 -export -out keystore.p12 -in a_public.pem -inkey a_private.pem -passin pass:secret -passout pass:secret -name "A"`
2. Add public key for B to the keystore using `keytool -importcert -file b_public.pem -keystore keystore.p12 -alias "B"`
