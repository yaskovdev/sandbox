package graph.theory;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class JourneyToTheMoon {

    private static class Graph {

        private final List<Set<Integer>> matrix;

        private Graph(final int size) {
            matrix = new ArrayList<Set<Integer>>();
            for (int i = 0; i < size; i++) {
                matrix.add(new HashSet<Integer>());
            }
        }

        void connect(int a, int b) {
            matrix.get(a).add(b);
            matrix.get(b).add(a);
        }

        List<Integer> powersOfGroups() {
            final Set<Integer> potentialGroups = new HashSet<Integer>();
            for (int i = 0; i < matrix.size(); i++) {
                potentialGroups.add(i);
            }
            final List<Integer> result = new ArrayList<Integer>();
            while (!potentialGroups.isEmpty()) {
                final Integer potentialGroup = potentialGroups.iterator().next();
                final HashSet<Integer> visited = new HashSet<Integer>();
                powerOfGroup(potentialGroup, visited);
                final int powerOfGroup = visited.size();
                potentialGroups.removeAll(visited);
                result.add(powerOfGroup);
            }
            return result;
        }

        private int powerOfGroup(int element, Set<Integer> visited) {
            int result = 1;
            visited.add(element);
            for (final Integer neighbor : neighboursOf(element)) {
                if (!visited.contains(neighbor)) {
                    result += powerOfGroup(neighbor, visited);
                }
            }
            return result;
        }

        private Set<Integer> neighboursOf(int element) {
            return matrix.get(element);
        }
    }

    public static void main(String[] args) throws Exception {
        BufferedReader bfr = new BufferedReader(new InputStreamReader(System.in));
        String[] temp = bfr.readLine().split(" ");
        int numberOfAstronauts = Integer.parseInt(temp[0]);

        Graph graph = new Graph(numberOfAstronauts);
        int I = Integer.parseInt(temp[1]);


        for (int i = 0; i < I; i++) {
            temp = bfr.readLine().split(" ");
            int a = Integer.parseInt(temp[0]);
            int b = Integer.parseInt(temp[1]);
            graph.connect(a, b);
        }

        long combinations = 0;
        final List<Integer> powersOfGroups = graph.powersOfGroups();

        for (int i = 0; i < powersOfGroups.size(); i++) {
            int result = 0;
            final Integer powerOfGroup = powersOfGroups.get(i);
            for (int j = i + 1; j < powersOfGroups.size(); j++) {
                result += powerOfGroup * powersOfGroups.get(j);
            }
            combinations += result;
        }

        System.out.println(combinations);
    }
}
