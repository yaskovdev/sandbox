package other;

import java.util.HashSet;
import java.util.Set;

public class Problem929 {

	public int numUniqueEmails(String[] emails) {
		final Set<String> uniqueEmails = new HashSet<>();
		for (String email : emails) {
			uniqueEmails.add(normalize(email));
		}
		return uniqueEmails.size();
	}

	private static String normalize(String email) {
		final String[] tokens = email.split("@");
		final String localPart = tokens[0];
		final String domain = tokens[1];
		final String localPartBeforePlusSign = localPart.split("\\+")[0];
		return localPartBeforePlusSign.replace(".", "") + "@" + domain;
	}
}
