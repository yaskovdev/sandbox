package lesson2;

public class SolvedChallenge extends Challenge {

    private final boolean userAgrees;

    public SolvedChallenge(final Challenge challenge, final boolean userAgrees) {
        super(challenge.getQuestion(), challenge.isTruthful());
        this.userAgrees = userAgrees;
    }

    public boolean isSolvedSuccessfully() {
        return userAgrees == isTruthful();
    }
}
