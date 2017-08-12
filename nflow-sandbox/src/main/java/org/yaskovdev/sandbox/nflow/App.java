package org.yaskovdev.sandbox.nflow;

import io.nflow.jetty.StartNflow;

public class App {

    public static void main(String[] args) throws Exception {
        new StartNflow().startJetty(7500, "local", "");
    }
}
