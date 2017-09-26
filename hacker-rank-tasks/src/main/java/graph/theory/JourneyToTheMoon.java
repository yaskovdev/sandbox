package graph.theory;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

public class JourneyToTheMoon {

    private static class Graph {

        private final Map<Integer, Set<Integer>> matrix;
        private final int size;

        private Graph(final int size) {
            this.size = size;
            this.matrix = new HashMap<Integer, Set<Integer>>();
        }

        void connect(int a, int b) {
            get(a).add(b);
            get(b).add(a);
        }

        private Set<Integer> get(int a) {
            final Set<Integer> result = matrix.get(a);
            if (result == null) {
                final HashSet<Integer> neighbours = new HashSet<Integer>();
                matrix.put(a, neighbours);
                return neighbours;
            } else {
                return result;
            }
        }

        List<Integer> powersOfGroups() {
            final Set<Integer> elements = new HashSet<Integer>();
            for (int i = 0; i < size; i++) {
                elements.add(i);
            }
            final List<Integer> result = new ArrayList<Integer>(size);
            while (!elements.isEmpty()) {
                final Integer elementOfGroup = elements.iterator().next();
                final int before = elements.size();
                deleteGroup(elementOfGroup, elements);
                result.add(before - elements.size());
            }
            return result;
        }

        private void deleteGroup(final int element, final Set<Integer> elements) {
            elements.remove(element);
            for (final Integer neighbor : neighboursOf(element)) {
                if (elements.contains(neighbor)) {
                    deleteGroup(neighbor, elements);
                }
            }
        }

        private Set<Integer> neighboursOf(final int element) {
            return get(element);
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
