package preparation;

import org.junit.Test;

import java.util.HashMap;
import java.util.Map;

import static org.junit.Assert.assertEquals;

public class HashTablesRansomNote {

    @Test
    public void test0() {
        assertEquals("Yes", checkMagazine2(new String[]{"give", "me", "one", "grand", "today", "night"},
                new String[]{"give", "one", "grand", "today"}));
    }

    @Test
    public void test1() {
        assertEquals("No", checkMagazine2(new String[]{"two", "times", "three", "is", "not", "four"},
                new String[]{"two", "times", "two", "is", "four"}));
    }

    @Test
    public void test2() {
        assertEquals("No", checkMagazine2(new String[]{"жопа"},
                new String[]{"счастье"}));
    }

    private static String checkMagazine2(String[] magazineArray, String[] noteArray) {
        final Map<String, Integer> magazine = new HashMap<>(magazineArray.length);
        for (String word : magazineArray) {
            final Integer count = magazine.getOrDefault(word, 0);
            magazine.put(word, count + 1);
        }

        final Map<String, Integer> note = new HashMap<>(noteArray.length);
        for (String word : noteArray) {
            final Integer count = note.getOrDefault(word, 0);
            note.put(word, count + 1);
        }

        for (final String word : note.keySet()) {
            final Integer requiredCount = note.get(word);
            final Integer availableCount = magazine.getOrDefault(word, 0);
            if (requiredCount > availableCount) {
                return "No";
            } else {
                magazine.put(word, availableCount - requiredCount);
            }
        }
        return "Yes";
    }

    static void checkMagazine(String[] magazine, String[] note) {
        System.out.print(checkMagazine2(magazine, note));
    }
}
