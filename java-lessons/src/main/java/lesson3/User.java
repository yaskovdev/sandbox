package lesson3;

import java.util.Scanner;

public class User {

    public SolvedChallenge solve(Challenge challenge) {
        return new SolvedChallenge(challenge, new Scanner(System.in).nextBoolean());
    }
}
