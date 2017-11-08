package codility;

/**
 * Created by sergei.iaskov on 9/30/2017.
 */
public class First {

    public static void main(String[] args) {
        System.out.println(solution(5, new int[]{5, 5, 1, 7, 2, 3, 5}));
        System.out.println(solution(7, new int[]{1, 3, 7, 2, 5, 8}));
    }

    public static int solution(int x, int[] array) {
        int sameFromTheLeft = 0;
        int differentFromTheRight = 0;
        for (int element : array) {
            if (element != x) {
                differentFromTheRight++;
            }
        }
        for (int i = 0; i < array.length; i++) {
            final int element = array[i];
            if (element == x) {
                sameFromTheLeft++;
            } else {
                differentFromTheRight--;
            }
            if (sameFromTheLeft == differentFromTheRight) {
                return i + 1;
            }
        }
        return -1;
    }
}
