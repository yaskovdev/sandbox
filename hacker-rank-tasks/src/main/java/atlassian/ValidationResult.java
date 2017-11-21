package atlassian;

import java.util.List;

class ValidationResult {
    private boolean error;
    private int lengthOfHeader;
    private String lastHeaderFieldName;
    private int lengthOfLongestRecord = 0;
    private int recordsInTotal = 0;
    private int emptyValuesInTotal = 0;

    void errorHappened() {
        error = true;
    }

    void setLengthOfHeader(final int lengthOfHeader) {
        this.lengthOfHeader = lengthOfHeader;
    }

    void updateLengthOfLongestRecordIfNeeded(final int lengthOfCandidate) {
        recordsInTotal++;
        if (lengthOfCandidate > lengthOfLongestRecord) {
            lengthOfLongestRecord = lengthOfCandidate;
        }
    }

    String format() {
        if (error) {
            return "0:0:0:format_error";
        } else {
            final int diff = lengthOfLongestRecord - lengthOfHeader;
            final String nameOfTheLastField = diff > 0 ? lastHeaderFieldName + "_" + diff : lastHeaderFieldName;
            return recordsInTotal + ":" + lengthOfLongestRecord + ":" + emptyValuesInTotal + ":" + nameOfTheLastField;
        }
    }

    void increaseTotalNumberOfFields(final List<String> fields) {
        for (final String field : fields) {
            if (field.isEmpty()) {
                emptyValuesInTotal++;
            }
        }
    }

    void setLastHeaderFieldName(final String lastHeaderFieldName) {
        this.lastHeaderFieldName = lastHeaderFieldName;
    }
}
