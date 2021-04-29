package lesson2;

public class Challenge {

    private final String question;
    private final boolean truthful;

    public Challenge(final String question, final boolean truthful) {
        this.question = question;
        this.truthful = truthful;
    }

    public String getQuestion() {
        return question;
    }

    public boolean isTruthful() {
        return truthful;
    }
}
