package com.yaskovdev.sandbox.spring.transaction;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.builder.SpringApplicationBuilder;
import org.springframework.boot.web.support.SpringBootServletInitializer;

@SpringBootApplication
public class SpringTransactionSandboxApplication extends SpringBootServletInitializer {

	@Override
	protected SpringApplicationBuilder configure(final SpringApplicationBuilder application) {
		return application.sources(SpringTransactionSandboxApplication.class);
	}

	public static void main(final String[] args) {
		SpringApplication.run(SpringTransactionSandboxApplication.class, args);
	}
}
