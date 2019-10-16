import static java.nio.charset.StandardCharsets.UTF_8;
import static java.nio.file.Files.readAllLines;
import static java.nio.file.Paths.get;
import static java.util.stream.Collectors.toList;

import java.util.List;

import lombok.SneakyThrows;

public class Simulation {

	@SneakyThrows
	private void start() {
		final List<String> names = readAllLines(get(getClass().getResource("users.txt").toURI()), UTF_8);
		final List<User> users = names.stream().map(name -> new User(name)).collect(toList());
		for (final User user : users) {
			final Thread thread = new Thread(user);
			thread.start();
		}
	}

	public static void main(final String[] args) {
		new Simulation().start();
	}
}
