package org.example;


import lombok.SneakyThrows;
import org.spiderland.Psh.BooleanStack;
import org.spiderland.Psh.FloatStack;
import org.spiderland.Psh.Interpreter;
import org.spiderland.Psh.ObjectStack;
import org.spiderland.Psh.Program;

import javax.swing.*;
import java.awt.*;

public class RectanglesDrawingExample extends JFrame {
    private static final int D_W = 2000;
    private static final int D_H = 200;
    private static final int CART_WIDTH = 50;
    private static final int CART_HEIGHT = 50;

    private final Interpreter interpreter = new Interpreter();

    private float cartPosition = 0.5f;
    private float cartVelocity = 1.0f;

    private final float cartAccelerationAbs = 0.5f;

    private boolean positiveForce;

    int time = 0;

    int y = 100;

    DrawPanel drawPanel = new DrawPanel();

    @SneakyThrows
    public RectanglesDrawingExample() {
        // (float.- (float.- (float.- input.in1 ((input.inallrev)) float.neg) ((input.inallrev)) float.neg) input.in1 (((((input.inallrev) (-1.4300003)))) (float.*)) (((float.< input.in0) float.neg)))
        // (float.% float.neg 8.789999 input.in1 float.% float.% input.inallrev float.+ float.>)
        // (input.inall input.stackdepth float./ float.% input.index float.- float.<)
        final Program program = new Program(interpreter, "(input.inall ((input.inall) (float./ float.%)) input.inall float.+ (float.> float./ -7.9300003 float.neg input.inall (float.neg input.inall)))");

        add(drawPanel);
        pack();
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLocationRelativeTo(null);
        setVisible(true);
        Thread thread = new Thread(() -> {
            while (true) {
                interpreter.ClearStacks();
                final FloatStack floatStack = interpreter.floatStack();
                final ObjectStack inputStack = interpreter.inputStack();
                final BooleanStack booleanStack = interpreter.boolStack();
                floatStack.push(cartPosition);
                floatStack.push(cartVelocity);
                inputStack.push(cartPosition);
                inputStack.push(cartVelocity);
                interpreter.Execute(program, 150);
                positiveForce = booleanStack.pop();
                cartVelocity += (positiveForce ? 1 : -1) * cartAccelerationAbs;
                cartPosition += cartVelocity / 100;
                drawPanel.repaint();
                try {
                    Thread.sleep(20);
                } catch (Exception ignored) {

                }
                time++;
            }
        });
        thread.start();
    }

    private class DrawPanel extends JPanel {

        protected void paintComponent(Graphics g) {
            super.paintComponent(g);
            g.setColor(Color.BLACK);
            g.fillRect((int) (cartPosition * 1000) + 1000, y, CART_WIDTH, CART_HEIGHT);
            g.setColor(Color.GRAY);
            g.fillRect(0, y + CART_HEIGHT, D_W, 10);
            g.fillPolygon(new int[]{75, positiveForce ? 100 : 50, 75}, new int[]{25, 50, 75}, 3);
        }

        public Dimension getPreferredSize() {
            return new Dimension(D_W, D_H);
        }
    }

    public static void main(String[] args) {
        EventQueue.invokeLater(RectanglesDrawingExample::new);
    }
}