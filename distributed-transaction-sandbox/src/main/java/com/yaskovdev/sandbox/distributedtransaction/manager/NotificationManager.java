package com.yaskovdev.sandbox.distributedtransaction.manager;

import com.yaskovdev.sandbox.distributedtransaction.model.Notification;
import com.yaskovdev.sandbox.distributedtransaction.workflow.CreateNotificationWorkflow;
import io.nflow.engine.service.WorkflowInstanceService;
import io.nflow.engine.workflow.instance.WorkflowInstanceFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

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
                .setType(CreateNotificationWorkflow.TYPE)
                .putStateVariable(CreateNotificationWorkflow.VAR_NOTIFICATION, notification)
                .build());
    }
}
