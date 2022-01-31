using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using NUnit.Framework;
using DjvuSharp.Enums;

namespace DjvuSharp.Tests
{
    [TestFixture]
    public class TestDjvuPage
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

        [Test]
        public void Test_Document_Property()
        {
            Assert.IsNotNull(boyAndChickenPage_0.Document);
        }


        const string boyAndChickenPageInfo = @"
        [
            { ""Height"": 256, ""Width"": 192, ""Gamma"": 2.2, ""DPI"": 100 },
            { ""Height"": 240, ""Width"": 181, ""Gamma"": 2.2, ""DPI"": 100 }
        ]
";

        private class boyAndChickenPage
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public double Gamma { get; set; }
            public int DPI { get; set; }
        }

        [Test]
        public void Test_Page_Height()
        {
            Assert.AreEqual(256, boyAndChickenPage_0.Height);
            Assert.AreEqual(240, boyAndChickenPage_1.Height);

            Assert.AreEqual(3295, djvuSpecPage_0.Height);
            Assert.AreEqual(3295, djvuSpecPage_0.Height);
        }

        [Test]
        public void Test_Boy_Page_Width()
        {
            Assert.AreEqual(192, boyAndChickenPage_0.Width);
            Assert.AreEqual(181, boyAndChickenPage_1.Width);

            Assert.AreEqual(2539, djvuSpecPage_0.Width);
            Assert.AreEqual(2539, djvuSpecPage_0.Width);
        }

        [Test]
        public void Test_Page_Gamma_Value()
        {
            Assert.AreEqual(2.2, boyAndChickenPage_0.Gamma);
            Assert.AreEqual(2.2, boyAndChickenPage_1.Gamma);

            Assert.AreEqual(2.2, djvuSpecPage_0.Gamma);
            Assert.AreEqual(2.2, djvuSpecPage_0.Gamma);
        }

        [Test]
        public void Test_Page_Resolution()
        {
            Assert.AreEqual(100, boyAndChickenPage_0.Resolution);
            Assert.AreEqual(100, boyAndChickenPage_1.Resolution);

            Assert.AreEqual(300, djvuSpecPage_0.Resolution);
            Assert.AreEqual(300, djvuSpecPage_0.Resolution);
        }


        [Test]
        public void Test_Page_Version()
        {
            Assert.AreEqual(24, boyAndChickenPage_0.Version);
            Assert.AreEqual(24, boyAndChickenPage_1.Version);

            Assert.AreEqual(25, djvuSpecPage_0.Version);
            Assert.AreEqual(25, djvuSpecPage_0.Version);
        }

        [Test]
        public void Test_Page_Type()
        {
            Assert.AreEqual(PageType.PHOTO, boyAndChickenPage_0.Type);
            Assert.AreEqual(PageType.PHOTO, boyAndChickenPage_1.Type);

            Assert.AreEqual(PageType.BITONAL, djvuSpecPage_0.Type);
            Assert.AreEqual(PageType.BITONAL, djvuSpecPage_0.Type);
        }
    }
}
