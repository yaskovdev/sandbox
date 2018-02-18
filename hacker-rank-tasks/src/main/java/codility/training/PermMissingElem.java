package codility.training;

/**
 * https://app.codility.com/programmers/lessons/3-time_complexity/perm_missing_elem/
 */
public class PermMissingElem {

    public static void main(final String[] args) {
        System.out.println(new PermMissingElem().solution(new int[]{2, 3, 1, 5}));
        System.out.println(new PermMissingElem().solution(new int[]{2}));
    }

    public int solution(final int[] array) {
        int outerSum = 0;
        for (int i = 1; i <= array.length + 1; i++) {
            outerSum += i;
        }
        int innerSum = 0;
        for (int i : array) {
            innerSum += i;
        }
        return outerSum - innerSum;
    }
}
