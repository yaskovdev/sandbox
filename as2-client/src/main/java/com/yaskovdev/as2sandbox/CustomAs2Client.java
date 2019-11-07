package com.yaskovdev.as2sandbox;

import static com.helger.as2lib.crypto.ECompressionType.ZLIB;
import static com.helger.as2lib.crypto.ECryptoAlgorithmCrypt.CRYPT_3DES;
import static com.helger.as2lib.crypto.ECryptoAlgorithmSign.DIGEST_SHA_256;
import static com.helger.as2lib.disposition.DispositionOptions.IMPORTANCE_OPTIONAL;
import static com.helger.as2lib.disposition.DispositionOptions.PROTOCOL_PKCS7_SIGNATURE;
import static com.helger.commons.mime.CMimeType.TEXT_PLAIN;
import static com.helger.security.keystore.EKeyStoreType.PKCS12;

import java.io.File;

import javax.activation.DataHandler;
import javax.mail.util.ByteArrayDataSource;

import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;

import org.springframework.stereotype.Component;

import com.helger.as2lib.client.AS2Client;
import com.helger.as2lib.client.AS2ClientRequest;
import com.helger.as2lib.client.AS2ClientResponse;
import com.helger.as2lib.client.AS2ClientSettings;
import com.helger.as2lib.disposition.DispositionOptions;

import edu.umd.cs.findbugs.annotations.SuppressFBWarnings;

@Component
@RequiredArgsConstructor
@SuppressFBWarnings
public class CustomAs2Client {

	private static final String SUBJECT = "AS2 Message";
	private static final String KEYSTORE_PASSWORD = "secret";
	private static final String SENDER_ID = "A";
	private static final String SENDER_EMAIL = "info@a.com";
	private static final String RECEIVER_ID = "B";
	private static final String RECEIVER_URL = "http://localhost:8082/as2";

	private static final boolean COMPRESS_BEFORE_SIGNING = false;

	/**
	 * Should contain public and private keys for sender and public key for receiver.
	 */
	private static final String KEYSTORE_FILE_NAME = "/keystore.p12";

	AS2ClientResponse sendSynchronous() {
		return new AS2Client().sendSynchronous(settings(), request());
	}

	private AS2ClientSettings settings() {
		return new AS2ClientSettings()
				.setKeyStore(PKCS12, new File(KEYSTORE_FILE_NAME), KEYSTORE_PASSWORD)
				.setSenderData(SENDER_ID, SENDER_EMAIL, SENDER_ID)
				.setEncryptAndSign(CRYPT_3DES, DIGEST_SHA_256)
				.setCompress(ZLIB, COMPRESS_BEFORE_SIGNING)
				.setMDNOptions(mdnOptions())
				.setReceiverData(RECEIVER_ID, RECEIVER_ID, RECEIVER_URL)
				.setPartnershipName(SENDER_ID + "_" + RECEIVER_ID);
	}

	private static DispositionOptions mdnOptions() {
		return new DispositionOptions()
				.setProtocolImportance(IMPORTANCE_OPTIONAL)
				.setProtocol(PROTOCOL_PKCS7_SIGNATURE)
				.setMICAlgImportance(IMPORTANCE_OPTIONAL)
				.setMICAlg(DIGEST_SHA_256);
	}

	@SneakyThrows
	private AS2ClientRequest request() {
		return new AS2ClientRequest(SUBJECT)
				.setData(new DataHandler(fakeDataSource()))
				.setContentType(TEXT_PLAIN.getAsString());
	}

	@SneakyThrows
	private ByteArrayDataSource fakeDataSource() {
		return new ByteArrayDataSource(getClass().getResourceAsStream("/xml_to_send.xml"), TEXT_PLAIN.getAsString());
	}
}
