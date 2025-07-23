// See https://aka.ms/new-console-template for more information

using System.Drawing.Imaging;
using WindowsFontsSandbox;

Console.WriteLine("Hello, World!");

string fileName = "text_output.png";
string fullPath = Path.Combine(@"C:\image", fileName);

var image = new Drawer().DrawText();

// Save the image
image.Save(fullPath, ImageFormat.Png);

// Print the full path
Console.WriteLine($"Image saved to: {fullPath}");
await Task.Delay(Timeout.Infinite);
