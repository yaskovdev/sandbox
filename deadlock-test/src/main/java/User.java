import static com.fasterxml.jackson.databind.DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES;

import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;

import com.fasterxml.jackson.databind.ObjectMapper;

import common.Config;
import common.Context;
import user.LoginChain;
import user.VideoCodingChain;

public class User {

	public static void main(final String... args) {
		final CloseableHttpClient httpClient = HttpClients.createDefault();
		final Config config = new Config("http://localhost:8108");
		final ObjectMapper mapper = new ObjectMapper().configure(FAIL_ON_UNKNOWN_PROPERTIES, false);

		final Context context = new Context();
		context.setUsername("sergei.iaskov");
		new LoginChain(httpClient, config, mapper).execute(context);

		while (true) {
			new VideoCodingChain(httpClient, config, mapper).execute(context);
		}
	}
}
