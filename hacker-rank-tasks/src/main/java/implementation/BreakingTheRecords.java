package implementation;

import java.util.Scanner;

public class BreakingTheRecords {

    private static int[] getRecord(int[] scores) {
        int minSoFar = scores[0];
        int maxSoFar = scores[0];

        int mins = 0;
        int maxes = 0;
        for (final int score : scores) {
            if (score < minSoFar) {
                minSoFar = score;
                mins++;
            } else if (score > maxSoFar) {
                maxSoFar = score;
                maxes++;
            }
        }

        return new int[]{maxes, mins};
    }

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        int n = in.nextInt();
        int[] s = new int[n];
        for (int s_i = 0; s_i < n; s_i++) {
            s[s_i] = in.nextInt();
        }
        int[] result = getRecord(s);
        String separator = "", delimiter = " ";
        for (Integer val : result) {
            System.out.print(separator + val);
            separator = delimiter;
        }
        System.out.println("");
    }
}
