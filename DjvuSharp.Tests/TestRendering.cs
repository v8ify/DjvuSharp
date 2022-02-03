using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DjvuSharp.Renderer;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace DjvuSharp.Tests
{
    [TestFixture]
    public class TestRendering
    {
        private DjvuDocument boyAndChicken;
        private DjvuDocument djvuSpec;

        private DjvuPage boyAndChickenPage_0;
        private DjvuPage boyAndChickenPage_1;

        private DjvuPage djvuSpecPage_0;
        private DjvuPage djvuSpecPage_70;

        [OneTimeSetUp]
        public void Setup()
        {
            boyAndChicken = DjvuDocument.Create("./assets/boy_and_chicken.djvu");
            djvuSpec = DjvuDocument.Create("./assets/DjVu3Spec.djvu");

            boyAndChickenPage_0 = new DjvuPage(boyAndChicken, 0);
            boyAndChickenPage_1 = new DjvuPage(boyAndChicken, 1);

            djvuSpecPage_0 = new DjvuPage(djvuSpec, 0);
            djvuSpecPage_70 = new DjvuPage(djvuSpec, 70);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            boyAndChicken.Dispose();
            djvuSpec.Dispose();
        }

        /*[Test]
        public void Test_Page_Render()
        {
            using var renderer = RenderEngineFactory.CreateRenderEngine(Enums.PixelFormatStyle.RGB24).SetRowOrder(true);

            Rectangle pageRect = new Rectangle(0, 0, boyAndChickenPage_1.Width, boyAndChickenPage_1.Height);
            Rectangle renderRect = new Rectangle(0, 0, boyAndChickenPage_1.Width, boyAndChickenPage_1.Height);

            var image = renderer.RenderPage(boyAndChickenPage_1, Enums.RenderMode.COLOR, pageRect, renderRect);

            sbyte[] actualBuffer;

            using (var br = new BinaryReader(File.OpenRead("./assets/boy_and_chicken_p1.ppm")))
            {
                var buffer = new List<sbyte>();
                while(br.BaseStream.Position != br.BaseStream.Length)
                {
                    buffer.Add(br.ReadSByte());
                }

                actualBuffer = buffer.ToArray();
            }

            Assert.AreEqual(image, actualBuffer);
        }*/
    }
}
