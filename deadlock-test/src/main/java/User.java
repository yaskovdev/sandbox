import java.io.IOException;

import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.CloseableHttpClient;

import user.SimpleResponseHandler;

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
			final String body = client.execute(request, new SimpleResponseHandler());
			System.out.println("----------------------------------------");
			System.out.println(body);
		} catch (final IOException e) {
			throw new RuntimeException(e);
		}
	}
}
