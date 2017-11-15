package atlassian;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

import static java.util.Arrays.asList;

public class Validate {

    public static void main(String[] args) {
//        System.out.println(validate("|name|address|~n|Patrick|patrick@test.com|pat@test.com|~n|Annie|annie@test.com|~n|Zoe|~n"));
//        System.out.println(validate("|name|address|~n|Patrick|patrick@test.com|pat@test.com|~n|Annie||annie@test.com|~n"));
        final String s = "name~||address";
        final String replaced = s.replace("~|", Character.toString('\u0000'));
        final List<String> split = new ArrayList<>(Arrays.asList(replaced.split("\\|")));
        for (int i = 0; i < split.size(); i++) {
            final String string = split.get(i);
            split.set(i, string.replace(Character.toString('\u0000'), "~|"));
        }

        System.out.println(split);
    }

    private static String validate(final String input) {
        final ValidationResult validationResult = new ValidationResult();
        validateCharacters(input, validationResult);

        final List<String> lines = asList(input.split("~n"));

        for (int i = 0; i < lines.size(); i++) {
            final String line = lines.get(i);
            validateStartAndEnd(line, validationResult);

            if (i == 0) {
                validateHeader(line, validationResult);
            }
            System.out.println(line);
        }

        return "";
    }

    private static void validateHeader(final String header, final ValidationResult validationResult) {

    }

    private static void validateCharacters(final String input, final ValidationResult validationResult) {
        for (final char character : input.toCharArray()) {
            if (!validCharacter(character)) {
                validationResult.errorHappened();
                return;
            }
        }
    }

    private static void validateStartAndEnd(final String line, final ValidationResult validationResult) {
        if (!line.startsWith("|") || !line.endsWith("|")) {
            validationResult.errorHappened();
        }
    }

    private static boolean validCharacter(char character) {
        return character >= 32 && character <= 126;
    }
}
