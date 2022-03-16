using DjvuSharp;
using DjvuSharp.Enums;
using DjvuSharp.Rendering;

const string djvuFile = @"C:\Users\prajwalj\Desktop\DjvuSharp\assets\boy_and_chicken.djvu";
const string targetFile = @"C:\Users\prajwalj\Desktop\chicken.ppm";

DjvuDocument document = DjvuDocument.Create(djvuFile);

DjvuPage page = new DjvuPage(document, 1);

Rectangle rect = new Rectangle(0, 0, page.Width, page.Height);

byte[] imagePixels = page.Render(RenderMode.COLOR, rect, rect, new PixelFormatGrey());

using (BinaryWriter bw = new BinaryWriter(File.Open(targetFile, FileMode.OpenOrCreate)))
{
    foreach (byte i in imagePixels)
    {
        bw.Write(i);
    }
}