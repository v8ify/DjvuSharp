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

        Assert.AreEqual("DjVuLibre-3.5.28", version);
    }
}