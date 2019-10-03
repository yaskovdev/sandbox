package common;

import static common.ExecutionResult.STOP;
import static java.util.Arrays.asList;

import java.util.List;

public abstract class AbstractChain {

	private final List<Command> chain;

	public AbstractChain(final Command... commands) {
		this.chain = asList(commands);
	}

	public void execute(final Context context) {
		for (final Command command : chain) {
			final ExecutionResult result = command.execute(context);
			if (result == STOP) {
				return;
			}
		}
	}
}
