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
    private static final int D_W = 1500;
    private static final int D_H = 200;
    private static final int CART_WIDTH = 50;
    private static final int CART_HEIGHT = 50;

    private final Interpreter interpreter;

    private float cartPosition = 0.9f;
    private float cartVelocity = 1.0f;

    private final float cartAccelerationAbs = 0.5f;

    private boolean positiveForce;

    int time = 0;

    int y = 100;

    DrawPanel drawPanel = new DrawPanel();

    @SneakyThrows
    public RectanglesDrawingExample() {
        interpreter = new Interpreter();
        interpreter.SetRandomParameters(-10, 10, 1, -10, 10, 0.01f, 40, 100);
        // (float.- (float.- (float.- input.in1 ((input.inallrev)) float.neg) ((input.inallrev)) float.neg) input.in1 (((((input.inallrev) (-1.4300003)))) (float.*)) (((float.< input.in0) float.neg)))
        // (float.% float.neg 8.789999 input.in1 float.% float.% input.inallrev float.+ float.>)
        // (input.inall input.stackdepth float./ float.% input.index float.- float.<)
        final Program program = new Program(interpreter, "((float.neg) float.shove (((((((integer.rand) boolean.shove integer.rot) integer.min) exec.s (exec.do*times) ((float.sin) (code.shove))) integer.swap)) ((code.do*times) (((boolean.shove (code.shove))) float.< name.dup) integer.min integer.=)) code.do*times)");

        add(drawPanel);
        pack();
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLocationRelativeTo(null);
        setVisible(true);
        Thread thread = new Thread(() -> {
            while (true) {
                interpreter.clearStacks();
                final FloatStack floatStack = interpreter.floatStack();
                final ObjectStack inputStack = interpreter.inputStack();
                floatStack.push(cartPosition);
                floatStack.push(cartVelocity);
                inputStack.push(cartPosition);
                inputStack.push(cartVelocity);
                interpreter.Execute(program, 150);
                final BooleanStack booleanStack = interpreter.boolStack();
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
            g.fillRect((int) (cartPosition * 500) + 750, y, CART_WIDTH, CART_HEIGHT);
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