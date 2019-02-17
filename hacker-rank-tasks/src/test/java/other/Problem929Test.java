package other;

import static org.junit.Assert.assertEquals;

import org.junit.Test;

public class Problem929Test {

	@Test
	public void test() {
		assertEquals(2, new Problem929().numUniqueEmails(new String[] { "test.email+alex@leetcode.com", "test.e.mail+bob.cathy@leetcode.com", "testemail+david@lee.tcode.com" }));
	}
}
