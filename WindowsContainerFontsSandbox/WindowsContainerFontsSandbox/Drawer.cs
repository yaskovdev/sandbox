using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace WindowsFontsSandbox;

public class Drawer
{
    public Bitmap DrawText()
    {
        // Create a bitmap to draw on
        var bitmap = new Bitmap(800, 100);
        using (var graphics = Graphics.FromImage(bitmap))
        {
            graphics.Clear(Color.White);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using (var font = new Font("Segoe UI", 24))
            using (var brush = new SolidBrush(Color.Black))
            {
                graphics.DrawString("The quick brown fox jumps over the lazy dog.", font, brush, 10, 30);
            }

            return bitmap;
        }
    }
}
