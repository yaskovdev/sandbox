package lesson3;

import java.util.Scanner;

public class Game {

    public static void main(String[] args) {
        final User user = new User();
        final Challenge challenge = new Challenge("2 + 2 = 5", false);
        System.out.println("Is it true that " + challenge.getQuestion() + "?");

        final SolvedChallenge solved = user.solve(challenge);
        System.out.println(solved.isSolvedSuccessfully() ? "You succeeded!" : "You failed.");
    }
}
