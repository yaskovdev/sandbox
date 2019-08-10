package user;

import static common.ExecutionResult.CONTINUE;
import static java.util.Arrays.asList;
import static org.apache.http.HttpHeaders.CONTENT_TYPE;

import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;

import org.apache.http.client.methods.HttpPut;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;

import com.fasterxml.jackson.databind.ObjectMapper;

import common.Command;
import common.Config;
import common.Context;
import common.ExecutionResult;
import model.Address;
import model.Contact;
import model.Task;
import model.UpdateShipmentRequest;

@RequiredArgsConstructor
public class SubmitTaskCommand implements Command {

	private final CloseableHttpClient client;
	private final Config config;
	final ObjectMapper mapper;

	@SneakyThrows
	@Override
	public ExecutionResult execute(final Context context) {
		System.out.println("Submitting task " + context.getTask());
		client.execute(request(context.getJwt(), context.getTask()), new SimpleResponseHandler());
		return CONTINUE;
	}

	@SneakyThrows
	private HttpPut request(final String jwt, final Task task) {
		final HttpPut request = new HttpPut(config.getOrigin() + "/api/v1/task/" + task.getId() + "/submit");
		request.setHeader("Authorization", jwt);
		request.setHeader(CONTENT_TYPE, "application/json;charset=UTF-8");
		request.setEntity(new StringEntity(mapper.writeValueAsString(map(task))));
		return request;
	}

	private static UpdateShipmentRequest map(final Task task) {
		return UpdateShipmentRequest.builder()
				.shipmentId(task.getShipmentId())
				.mainService("PARCEL")
				.deliveryChannel("POST_OFFICE")
				.inboundNetwork("PRIME_EXPRE")
				.addresses(asList(address("RECEIVER", "57D7E18557F8BA0F3BB187828939E688"), address("SENDER_ADDRESS", "CA2807")))
				.contacts(asList(contact("SENDER", "United States of America"), contact("RECIPIENT", "Gena Bukin")))
				.build();
	}

	private static Address address(final String locationType, final String rmaId) {
		return Address.builder().locationType(locationType).rmaId(rmaId).build();
	}

	private static Contact contact(final String role, final String name) {
		return Contact.builder().role(role).name(name).build();
	}
}
