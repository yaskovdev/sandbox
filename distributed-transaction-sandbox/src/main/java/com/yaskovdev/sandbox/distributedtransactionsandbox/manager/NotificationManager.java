package com.yaskovdev.sandbox.distributedtransactionsandbox.manager;

import com.yaskovdev.sandbox.distributedtransactionsandbox.model.Notification;
import com.yaskovdev.sandbox.distributedtransactionsandbox.workflow.ExampleWorkflow;
import io.nflow.engine.service.WorkflowInstanceService;
import io.nflow.engine.workflow.instance.WorkflowInstanceFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import java.util.UUID;

@Component
public class NotificationManager {

    private final WorkflowInstanceService workflowInstances;

    private final WorkflowInstanceFactory workflowInstanceFactory;

    @Autowired
    public NotificationManager(final WorkflowInstanceService workflowInstances, final WorkflowInstanceFactory workflowInstanceFactory) {
        this.workflowInstances = workflowInstances;
        this.workflowInstanceFactory = workflowInstanceFactory;
    }

    public void createNotification(final Notification notification) {
        workflowInstances.insertWorkflowInstance(workflowInstanceFactory.newWorkflowInstanceBuilder()
                .setType(ExampleWorkflow.TYPE)
                .setExternalId(UUID.randomUUID().toString())
                .putStateVariable(ExampleWorkflow.VAR_NOTIFICATION, notification)
                .build());
    }
}
