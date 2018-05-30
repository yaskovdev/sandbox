package codility.training;

/**
 * https://codility.com/programmers/lessons/3-time_complexity/frog_jmp/
 */
public class FrogJmp {

    public static void main(String[] args) {
        System.out.println(new FrogJmp().solution(3, 5, 2));
        System.out.println(new FrogJmp().solution(1, 5, 3));
        System.out.println(new FrogJmp().solution(100, 100, 1_000_000_000));
    }

    public int solution(int x, int y, int jump) {
        final int distance = y - x;
        final int numberOfJumps = distance / jump;
        return jump * numberOfJumps < distance ? numberOfJumps + 1 : numberOfJumps;
    }
}
