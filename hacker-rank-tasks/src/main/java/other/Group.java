package other;

public class Group {

    final String pattern;

    public Group(String pattern) {
        this.pattern = pattern;
    }

    @Override
    public String toString() {
        return "(" + pattern + ")";
    }
}
