import java.io.IOException;

import org.apache.http.HttpEntity;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.ResponseHandler;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.util.EntityUtils;

class User implements Runnable {

	private final CloseableHttpClient client;
	private final HttpGet request;

	User(final CloseableHttpClient client, final HttpGet request) {
		this.client = client;
		this.request = request;
	}

	@Override
	public void run() {
		try {
			final ResponseHandler<String> handler = response -> {
				int status = response.getStatusLine().getStatusCode();
				if (status >= 200 && status < 300) {
					HttpEntity entity = response.getEntity();
					return entity != null ? EntityUtils.toString(entity) : null;
				} else {
					throw new ClientProtocolException("unexpected response status: " + status);
				}
			};
			final String body = client.execute(request, handler);
			System.out.println("----------------------------------------");
			System.out.println(body);
		} catch (final IOException e) {
			throw new RuntimeException(e);
		}
	}
}
