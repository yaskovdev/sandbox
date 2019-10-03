package user;

import static common.Counter.deadlocks;

import java.io.IOException;

import lombok.RequiredArgsConstructor;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ResponseHandler;
import org.apache.http.util.EntityUtils;

import com.fasterxml.jackson.databind.ObjectMapper;

import model.ResponseError;

@RequiredArgsConstructor
public class CountingDeadlockResponseHandler implements ResponseHandler<String> {

	private final ObjectMapper mapper;

	@Override
	public String handleResponse(HttpResponse response) throws IOException {
		int status = response.getStatusLine().getStatusCode();
		final HttpEntity entity = response.getEntity();
		final String body = entity == null ? "" : EntityUtils.toString(entity);
		if (status >= 200 && status < 300) {
			return body;
		} else {
			System.out.println("Entity was " + body);
			final ResponseError error = mapper.readValue(body, ResponseError.class);
			if ("org.springframework.dao.CannotAcquireLockException".equals(error.getDeveloperMessage())) {
				deadlocks.incrementAndGet();
			}
			return "{}";
		}
	}
}
