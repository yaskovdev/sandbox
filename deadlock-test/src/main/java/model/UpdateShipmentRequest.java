package model;

import java.util.List;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class UpdateShipmentRequest {

	private Long shipmentId;
	private String mainService;
	private String deliveryChannel;
	private String inboundNetwork;
	private List<Contact> contacts;
	private List<Address> addresses;
}
