package user;

import static common.ExecutionResult.CONTINUE;
import static org.apache.http.HttpHeaders.CONTENT_TYPE;

import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;

import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;

import com.fasterxml.jackson.databind.ObjectMapper;

import common.Command;
import common.Config;
import common.Context;
import common.ExecutionResult;
import model.AuthRequest;
import model.AuthResponse;

@RequiredArgsConstructor
public class LoginCommand implements Command {

	private final CloseableHttpClient client;
	private final Config config;
	final ObjectMapper mapper;

	@SneakyThrows
	@Override
	public ExecutionResult execute(final Context context) {
		final String json = client.execute(request(context.getUsername()), new SimpleResponseHandler());
		final AuthResponse response = mapper.readValue(json, AuthResponse.class);
		context.setJwt(response.getToken());
		return CONTINUE;
	}

	@SneakyThrows
	private HttpPost request(final String username) {
		final HttpPost request = new HttpPost(config.getOrigin() + "/api/v1/auth");
		request.setHeader(CONTENT_TYPE, "application/json;charset=UTF-8");
		request.setEntity(new StringEntity(mapper.writeValueAsString(map(username))));
		return request;
	}

	private static AuthRequest map(final String username) {
		return AuthRequest.builder().username(username).password("whatever").build();
	}
}
