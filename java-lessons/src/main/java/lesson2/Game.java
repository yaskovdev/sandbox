package lesson2;

import java.util.Scanner;

public class Game {

    public static void main(String[] args) {
        final RandomChallengeFactory factory = new RandomChallengeFactory();
        final Challenge challenge = factory.generateChallenge();
        System.out.println("Is it true that " + challenge.getQuestion() + "?");

        final SolvedChallenge solved = solve(challenge);
        System.out.println("Your answer accepted");

        System.out.println(solved.isSolvedSuccessfully() ? "You succeeded!" : "You failed.");
    }

    private static SolvedChallenge solve(final Challenge challenge) {
        return new SolvedChallenge(challenge, new Scanner(System.in).nextBoolean());
    }
}
