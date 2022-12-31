package org.example;

import dev.robocode.tankroyale.botapi.Bot;
import dev.robocode.tankroyale.botapi.BotInfo;
import dev.robocode.tankroyale.botapi.InitialPosition;

import java.util.List;

public class MyFirstBot extends Bot {

    public MyFirstBot() {
        super(new BotInfo("My First Bot", "1.0", List.of("Sergey Yaskov"), "My first bot", "",
            List.of("ee"), List.of("melee", "classic", "1v1"), "Java", "Java", InitialPosition.fromString("")));
    }
}
