package lesson1;

public class Main {

    public static void main(final String[] args) {
        final Dog dog = new Dog("Шарик", 2010);
        printPetInfo(dog);
        final Cat cat = new Cat("Барсик", 2005);
        printPetInfo(cat);
    }

    private static void printPetInfo(final Pet pet) {
        System.out.println("Имя питомца: " + pet.getName() + ", возраст: " + pet.getAgeInYears() + " лет, питомец говорит " + pet.getVoice());
    }
}
