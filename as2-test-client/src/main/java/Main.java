import static java.lang.String.format;
import static org.apache.commons.codec.binary.Base64.decodeBase64;
import static org.apache.http.impl.client.HttpClients.createDefault;

import lombok.SneakyThrows;

import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.ByteArrayEntity;
import org.apache.http.impl.client.CloseableHttpClient;

public class Main {

	private static final String HOST = "localhost:8117";

	@SneakyThrows
	public static void main(String[] args) {
		try (final CloseableHttpClient client = createDefault()) {
			final HttpPost request = new HttpPost(format("http://%s/pyas2/as2receive", HOST));
			request.setHeader("Content-type", "application/pkcs7-mime; smime-type=enveloped-data");
			request.setHeader("Subject", "AS2 Data Message Request");
			request.setHeader("Message-ID", "<003780288263670635@as2.eu.gxsics.com>");
			request.setHeader("Connection", "close");
			request.setHeader("User-Agent", "RPT-HTTPClient/0.3-3I (Linux)");
			request.setHeader("Mime-Version", "1.0");
			request.setHeader("AS2-Version", "1.2");
			request.setHeader("Date", "Thu, 17 Oct 2019 08:04:24 GMT");
			request.setHeader("AS2-From", "GXS_IPC_EDI");
			request.setHeader("AS2-To", "OMNIVA_TEST");
			request.setHeader("From", "notifyas2@gxs.com");
			request.setHeader("Disposition-Notification-To", "http://as2.eu.gxsics.com/enterprise/as2");
			request.setHeader("Disposition-Notification-Options", "signed-receipt-protocol=optional,pkcs7-signature; signed-receipt-micalg=optional, sha-256, sha1, md5");
			request.setHeader("EDIINT-Features", "AS2-Reliability");
			request.setHeader("Host", HOST);
			request.setHeader("Accept-Encoding", "deflate, gzip, x-gzip, compress, x-compress");
			request.setEntity(new ByteArrayEntity(decodeBase64(
					"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")));
			final String response = client.execute(request, new SimpleResponseHandler());
			System.out.println(response);
		}
	}
}
