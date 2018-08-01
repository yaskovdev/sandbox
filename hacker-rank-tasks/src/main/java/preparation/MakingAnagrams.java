package preparation;

import org.junit.Assert;
import org.junit.Test;

import java.util.HashMap;
import java.util.Map;

public class MakingAnagrams {

    @Test
    public void test0() {
        Assert.assertEquals(4, new MakingAnagrams().solution("cde", "abc"));
    }

    @Test
    public void test1() {
        Assert.assertEquals(0, new MakingAnagrams().solution("cde", "dec"));
    }

    @Test
    public void test2() {
        Assert.assertEquals(2, new MakingAnagrams().solution("c", "abc"));
    }

    @Test
    public void test3() {
        Assert.assertEquals(1, new MakingAnagrams().solution("abc", "ba"));
    }

    @Test
    public void test4() {
        Assert.assertEquals(2, new MakingAnagrams().solution("baa", "abc"));
    }

    int solution(String a, String b) {
        final Map<Character, Integer> first = new HashMap<>();
        for (char c : a.toCharArray()) {
            if (first.containsKey(c)) {
                first.put(c, first.get(c) + 1);
            } else {
                first.put(c, 1);
            }
        }

        final Map<Character, Integer> second = new HashMap<>();
        for (char c : b.toCharArray()) {
            if (second.containsKey(c)) {
                second.put(c, second.get(c) + 1);
            } else {
                second.put(c, 1);
            }
        }

        int counter = 0;
        for (Character character : first.keySet()) {
            counter += Math.min(first.get(character), second.getOrDefault(character, 0));
        }
        return (a.length() - counter) + (b.length() - counter);
    }
}
