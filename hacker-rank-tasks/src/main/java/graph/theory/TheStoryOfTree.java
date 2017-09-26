package graph.theory;

import java.math.BigInteger;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Queue;
import java.util.Scanner;
import java.util.Set;

public class TheStoryOfTree {

    private static class Pair {
        int u;
        int v;

        Pair(int u, int v) {
            this.u = u;
            this.v = v;
        }

        boolean containsNode(int node) {
            return u == node || v == node;
        }

        Integer anotherSideFrom(int node) {
            return node == u ? v : u;
        }

        @Override
        public boolean equals(Object o) {
            if (this == o) return true;
            if (o == null || getClass() != o.getClass()) return false;

            Pair pair = (Pair) o;

            if (u != pair.u) return false;
            return v == pair.v;
        }

        @Override
        public int hashCode() {
            int result = u;
            result = 31 * result + v;
            return result;
        }
    }

    private static class Game {
        List<Pair> tree;
        List<Pair> guesses;
        int minimumWinningScore;

        Game(List<Pair> tree, List<Pair> guesses, int minimumWinningScore) {
            this.tree = tree;
            this.guesses = guesses;
            this.minimumWinningScore = minimumWinningScore;
        }
    }

    public static void main(String[] args) {
        final List<Game> games = new ArrayList<Game>();
        final Scanner in = new Scanner(System.in);
        int numberOfGames = in.nextInt();
        for (int game = 0; game < numberOfGames; game++) {
            int numberOfTreeNodes = in.nextInt();
            final List<Pair> tree = new ArrayList<Pair>();
            for (int a1 = 0; a1 < numberOfTreeNodes - 1; a1++) {
                int u = in.nextInt();
                int v = in.nextInt();
                final Pair pair = new Pair(u, v);
                tree.add(pair);
            }
            int numberOfGuesses = in.nextInt();
            int minimumWinningScore = in.nextInt();
            final List<Pair> guesses = new ArrayList<Pair>();
            for (int a1 = 0; a1 < numberOfGuesses; a1++) {
                int u = in.nextInt();
                int v = in.nextInt();
                guesses.add(new Pair(u, v));
            }
            games.add(new Game(tree, guesses, minimumWinningScore));
        }

        for (Game game : games) {
            System.out.println(probabilityToWinTheGame(game));
        }
    }

    private static String probabilityToWinTheGame(final Game game) {
        final Set<Integer> rootsToCheck = possibleRootsOf(game.tree);
        final int denominator = rootsToCheck.size();
        int totalWins = 0;
        while (!rootsToCheck.isEmpty()) {
            final Integer root = rootsToCheck.iterator().next();
            final List<Integer> childrenOfRoot = childrenOf(root, game.tree);
            totalWins += totalWins(game, root, childrenOfRoot, rootsToCheck);
            rootsToCheck.remove(root);
            rootsToCheck.removeAll(childrenOfRoot);
        }
        return reduceFraction(totalWins, denominator);
    }

    private static int totalWins(final Game game, final Integer root, final List<Integer> childrenOfRoot, Set<Integer> rootsToCheck) {
        int totalWins = 0;
        final Set<Pair> relationships = allRelationships(root, game.tree);
        if (wouldWin(game, relationships)) {
            totalWins++;
        }

        for (final Integer child : childrenOfRoot) {
            if (rootsToCheck.contains(child)) {
                final Set<Pair> updatedRelationships = updateRelationships(root, child, relationships);
                if (wouldWin(game, updatedRelationships)) {
                    totalWins++;
                }
            }
        }

        return totalWins;
    }

    private static Set<Pair> updateRelationships(Integer oldRoot, Integer newRoot, Set<Pair> oldRelationships) {
        final Set<Pair> newRelationships = new HashSet<Pair>(oldRelationships);
        newRelationships.remove(new Pair(oldRoot, newRoot));
        newRelationships.add(new Pair(newRoot, oldRoot));
        return newRelationships;
    }

    private static boolean wouldWin(final Game game, final Set<Pair> relationships) {
        int points = 0;
        for (final Pair guess : game.guesses) {
            if (isCorrect(guess, relationships)) {
                points++;
            }
        }
        return points >= game.minimumWinningScore;
    }

    private static String reduceFraction(int numerator, int denominator) {
        final BigInteger gcd = BigInteger.valueOf(numerator).gcd(BigInteger.valueOf(denominator));
        final int gcdValue = gcd.intValue();
        return (numerator / gcdValue) + "/" + (denominator / gcdValue);
    }

    private static boolean isCorrect(Pair guess, Set<Pair> allParentOfRelationships) {
        return allParentOfRelationships.contains(guess);
    }

    private static Set<Integer> possibleRootsOf(List<Pair> tree) {
        final Set<Integer> result = new HashSet<Integer>();
        for (Pair pair : tree) {
            result.add(pair.u);
            result.add(pair.v);
        }
        return result;
    }

    private static Set<Pair> allRelationships(int root, List<Pair> tree) {
        final Queue<Integer> queue = new LinkedList<Integer>();
        final Set<Integer> visited = new HashSet<Integer>();
        queue.add(root);
        visited.add(root);
        final Set<Pair> result = new HashSet<Pair>();
        while (!queue.isEmpty()) {
            final int head = queue.remove();
            for (final Integer child : childrenOf(head, tree)) {
                if (!visited.contains(child)) {
                    result.add(new Pair(head, child));
                    visited.add(child);
                    queue.add(child);
                }
            }
        }
        return result;
    }

    private static List<Integer> childrenOf(int node, List<Pair> tree) {
        final List<Integer> result = new ArrayList<Integer>();
        for (Pair edge : tree) {
            if (edge.containsNode(node)) {
                result.add(edge.anotherSideFrom(node));
            }
        }
        return result;
    }
}
