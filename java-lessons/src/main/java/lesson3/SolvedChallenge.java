package lesson3;

public class SolvedChallenge extends Challenge {

    private final boolean userAgreed;

    public SolvedChallenge(final Challenge challenge, boolean userAgreed) {
        super(challenge.getQuestion(), challenge.isTruthful());
        this.userAgreed = userAgreed;
    }

    public boolean isSolvedSuccessfully() {
        return userAgreed == isTruthful();
    }

    public String toString() {
        return isSolvedSuccessfully() ? "You succeeded!" : "You failed.";
    }
}
