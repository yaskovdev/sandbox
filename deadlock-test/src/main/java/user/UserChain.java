package user;

import org.apache.http.impl.client.CloseableHttpClient;

import com.fasterxml.jackson.databind.ObjectMapper;

import common.AbstractChain;
import common.Config;

public class UserChain extends AbstractChain {

	public UserChain(final CloseableHttpClient client, final Config config, final ObjectMapper mapper) {
		super(new FetchTaskCommand(client, config, mapper), new FillDataCommand(), new SubmitTaskCommand(client, config, mapper));
	}
}
