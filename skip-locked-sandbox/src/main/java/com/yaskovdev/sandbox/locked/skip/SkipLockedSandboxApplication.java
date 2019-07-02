package com.yaskovdev.sandbox.locked.skip;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.builder.SpringApplicationBuilder;
import org.springframework.boot.web.support.SpringBootServletInitializer;

@SpringBootApplication
public class SkipLockedSandboxApplication extends SpringBootServletInitializer {

	@Override
	protected SpringApplicationBuilder configure(final SpringApplicationBuilder application) {
		return application.sources(SkipLockedSandboxApplication.class);
	}

	public static void main(final String[] args) {
		SpringApplication.run(SkipLockedSandboxApplication.class, args);
	}
}
