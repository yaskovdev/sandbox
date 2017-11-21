package atlassian;

import java.util.ArrayList;
import java.util.List;

import static java.util.Arrays.asList;

public class Validate {

    public static void main(String[] args) {
        System.out.println(validate("|name|address|~n|Patrick|patrick@test.com|pat@test.com|~n|Annie||annie@test.com|~n"));
        System.out.println(validate("|name|address|~n|Patrick|patrick@test.com|pat@test.com|~n|Annie|annie@test.com|~n|Zoe|~n"));
    }

    private static List<String> splitRecord(final String s) {
        final String withoutExtraDelimiters = s.substring(1, s.length() - 1);
        final String replaced = withoutExtraDelimiters.replace("~|", Character.toString('\u0000'));
        final List<String> split = new ArrayList<>(asList(replaced.split("\\|")));
        for (int i = 0; i < split.size(); i++) {
            final String string = split.get(i);
            split.set(i, string.replace(Character.toString('\u0000'), "~|"));
        }

        return split;
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
            } else {
                validateRecord(line, validationResult);
            }
        }

        return validationResult.format();
    }

    private static void validateRecord(final String line, final ValidationResult validationResult) {
        final List<String> fields = splitRecord(line);
        validationResult.updateLengthOfLongestRecordIfNeeded(fields.size());
        validationResult.increaseTotalNumberOfFields(fields);
    }

    private static void validateHeader(final String header, final ValidationResult validationResult) {
        final List<String> names = splitRecord(header);
        validationResult.setLengthOfHeader(names.size());
        validationResult.setLastHeaderFieldName(names.get(names.size() - 1));
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

    private static boolean validCharacter(final char character) {
        return character >= 32 && character <= 126;
    }
}
