package lesson3;

public class Challenge {

    private final String question;
    private final boolean truthful;

    public Challenge(String question, boolean truthful) {
        this.question = question;
        this.truthful = truthful;
    }

    public String getQuestion() {
        return question;
    }

    public boolean isTruthful() {
        return truthful;
    }

    public String toString() {
        return "Is it true that " + question + "?";
    }
}
