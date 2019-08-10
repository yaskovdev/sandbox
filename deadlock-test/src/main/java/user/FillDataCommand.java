package user;

import static common.ExecutionResult.CONTINUE;
import static java.lang.Thread.sleep;

import lombok.SneakyThrows;

import common.Command;
import common.Context;
import common.ExecutionResult;

public class FillDataCommand implements Command {

	@Override
	@SneakyThrows
	public ExecutionResult execute(final Context context) {
		System.out.println("Filling task with data " + context.getTask());
		sleep(1000L);
		return CONTINUE;
	}
}
