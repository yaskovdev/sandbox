package com.omniva.phoenix.dozer;

import java.util.HashSet;
import java.util.Objects;
import java.util.Set;

public class Main {

	static class Pair {
		public Pair(String a, String b) {
			this.a = a;
			this.b = b;
		}

		String a;
		String b;

		@Override
		public boolean equals(Object o) {
			if (this == o)
				return true;
			if (o == null || getClass() != o.getClass())
				return false;
			Pair pair = (Pair) o;
			return Objects.equals(a, pair.a) &&
					Objects.equals(b, pair.b);
		}

		@Override
		public int hashCode() {

			return Objects.hash(a, b);
		}
	}

	public static void main(String[] args) {
		Pair p = new Pair("a", "b");
		Set<Pair> set = new HashSet<>();
		set.add(p);
		p.b = "b2";
		System.out.println(set.contains(new Pair("a", "b2")));
	}
}
