package implementation;

import java.util.Scanner;

public class AppleAndOrange {

    public static void main(String[] args) {
        Scanner in = new Scanner(System.in);
        int houseStart = in.nextInt();
        int houseEnd = in.nextInt();
        int appleTree = in.nextInt();
        int orangeTree = in.nextInt();
        int numberOfApples = in.nextInt();
        int numberOfOranges = in.nextInt();
        int[] apples = new int[numberOfApples];
        for (int i = 0; i < numberOfApples; i++) {
            apples[i] = in.nextInt();
        }
        int[] oranges = new int[numberOfOranges];
        for (int i = 0; i < numberOfOranges; i++) {
            oranges[i] = in.nextInt();
        }

        int numberOfGoodApples = 0;
        for (int apple : apples) {
            if (appleTree + apple >= houseStart && appleTree + apple <= houseEnd) {
                numberOfGoodApples++;
            }
        }

        int numberOfGoodOranges = 0;
        for (int orange : oranges) {
            if (orangeTree + orange >= houseStart && orangeTree + orange <= houseEnd) {
                numberOfGoodOranges++;
            }
        }

        System.out.println(numberOfGoodApples);
        System.out.println(numberOfGoodOranges);
    }
}
