package model;

import lombok.Builder;
import lombok.Data;

@Data
@Builder
public class Contact {

	private String role;
	private String name;
}
