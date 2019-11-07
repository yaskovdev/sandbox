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


1. Create a keystore with private and public keys for B using `openssl pkcs12 -export -out keystore.p12 -in b_public.pem -inkey b_private.pem -passin pass:secret -passout pass:secret -name "B"`
2. Add public key for A to the keystore using `keytool -importcert -file a_public.pem -keystore keystore.p12 -alias "A"`
