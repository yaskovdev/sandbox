package user;

import static common.ExecutionResult.CONTINUE;
import static java.lang.Thread.sleep;

import common.Command;
import common.Context;
import common.ExecutionResult;

public class FillDataCommand implements Command {

	@Override
	public ExecutionResult execute(final Context context) {
		System.out.println("Filling task with data " + context.getTask());
		try {
			sleep(1000L);
		} catch (final InterruptedException ignored) {
		}
		return CONTINUE;
	}
}
