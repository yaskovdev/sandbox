package codility.training;

/**
 * https://codility.com/programmers/lessons/1-iterations/binary_gap/
 */
public class BinaryGap {

    public static void main(String[] args) {
        System.out.println(new BinaryGap().solution(1041));
        System.out.println(new BinaryGap().solution(20));
        System.out.println(new BinaryGap().solution(1162));
        System.out.println(new BinaryGap().solution(1610612737));
        System.out.println(new BinaryGap().solution(51712));
        System.out.println(new BinaryGap().solution(1024));
        System.out.println(new BinaryGap().solution(6));
        System.out.println(new BinaryGap().solution(328));
    }

    public int solution(final int number) {
        int n = number;
        int currentGap = 0;
        int record = 0;
        boolean started = false;
        while (n > 0) {
            final int lastBinaryDigit = n % 2;
            if (lastBinaryDigit == 1) {
                started = true;
                if (record < currentGap) {
                    record = currentGap;
                }
                currentGap = 0;
            } else {
                if (started) {
                    currentGap++;
                }
            }
            n = (n - lastBinaryDigit) / 2;
        }
        return record;
    }
}
