package lesson1;

public class Dog extends Pet {

    public Dog(final String name, final int yearOfBirth) {
        super(name, yearOfBirth);
    }

    @Override
    public String getVoice() {
        return "Гав!";
    }
}
