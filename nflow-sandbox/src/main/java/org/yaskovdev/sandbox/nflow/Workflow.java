package org.yaskovdev.sandbox.nflow;

import io.nflow.engine.workflow.definition.WorkflowDefinition;
import io.nflow.engine.workflow.definition.WorkflowSettings;

public class Workflow extends WorkflowDefinition {
    protected Workflow(String type, Enum initialState, Enum errorState) {
        super(type, initialState, errorState);
    }

    protected Workflow(String type, Enum initialState, Enum errorState, WorkflowSettings settings) {
        super(type, initialState, errorState, settings);
    }
}
