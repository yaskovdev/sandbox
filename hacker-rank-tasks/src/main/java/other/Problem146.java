package other;

import java.util.HashMap;
import java.util.Map;

public class Problem146 {
}

class LRUCache {

	private final int capacity;
	private final Map<Integer, Integer> map;

	public LRUCache(final int capacity) {
		this.capacity = capacity;
		this.map = new HashMap<>(capacity);
	}

	public int get(final int key) {
		return map.getOrDefault(key, -1);
	}

	public void put(final int key, final int value) {
		map.put(key, value);
	}
}
