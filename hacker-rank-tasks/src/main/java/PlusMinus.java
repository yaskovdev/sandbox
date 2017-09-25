import java.util.Scanner;

/**
 * Created by Sergey on 03.09.2017.
 */
public class PlusMinus {

    public static void main(String[] args) {
        final Scanner scanner = new Scanner(System.in);
        final int size = scanner.nextInt();
        final int[] array = new int[size];
        for (int i = 0; i < size; i++) {
            array[i] = scanner.nextInt();
        }
        printFractions(array);
    }

    private static void printFractions(final int[] array) {
        int positives = 0;
        int negatives = 0;
        int zeros = 0;
        for (final int i : array) {
            if (i > 0) {
                positives++;
            } else if (i < 0) {
                negatives++;
            } else {
                zeros++;
            }
        }
        System.out.println((float) positives / array.length);
        System.out.println((float) negatives / array.length);
        System.out.println((float) zeros / array.length);
    }
}
