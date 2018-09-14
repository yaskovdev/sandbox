package other;

public class Task {

    private char name;
    private int delay;
    private int frequency;

    public Task(char name, int delay, int frequency) {
        this.name = name;
        this.delay = delay;
        this.frequency = frequency;
    }

    public char getName() {
        return name;
    }

    public int getDelay() {
        return delay;
    }

    public int getFrequency() {
        return frequency;
    }

    public void process(int n) {
        frequency--;
        delay = n;
    }

    public void decrementDelay() {
        delay--;
    }
}
