package com.yaskovdev.sandbox.distributedtransactionsandbox;

import com.yaskovdev.sandbox.distributedtransactionsandbox.client.JdbcClient;
import com.yaskovdev.sandbox.distributedtransactionsandbox.client.JmsClient;
import com.yaskovdev.sandbox.distributedtransactionsandbox.workflow.ExampleWorkflow;
import io.nflow.engine.internal.config.EngineConfiguration;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Import;

@SpringBootApplication
@Import(EngineConfiguration.class)
public class DistributedTransactionSandboxApplication {

    @Bean
    public ExampleWorkflow exampleWorkflow(final JdbcClient jdbcClient, final JmsClient jmsClient) {
        return new ExampleWorkflow(jdbcClient, jmsClient);
    }

    public static void main(final String[] args) {
        SpringApplication.run(DistributedTransactionSandboxApplication.class, args);
    }
}
