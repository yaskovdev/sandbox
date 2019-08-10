package user;

import static common.ExecutionResult.CONTINUE;
import static common.ExecutionResult.STOP;
import static java.util.Objects.isNull;

import java.io.IOException;

import lombok.RequiredArgsConstructor;

import org.apache.http.client.methods.HttpGet;
import org.apache.http.impl.client.CloseableHttpClient;

import com.fasterxml.jackson.databind.ObjectMapper;

import common.Command;
import common.Config;
import common.Context;
import common.ExecutionResult;
import model.Task;

@RequiredArgsConstructor
public class FetchTaskCommand implements Command {

	private final CloseableHttpClient client;
	private final Config config;
	final ObjectMapper mapper;

	@Override
	public ExecutionResult execute(final Context context) {
		System.out.println("Fetching task");

		final String json;
		try {
			json = client.execute(request(context.getJwt()), new SimpleResponseHandler());
		} catch (final IOException e) {
			throw new RuntimeException("cannot execute request", e);
		}

		final Task task;
		try {
			task = mapper.readValue(json, Task.class);
		} catch (final IOException e) {
			throw new RuntimeException("cannot deserialize task", e);
		}

		if (isNull(task.getId())) {
			System.out.println("No tasks in queue, going to try again later");
			return STOP;
		} else {
			context.setTask(task);
			return CONTINUE;
		}
	}

	private HttpGet request(final String jwt) {
		final HttpGet request = new HttpGet(config.getOrigin() + "/api/v1/task/not-started");
		request.setHeader("Authorization", jwt);
		return request;
	}
}
