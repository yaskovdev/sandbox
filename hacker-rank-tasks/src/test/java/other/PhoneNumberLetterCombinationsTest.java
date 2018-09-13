package other;

import org.junit.Test;

import java.util.List;

public class PhoneNumberLetterCombinationsTest {

    @Test
    public void test_letterCombinations() {
        final List<String> solutions = new PhoneNumberLetterCombinations().letterCombinations("23");
        System.out.println(solutions);
    }
}