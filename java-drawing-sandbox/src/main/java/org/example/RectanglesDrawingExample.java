package org.example;


import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class RectanglesDrawingExample extends JFrame {
    private static final int D_W = 500;
    private static final int D_H = 200;
    private static final int CART_WIDTH = 50;
    private static final int CART_HEIGHT = 50;
    int x = 0;
    int y = 100;

    DrawPanel drawPanel = new DrawPanel();

    public RectanglesDrawingExample() {
        ActionListener listener = new AbstractAction() {
            public void actionPerformed(ActionEvent e) {
                if (x >= D_W) {
                    x = 0;
                    drawPanel.repaint();
                } else {
                    x += 10;
                    drawPanel.repaint();
                }
            }
        };
        Timer timer = new Timer(100, listener);
        timer.start();
        add(drawPanel);

        pack();
        setDefaultCloseOperation(EXIT_ON_CLOSE);
        setLocationRelativeTo(null);
        setVisible(true);
    }

    private class DrawPanel extends JPanel {

        protected void paintComponent(Graphics g) {
            super.paintComponent(g);
            g.setColor(Color.BLACK);
            g.fillRect(x, y, CART_WIDTH, CART_HEIGHT);
            g.setColor(Color.GRAY);
            g.fillRect(0, y + CART_HEIGHT, D_W, 10);
        }

        public Dimension getPreferredSize() {
            return new Dimension(D_W, D_H);
        }
    }

    public static void main(String[] args) {
        EventQueue.invokeLater(RectanglesDrawingExample::new);
    }
}