package lesson1;

import java.time.Year;

public class Pet {

    private final String name;
    private final int yearOfBirth;

    public Pet(final String name, int yearOfBirth) {
        this.name = name;
        this.yearOfBirth = yearOfBirth;
    }

    public String getName() {
        return name;
    }

    public int getYearOfBirth() {
        return yearOfBirth;
    }

    public int getAgeInYears() {
        return Year.now().getValue() - yearOfBirth;
    }

    public String getVoice() {
        return "";
    }
}
