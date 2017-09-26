package warmup;

import java.util.Scanner;

public class Staircase {

    public static void main(String[] args) {
        final Scanner scanner = new Scanner(System.in);
        final int size = scanner.nextInt();

        for (int i = size - 1; i >= 0; i--) {
            printRow(i, size);
        }
    }

    private static void printRow(final int rowIndex, final int rowLength) {
        for (int i = 0; i < rowLength; i++) {
            System.out.print(i < rowIndex ? " " : "#");
        }
        System.out.println();
    }
}
