import static java.nio.charset.Charset.forName;
import static java.nio.file.Files.readAllLines;
import static java.nio.file.Paths.get;

import java.util.List;
import java.util.stream.Collectors;

import lombok.SneakyThrows;

public class Simulation {

	@SneakyThrows
	private void start() {
		final List<String> names = readAllLines(get(getClass().getResource("users.txt").toURI()), forName("UTF-8"));
		final List<User> users = names.stream().map(name -> new User(name)).collect(Collectors.toList());
		for (final User user : users) {
			final Thread thread = new Thread(user);
			thread.start();
		}
	}

	public static void main(String[] args) {
		new Simulation().start();
	}
}
