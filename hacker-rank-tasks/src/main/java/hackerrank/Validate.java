package hackerrank;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

/**
 * Created by sergei.iaskov on 9/30/2017.
 */
public class Validate {

    public static void main(String[] args) {
        System.out.println(validate("|name|address|~n|Patrick|patrick@test.com|pat@test.com|~n|Annie|annie@test.com|~n|Zoe|~n"));
//        System.out.println(validate("|name|address|~n|Patrick|patrick@test.com|pat@test.com|~n|Annie||annie@test.com|~n"));
    }

    static String validate(String input) {
        final String[] lines = input.split("~n");
        final List<String> linesList = Arrays.asList(lines);
        final String headerLine = lines[0];
        final String[] headerFields = headerLine.substring(1, headerLine.length() - 1).split("\\|");
        final List<String> header = new ArrayList<>(Arrays.asList(headerFields));
        boolean error = false;
        int numberOfFields = 0;
        int numberOfRecords = 0;
        int numberOfEmptyFields = 0;
        boolean headersFixed = false;
        for (String record : linesList.subList(1, linesList.size())) {
            final String[] fields = record.substring(1, record.length() - 1).split("\\|");
            numberOfRecords++;
            if (!record.startsWith("|") || !record.endsWith("|")) {
                error = true;
            }
            numberOfFields = fields.length;
            if (!headersFixed && fields.length > header.size()) {
                final String lastHeader = header.get(header.size() - 1);
                for (int i = 0; i < fields.length - header.size(); i++) {
                    header.add(lastHeader + "_" + (i + 1));
                }
                headersFixed = true;
            }
            for (String field : fields) {
                if (field.isEmpty()) {
                    numberOfEmptyFields++;
                }
            }
        }
        return error ? "0:0:0:format_error" : numberOfRecords + ":" + numberOfFields + ":" + numberOfEmptyFields + ":" + header.get(header.size() - 1);
    }

    static boolean validCharacter(char character) {
        return character >= 32 && character <= 126;
    }
}
