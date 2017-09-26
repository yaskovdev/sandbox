package implementation;

import java.util.Scanner;

public class GradingStudents {

    public static void main(final String[] args) {
        final Scanner scanner = new Scanner(System.in);
        final int size = scanner.nextInt();
        final int[] grades = new int[size];
        for (int i = 0; i < size; i++) {
            grades[i] = scanner.nextInt();
        }
        for (final int grade : grades) {
            System.out.println(solve(grade));
        }
    }

    private static int solve(final int grade) {
        if (grade < 38) {
            return grade;
        }
        final int multipleOf5 = nextMultipleOf5(grade);
        final int diff = multipleOf5 - grade;
        if (diff < 3) {
            return multipleOf5;
        } else {
            return grade;
        }
    }

    private static int nextMultipleOf5(final int grade) {
        final int r = grade / 5;
        return 5 * (r + 1);
    }
}
