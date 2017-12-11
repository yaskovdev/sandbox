package atlassian;

import java.time.ZonedDateTime;
import java.time.format.DateTimeFormatter;

public class Main {

    public static void main(final String[] args) {
        final ZonedDateTime dateTime = ZonedDateTime.parse("20.12.2017 12:00 Europe/Moscow", DateTimeFormatter.ofPattern("dd.MM.yyyy HH:mm z"));
        System.out.println(dateTime.toInstant());
    }
}
