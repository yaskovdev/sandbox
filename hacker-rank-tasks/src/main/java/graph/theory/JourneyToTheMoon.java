package graph.theory;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;
import java.util.Set;

public class JourneyToTheMoon {

    private static class Astronaut {
        int id;
        Set<Astronaut> compatriots = new HashSet<Astronaut>();

        Astronaut(int id) {
            this.id = id;
        }

        @Override
        public String toString() {
            return "" + id;
        }
    }

    private static class Graph {
        Map<Integer, Astronaut> astronauts = new HashMap<Integer, Astronaut>();

        void connect(int a, int b) {
            final Astronaut first = astronauts.get(a);
            final Astronaut second = astronauts.get(b);
            first.compatriots.add(second);
            second.compatriots.add(first);
        }

        void makeAstronautsWithoutCountryCompatriotsOfOneCountry() {
            Astronaut lastAstronautWithoutCountry = null;
            for (final Integer integer : astronauts.keySet()) {
                final Astronaut astronaut = astronauts.get(integer);
                if (astronaut.compatriots.isEmpty()) {
                    if (lastAstronautWithoutCountry != null) {
                        connect(lastAstronautWithoutCountry.id, astronaut.id);
                    }
                    lastAstronautWithoutCountry = astronaut;
                }
            }
        }
    }

    public static void main(String[] args) throws Exception {
        BufferedReader bfr = new BufferedReader(new InputStreamReader(System.in));
        String[] temp = bfr.readLine().split(" ");
        int numberOfAstronauts = Integer.parseInt(temp[0]);

        Graph graph = new Graph();
        for (int i = 0; i < numberOfAstronauts; i++) {
            graph.astronauts.put(i, new Astronaut(i));
        }
        int I = Integer.parseInt(temp[1]);


        for (int i = 0; i < I; i++) {
            temp = bfr.readLine().split(" ");
            int a = Integer.parseInt(temp[0]);
            int b = Integer.parseInt(temp[1]);
            graph.connect(a, b);
        }

        graph.makeAstronautsWithoutCountryCompatriotsOfOneCountry();

        long combinations = 0;

        // Compute the final answer - the number of combinations

        System.out.println(combinations);
    }
}
