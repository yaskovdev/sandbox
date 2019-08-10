package user;

import org.apache.http.impl.client.CloseableHttpClient;

import com.fasterxml.jackson.databind.ObjectMapper;

import common.AbstractChain;
import common.Config;

public class VideoCodingChain extends AbstractChain {

	public VideoCodingChain(final CloseableHttpClient client, final Config config, final ObjectMapper mapper) {
		super(new FetchTaskCommand(client, config, mapper), new FillDataCommand(), new SubmitTaskCommand(client, config, mapper));
	}
}
