package other;

import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

import java.util.Arrays;

import org.junit.Test;

public class Problem139Test {

	@Test
	public void test() {
		assertTrue(new Problem139().wordBreak("dogscat", Arrays.asList("dog", "dogs", "cat")));
	}

	@Test
	public void test2() {
		assertFalse(new Problem139().wordBreak("dogscat", Arrays.asList("dog", "cat")));
	}
}
