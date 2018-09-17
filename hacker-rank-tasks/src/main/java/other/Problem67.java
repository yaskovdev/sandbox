package other;

class Problem67 {

    String addBinary(String a, String b) {
        StringBuilder first = new StringBuilder(a).reverse();
        StringBuilder second = new StringBuilder(b).reverse();
        StringBuilder longer = first.length() > second.length() ? first : second;
        StringBuilder shorter = first.length() > second.length() ? second : first;
        final int difference = longer.length() - shorter.length();
        for (int i = 0; i < difference; i++) {
            shorter.append("0");
        }
        return add(shorter.toString(), longer.toString());
    }

    private String add(String a, String b) {
        int buffer = 0;
        final StringBuilder sum = new StringBuilder();
        for (int i = 0; i < a.length(); i++) {
            int value = Integer.parseInt(a.charAt(i) + "") + Integer.parseInt(b.charAt(i) + "") + buffer;
            if (value < 2) {
                buffer = 0;
                sum.append(value);
            } else {
                buffer = 1;
                sum.append(value - 2);
            }
        }
        if (buffer > 0) {
            sum.append(buffer);
        }
        return sum.reverse().toString();
    }
}
