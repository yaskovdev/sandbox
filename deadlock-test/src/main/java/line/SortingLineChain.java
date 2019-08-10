package line;

import common.AbstractChain;

public class SortingLineChain extends AbstractChain {

	public SortingLineChain() {
		super(new CreateTaskCommand());
	}
}
