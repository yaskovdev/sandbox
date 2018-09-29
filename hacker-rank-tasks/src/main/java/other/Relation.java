package other;

class Relation {

    boolean knows(int a, int b) {
        if (a == 0 && b == 1) {
            return true;
        } else if (a == 0 && b == 2) {
            return true;
        } else if (a == 1 && b == 2) {
            return true;
        } else if (a == 3 && b == 2) {
            return true;
        } else if (a == 4 && b == 2) {
            return true;
        } else {
            return false;
        }
    }
}
