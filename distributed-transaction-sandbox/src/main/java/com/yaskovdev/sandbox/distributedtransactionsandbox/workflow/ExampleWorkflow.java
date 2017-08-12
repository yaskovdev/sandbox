package com.yaskovdev.sandbox.distributedtransactionsandbox.workflow;

import io.nflow.engine.workflow.definition.NextAction;
import io.nflow.engine.workflow.definition.StateExecution;
import io.nflow.engine.workflow.definition.WorkflowDefinition;
import io.nflow.engine.workflow.definition.WorkflowState;
import io.nflow.engine.workflow.definition.WorkflowStateType;

import static com.yaskovdev.sandbox.distributedtransactionsandbox.workflow.ExampleWorkflow.State.error;
import static com.yaskovdev.sandbox.distributedtransactionsandbox.workflow.ExampleWorkflow.State.repeat;
import static io.nflow.engine.workflow.definition.NextAction.moveToStateAfter;
import static io.nflow.engine.workflow.definition.WorkflowStateType.manual;
import static io.nflow.engine.workflow.definition.WorkflowStateType.start;
import static org.joda.time.DateTime.now;

public class ExampleWorkflow extends WorkflowDefinition<ExampleWorkflow.State> {

    public static final String TYPE = "repeatingWorkflow";

    public static final String VAR_COUNTER = "VAR_COUNTER";

    public enum State implements WorkflowState {

        repeat(start, "Repeating state"),
        error(manual, "Error state");

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

    public ExampleWorkflow() {
        super(TYPE, repeat, error);
    }

    @SuppressWarnings("unused")
    public NextAction repeat(final StateExecution execution) {
        System.out.println("Counter: " + execution.getVariable(VAR_COUNTER));
        execution.setVariable(VAR_COUNTER, execution.getVariable(VAR_COUNTER, Integer.class) + 1);
        return moveToStateAfter(repeat, now().plusSeconds(10), "Next iteration");
    }
}
