package com.yaskovdev.as2sandbox;

import static org.springframework.http.ResponseEntity.ok;

import lombok.RequiredArgsConstructor;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.helger.as2lib.client.AS2ClientResponse;

@RestController
@RequiredArgsConstructor
@RequestMapping("/outgoing")
class OutgoingAs2MessageController {

	private final CustomAs2Client client;

	@PostMapping
	ResponseEntity<String> handlePost() {
		final AS2ClientResponse response = client.sendSynchronous();
		return ok(response.getAsString());
	}
}
