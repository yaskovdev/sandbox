package line;

import static common.ExecutionResult.CONTINUE;

import common.Command;
import common.Context;
import common.ExecutionResult;

public class CreateTaskCommand implements Command {

	@Override
	public ExecutionResult execute(final Context context) {
		System.out.println("Creating task");
		return CONTINUE;
	}
}
