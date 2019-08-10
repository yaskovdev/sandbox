package user;

import static common.ExecutionResult.CONTINUE;

import java.util.Random;

import lombok.SneakyThrows;

import common.Command;
import common.Context;
import common.ExecutionResult;

public class FillDataCommand implements Command {

	private static final int MIN = 5;
	private static final int DELTA = 60;
	private final Random random = new Random();

	@Override
	@SneakyThrows
	public ExecutionResult execute(final Context context) {
		System.out.println("User " + context.getUsername() + " is filling task with data " + context.getTask());
		//		final int seconds = MIN + random.nextInt(DELTA + 1);
		Thread.sleep(10000L);
		return CONTINUE;
	}
}
