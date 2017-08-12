package com.yaskovdev.sandbox.distributedtransactionsandbox;

import com.yaskovdev.sandbox.distributedtransactionsandbox.workflow.ExampleWorkflow;
import io.nflow.engine.internal.config.EngineConfiguration;
import io.nflow.engine.service.WorkflowInstanceService;
import io.nflow.engine.workflow.instance.WorkflowInstanceFactory;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Import;

import javax.annotation.PostConstruct;
import javax.inject.Inject;

@SpringBootApplication
@Import(EngineConfiguration.class)
public class DistributedTransactionSandboxApplication {

    @Inject
    private WorkflowInstanceService workflowInstances;

    @Inject
    private WorkflowInstanceFactory workflowInstanceFactory;

    @Bean
    public ExampleWorkflow exampleWorkflow() {
        return new ExampleWorkflow();
    }

    @PostConstruct
    public void createExampleWorkflowInstance() {
        workflowInstances.insertWorkflowInstance(workflowInstanceFactory.newWorkflowInstanceBuilder()
                .setType(ExampleWorkflow.TYPE)
                .setExternalId("example")
                .putStateVariable(ExampleWorkflow.VAR_COUNTER, 0)
                .build());
    }

    public static void main(final String[] args) {
        SpringApplication.run(DistributedTransactionSandboxApplication.class, args);
    }
}
