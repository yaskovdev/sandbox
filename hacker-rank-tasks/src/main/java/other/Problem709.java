package other;

public class Problem709 {

	public String toLowerCase(String str) {
		final StringBuilder builder = new StringBuilder();
		for (final Character c : str.toCharArray()) {
			builder.append(Character.toLowerCase(c));
		}
		return builder.toString();
	}
}
