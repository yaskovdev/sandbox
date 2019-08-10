package user;

import java.io.IOException;

import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.ResponseHandler;
import org.apache.http.util.EntityUtils;

public class SimpleResponseHandler implements ResponseHandler<String> {

	@Override
	public String handleResponse(final HttpResponse response) throws IOException {
		int status = response.getStatusLine().getStatusCode();
		if (status >= 200 && status < 300) {
			final HttpEntity entity = response.getEntity();
			return entity == null ? null : EntityUtils.toString(entity);
		} else {
			throw new ClientProtocolException("unexpected response status: " + status);
		}
	}
}
