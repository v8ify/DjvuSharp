using NUnit.Framework;

namespace DjvuSharp.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test_DjvuVersion()
    {
        var version = Djvu.GetDjvuVersion();

        Assert.AreEqual("DjVuLibre-3.5.27", version);
    }

    [TestCase(1u)]
    [TestCase(50u)]
    [TestCase(100000u)]
    public void Test_DjvuContextCacheProperty(uint size)
    {
        using (var context = new DjvuContext("NUnit Test"))
        {
            context.CacheSize = size;

            Assert.AreEqual(size, context.CacheSize);
        }
    }
}