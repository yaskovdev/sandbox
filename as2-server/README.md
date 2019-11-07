# Company B, aka Receiver

## How To Create Public And Private Keys

See [here](https://github.com/yaskovdev/sandbox/blob/master/as2-client/README.md).

## How To Create The Keystore

I created the `keystore.p12` file in this way:

1. Create a keystore with private and public keys for B using `openssl pkcs12 -export -out keystore.p12 -in b_public.pem -inkey b_private.pem -passin pass:secret -passout pass:secret -name "B"`
2. Add public key for A to the keystore using `keytool -importcert -file a_public.pem -keystore keystore.p12 -alias "A"`
