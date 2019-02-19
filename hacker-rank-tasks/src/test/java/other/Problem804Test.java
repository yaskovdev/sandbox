package other;

import static org.junit.Assert.assertEquals;

import org.junit.Test;

public class Problem804Test {

	@Test
	public void test1() {
		assertEquals(2, new Problem804().uniqueMorseRepresentations(new String[] { "gin", "zen", "gig", "msg" }));
	}
}
