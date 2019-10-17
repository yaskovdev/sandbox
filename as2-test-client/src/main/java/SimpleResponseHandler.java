import static java.lang.System.lineSeparator;
import static java.util.Arrays.stream;
import static java.util.stream.Collectors.toList;
import static org.apache.commons.lang3.StringUtils.join;
import static org.junit.Assert.assertTrue;

import java.io.IOException;
import java.util.List;

import lombok.SneakyThrows;

import org.apache.http.HttpResponse;
import org.apache.http.client.ResponseHandler;
import org.apache.http.util.EntityUtils;

public class SimpleResponseHandler implements ResponseHandler<String> {

	private static final String NEW_LINE = lineSeparator();
	private static final String SEPARATOR = NEW_LINE + "########" + NEW_LINE;

	@Override
	public String handleResponse(final HttpResponse response) throws IOException {
		final List<String> headers = stream(response.getAllHeaders()).map(Object::toString).collect(toList());
		final String entity = EntityUtils.toString(response.getEntity());
		assertMic("GI3f0fqGnJWPHRhLOJQMJ0LuhknJIbOBB3k+o6ghO58=, sha-256", entity);
		final String formattedHeaders = join(headers, NEW_LINE);
		return SEPARATOR + formattedHeaders + SEPARATOR + entity + SEPARATOR;
	}

	@SneakyThrows
	private static void assertMic(final String expected, final String response) {
		assertTrue(response.contains("Received-Content-MIC: " + expected));
	}
}
