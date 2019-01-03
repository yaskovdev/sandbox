package other;

public class TryFinally {

    public static void main(String[] args) {
        System.out.println(new TryFinally().method());
    }

    public int method() {
        try {
            return 0;
        } finally {
            System.out.println("Finally");
            return 1;
        }
    }
}
