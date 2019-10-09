package lesson1;

public class Cat extends Pet {

    public Cat(final String name, final int yearOfBirth) {
        super(name, yearOfBirth);
    }

    @Override
    public String getVoice() {
        return "Мяу!";
    }
}
