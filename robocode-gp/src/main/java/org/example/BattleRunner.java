package org.example;


import robocode.control.BattleSpecification;
import robocode.control.BattlefieldSpecification;
import robocode.control.RobocodeEngine;
import robocode.control.RobotSpecification;

import java.io.Closeable;
import java.io.File;

public class BattleRunner implements Closeable {
    private final RobocodeEngine engine;

    public BattleRunner() {
        allowRobotsReadExternalFiles();
        RobocodeEngine.setLogMessagesEnabled(false);
        final RobocodeEngine engine = new RobocodeEngine(new File("/Users/yaskovdev/robocode"));
        engine.addBattleListener(new BattleObserver());
        engine.setVisible(true);
        this.engine = engine;
    }

    public void runBattle() {
        final int numberOfRounds = 1;
        final BattlefieldSpecification battlefield = new BattlefieldSpecification(800, 600);
        final RobotSpecification[] selectedRobots = engine.getLocalRepository("org.example.PushRobot,sample.Tracker");
        final BattleSpecification battleSpec = new BattleSpecification(numberOfRounds, battlefield, selectedRobots);
        engine.runBattle(battleSpec, true);
        System.out.println("Battle is over");
    }

    @Override
    public void close() {
        engine.close();
    }

    private static void allowRobotsReadExternalFiles() {
        System.setProperty("NOSECURITY", "true");
    }
}
