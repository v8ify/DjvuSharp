using DjvuSharp;
using DjvuSharp.Enums;
using DjvuSharp.Rendering;

const string djvuFile = @"C:\Users\prajwalj\Desktop\DjvuSharp\assets\boy_and_chicken.djvu";
const string targetFile = @"C:\Users\prajwalj\Desktop\chicken_thumbnail.ppm";

DjvuDocument document = DjvuDocument.Create(djvuFile);

DjvuPage page = new DjvuPage(document, 1);

using (var renderEngine = RenderEngineFactory.CreateRenderEngine(PixelFormatStyle.RGB24))
{
    renderEngine.SetRowOrder(true);

    Thumbnail thumbnail = new Thumbnail(document, 1);

    int width = 50;
    int height = 50;

    byte[] imagePixels = renderEngine.RenderPageThumbnail(thumbnail, ref width, ref height);

    using (BinaryWriter bw = new BinaryWriter(File.Open(targetFile, FileMode.OpenOrCreate)))
    {
        foreach (byte i in imagePixels)
        {
            bw.Write(i);
        }
    }

    Console.WriteLine($"Width: {width}");
    Console.WriteLine($"Height: {height}");
}
