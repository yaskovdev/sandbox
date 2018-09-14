package other;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;

import static java.util.Comparator.comparing;
import static java.util.stream.Collectors.toList;

class Problem621 {

    int leastInterval(char[] input, int n) {
        final Map<Character, Integer> frequency = new HashMap<>();
        for (char c : input) {
            final Integer value = frequency.computeIfAbsent(c, k -> 0);
            frequency.put(c, value + 1);
        }
        List<Task> tasks = new ArrayList<>();
        for (Character character : frequency.keySet()) {
            tasks.add(new Task(character, 0, frequency.get(character)));
        }

        int time = 0;
        while (!tasks.isEmpty()) {
            final Optional<Task> task = tasks.stream().filter(t -> t.getDelay() == 0).max(comparing(Task::getFrequency));
            task.ifPresent(t -> t.process(n + 1));
            tasks.stream().filter(t -> t.getDelay() > 0).forEach(Task::decrementDelay);
            tasks = tasks.stream().filter(t -> t.getFrequency() > 0).collect(toList());
            time++;
        }
        return time;
    }
}
