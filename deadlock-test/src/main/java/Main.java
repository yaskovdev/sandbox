import static java.lang.System.currentTimeMillis;
import static java.util.concurrent.TimeUnit.MILLISECONDS;

import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;

public class Main {

	private final static String TOKEN_1 = "";

	private final static String TOKEN_2 = "";

	public static void main(final String... args) throws InterruptedException {
		final CloseableHttpClient client = HttpClients.createDefault();

		final User client1 = new User(client, request(TOKEN_1));
		final User client2 = new User(client, request(TOKEN_2));

		final Thread thread1 = new Thread(client1);
		final Thread thread2 = new Thread(client2);

		final long start = currentTimeMillis();
		thread1.start();
		thread2.start();
		thread1.join();
		thread2.join();
		final long delta = currentTimeMillis() - start;
		System.out.println("Passed " + MILLISECONDS.toSeconds(delta) + " seconds");
	}

	private static HttpGet request(final String token) {
		final HttpGet request = new HttpGet("http://localhost:8108/api/v1/task/not-started");
		request.setHeader("Authorization", token);
		return request;
	}
}
