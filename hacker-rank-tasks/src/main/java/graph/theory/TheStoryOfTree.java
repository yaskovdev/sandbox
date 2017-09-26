package graph.theory;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
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

    public static void main(String[] args) {
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

            System.out.println(probabilityToWinTheGame(tree, guesses, minimumWinningScore));
        }
    }

    private static String probabilityToWinTheGame(List<Pair> tree, List<Pair> guesses, int minimumWinningScore) {
        final Set<Integer> possibleRoots = possibleRootsOf(tree);
        int numerator = 0;
        final int denominator = possibleRoots.size();
        for (int root : possibleRoots) {
            final List<Pair> allParentOfRelationships = allParentOfRelationships(root, tree);

            int points = 0;
            for (final Pair guess : guesses) {
                if (isCorrect(guess, allParentOfRelationships)) {
                    points++;
                }
            }
            if (points >= minimumWinningScore) {
                numerator++;
            }
        }
        return numerator + "/" + denominator;
    }

    private static boolean isCorrect(Pair guess, List<Pair> allParentOfRelationships) {
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

    private static List<Pair> allParentOfRelationships(int root, List<Pair> tree) {
        final List<Pair> relationships = new ArrayList<Pair>();
        allParentOfRelationships(relationships, root, tree);
        return relationships;
    }

    private static void allParentOfRelationships(List<Pair> relationships, int node, List<Pair> tree) {
        for (final Integer child : childrenOf(node, tree)) {
            final Pair parentOf = new Pair(node, child);
            relationships.add(parentOf);
            final List<Pair> treeWithoutRelationship = new ArrayList<Pair>(tree);
            treeWithoutRelationship.remove(parentOf);
            allParentOfRelationships(relationships, child, treeWithoutRelationship);
        }
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
