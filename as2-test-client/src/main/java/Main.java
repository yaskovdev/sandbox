import static org.apache.commons.codec.binary.Base64.decodeBase64;
import static org.apache.http.impl.client.HttpClients.createDefault;

import lombok.SneakyThrows;

import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.ByteArrayEntity;
import org.apache.http.impl.client.CloseableHttpClient;

public class Main {

	@SneakyThrows
	public static void main(String[] args) {
		try (final CloseableHttpClient client = createDefault()) {
			final HttpPost request = new HttpPost("http://localhost:8118/pyas2/as2receive");
			request.setHeader("Content-Type", "application/pkcs7-mime; name=\"smime.p7z\"; smime-type=compressed-data");
			request.setHeader("Subject", "Test Message From Omniva");
			request.setHeader("Message-ID", "<ph-OpenAS2-16102019133919+0300-2191@OMNIVA_TEST_OPEN_TEXT_LOCAL>");
			request.setHeader("Content-Transfer-Encoding", "binary");
			request.setHeader("Connection", "close, TE");
			request.setHeader("User-Agent", "ph-OpenAS2/AS2Sender");
			request.setHeader("Mime-Version", "1.0");
			request.setHeader("AS2-Version", "1.1");
			request.setHeader("Date", "Wed, 16 Oct 2019 13:39:19 +0300");
			request.setHeader("Recipient-Address", "http://localhost:8888/pyas2/as2receive");
			request.setHeader("AS2-From", "OMNIVA_TEST");
			request.setHeader("AS2-To", "OPEN_TEXT_LOCAL");
			request.setHeader("From", "yaskovdev@gmail.com");
			request.setHeader("Disposition-Notification-To", "yaskovdev@gmail.com");
			request.setHeader("Disposition-Notification-Options", "signed-receipt-protocol=optional, pkcs7-signature; signed-receipt-micalg=optional, sha-256");
			request.setHeader("Host", "localhost:8888");
			request.setHeader("Accept-Encoding", "gzip,deflate");
			request.setEntity(new ByteArrayEntity(decodeBase64(
					"MIAGCyqGSIb3DQEJEAEJoIAwgAIBADANBgsqhkiG9w0BCRADCDCABgkqhkiG9w0BBwGggCSABF54nHPOzytJzSvRDaksSLVSKEmtKNEvyEnMzOPlcobJFCXmFaelFum65iXnp2TmpVspJGXmJRZV8nLxctkU5eeX2EX4+iikZeakKqQV5ecq+OfmZZYl2uiDpQByCCGMAAAAAAAAAAAAAAAA")));
			final String response = client.execute(request, new SimpleResponseHandler());
			System.out.println(response);
		}
	}
}
