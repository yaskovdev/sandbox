package user;

import static common.ExecutionResult.CONTINUE;
import static common.ExecutionResult.STOP;
import static java.lang.Thread.sleep;
import static java.util.Objects.isNull;

import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;

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

	private static final long DELAY_IF_NO_TASKS_MS = 2000L;

	private final CloseableHttpClient client;
	private final Config config;
	private final ObjectMapper mapper;

	@SneakyThrows
	@Override
	public ExecutionResult execute(final Context context) {
		System.out.println("User " + context.getUsername() + " is fetching task");
		final String json = client.execute(request(context.getJwt()), new CountingDeadlockResponseHandler(mapper));
		final Task task = mapper.readValue(json, Task.class);

		if (isNull(task.getId())) {
			System.out.println("No tasks in queue, going to try again after " + DELAY_IF_NO_TASKS_MS + " ms");
			sleep(DELAY_IF_NO_TASKS_MS);
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
