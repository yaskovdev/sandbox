package graph.theory;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

public class JourneyToTheMoon {

    private static class Graph {

        private final int[][] matrix;

        private final List<Integer> groups;

        private Graph(final int size) {
            matrix = new int[size][size];
            for (int[] row : matrix) {
                Arrays.fill(row, 0);
            }
            groups = new ArrayList<Integer>(size);
            for (int i = 0; i < size; i++) {
                groups.add(i);
            }
        }

        void connect(int a, int b) {
            matrix[a][b] = matrix[b][a] = 1;
            if (groups.contains(a)) {
                groups.remove((Integer) a);
            } else if (groups.contains(b)) {
                groups.remove((Integer) b);
            }
        }

        List<Integer> powersOfGroups() {
            final List<Integer> result = new ArrayList<Integer>(groups.size()); // TODO: +1?
            for (Integer group : groups) {
                result.add(powerOfGroup(group, new HashSet<Integer>()));
            }
            return result;
        }

        private int powerOfGroup(int element, Set<Integer> visited) {
            int result = 1;
            visited.add(element);
            for (Integer neighbor : neighboursOf(element)) {
                if (!visited.contains(neighbor)) {
                    result += powerOfGroup(neighbor, visited);
                }
            }
            return result;
        }

        private List<Integer> neighboursOf(int element) {
            final int[] row = this.matrix[element];
            final List<Integer> result = new ArrayList<Integer>(row.length);
            for (int i = 0; i < row.length; i++) {
                if (row[i] == 1) {
                    result.add(i);
                }
            }
            return result;
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
