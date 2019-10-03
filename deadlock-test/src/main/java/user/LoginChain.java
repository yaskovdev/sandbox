package user;

import org.apache.http.impl.client.CloseableHttpClient;

import com.fasterxml.jackson.databind.ObjectMapper;

import common.AbstractChain;
import common.Config;

public class LoginChain extends AbstractChain {

	public LoginChain(final CloseableHttpClient client, final Config config, final ObjectMapper mapper) {
		super(new LoginCommand(client, config, mapper));
	}
}
