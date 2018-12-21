package other;

import static java.util.Arrays.asList;
import static java.util.Collections.emptyList;
import static org.junit.Assert.assertEquals;

import org.junit.Test;

public class Problem17Test {

	@Test
	public void test() {
		assertEquals(new Problem17().letterCombinations("23"), asList("ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"));
	}

	@Test
	public void test2() {
		assertEquals(new Problem17().letterCombinations(""), emptyList());
	}
}
