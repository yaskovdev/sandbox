package com.yaskovdev.sandbox.distributedtransaction.workflow;

import com.yaskovdev.sandbox.distributedtransaction.client.JdbcClient;
import com.yaskovdev.sandbox.distributedtransaction.client.JmsClient;
import com.yaskovdev.sandbox.distributedtransaction.model.Notification;
import io.nflow.engine.workflow.definition.NextAction;
import io.nflow.engine.workflow.definition.StateExecution;
import io.nflow.engine.workflow.definition.WorkflowDefinition;
import io.nflow.engine.workflow.definition.WorkflowSettings;
import io.nflow.engine.workflow.definition.WorkflowState;
import io.nflow.engine.workflow.definition.WorkflowStateType;
import org.slf4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import static com.yaskovdev.sandbox.distributedtransaction.workflow.CreateNotificationWorkflow.State.createEvent;
import static com.yaskovdev.sandbox.distributedtransaction.workflow.CreateNotificationWorkflow.State.done;
import static com.yaskovdev.sandbox.distributedtransaction.workflow.CreateNotificationWorkflow.State.error;
import static com.yaskovdev.sandbox.distributedtransaction.workflow.CreateNotificationWorkflow.State.sendNotification;
import static io.nflow.engine.workflow.definition.NextAction.moveToState;
import static io.nflow.engine.workflow.definition.WorkflowStateType.end;
import static io.nflow.engine.workflow.definition.WorkflowStateType.normal;
import static io.nflow.engine.workflow.definition.WorkflowStateType.start;
import static org.slf4j.LoggerFactory.getLogger;

@Component
public class CreateNotificationWorkflow extends WorkflowDefinition<CreateNotificationWorkflow.State> {

    private static final Logger logger = getLogger(CreateNotificationWorkflow.class);

    public static final String TYPE = "createNotificationWorkflow";

    public static final String VAR_NOTIFICATION = "VAR_NOTIFICATION";

    private final JdbcClient jdbcClient;

    private final JmsClient jmsClient;

    public enum State implements WorkflowState {

        createEvent(start, "Event is persisted to database"),
        sendNotification(normal, "Notification is sent to a message queue"),
        done(end, "Notification creation is finished"),
        error(end, "Error state");

        private final WorkflowStateType type;
        private final String description;

        State(final WorkflowStateType type, final String description) {
            this.type = type;
            this.description = description;
        }

        @Override
        public WorkflowStateType getType() {
            return type;
        }

        @Override
        public String getDescription() {
            return description;
        }
    }

    @Autowired
    public CreateNotificationWorkflow(final JdbcClient jdbcClient, final JmsClient jmsClient) {
        super(TYPE, createEvent, error, new WorkflowSettings.Builder()
                .setMinErrorTransitionDelay(10000)
                .setMaxRetries(5)
                .build());
        this.jdbcClient = jdbcClient;
        this.jmsClient = jmsClient;
        permit(createEvent, sendNotification);
        permit(createEvent, error);
        permit(sendNotification, done);
        permit(sendNotification, error);
    }

    @SuppressWarnings("unused")
    public NextAction createEvent(final StateExecution execution) {
        final Notification notification = execution.getVariable(VAR_NOTIFICATION, Notification.class);
        logger.info("Going to create event for " + notification);
        jdbcClient.createEvent(notification);
        return moveToState(sendNotification, "Event created, going to send notification");
    }

    @SuppressWarnings("unused")
    public NextAction sendNotification(final StateExecution execution) {
        final Notification notification = execution.getVariable(VAR_NOTIFICATION, Notification.class);
        logger.info("Going to send " + notification + " to a message queue");
        jmsClient.sendNotification(notification);
        return moveToState(done, "Notification sent, going to done");
    }

    @SuppressWarnings("unused")
    public void done(final StateExecution execution) {
        final Notification notification = execution.getVariable(VAR_NOTIFICATION, Notification.class);
        logger.info("Notification " + notification + " processing done");
    }

    @SuppressWarnings("unused")
    public void error(final StateExecution execution) {
        final Notification notification = execution.getVariable(VAR_NOTIFICATION, Notification.class);
        logger.info("Notification " + notification + " processing error");
    }
}
