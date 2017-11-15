package atlassian;

/**
 * Created by Sergey on 15.11.2017.
 */
public class ValidationResult {
    private boolean error;
    private int lengthOfHeader;
    private int lengthOfLongestRecord = 0;

    public void errorHappened() {
        error = true;
    }

    public void setLengthOfHeader(final int lengthOfHeader) {
        this.lengthOfHeader = lengthOfHeader;
    }

    public void updateLengthOfLongestRecordIfNeeded(final int lengthOfCandidate) {
        if (lengthOfCandidate > lengthOfLongestRecord) {
            lengthOfLongestRecord = lengthOfCandidate;
        }
    }
}
