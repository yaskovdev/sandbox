import static com.fasterxml.jackson.databind.DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES;
import static common.Counter.deadlocks;
import static java.lang.System.currentTimeMillis;

import lombok.RequiredArgsConstructor;

import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;

import com.fasterxml.jackson.databind.ObjectMapper;

import common.Config;
import common.Context;
import user.LoginChain;
import user.VideoCodingChain;

@RequiredArgsConstructor
public class User implements Runnable {

	private static final long WORK_DURATION = 10 * 60 * 1000;

	private final String username;

	@Override
	public void run() {
		final CloseableHttpClient httpClient = HttpClients.createDefault();
		final Config config = new Config("http://localhost:8115");
		final ObjectMapper mapper = new ObjectMapper().configure(FAIL_ON_UNKNOWN_PROPERTIES, false);

		final Context context = new Context();
		context.setUsername(username);
		new LoginChain(httpClient, config, mapper).execute(context);

		final long workStartedTimeMs = currentTimeMillis();
		while (true) {
			final long workDurationMs = currentTimeMillis() - workStartedTimeMs;
			if (workDurationMs >= WORK_DURATION) {
				System.out.println("User worked more than " + WORK_DURATION + " ms, stopping");
				break;
			}
			new VideoCodingChain(httpClient, config, mapper).execute(context);
			System.out.println("Number of deadlocks so far " + deadlocks.get());
		}
	}
}
