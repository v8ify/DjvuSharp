using NUnit.Framework;
using System.Runtime.InteropServices;

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

    [Test]
    public void Test_Native_Create_Context()
    {
        var context = Native.ddjvu_context_create("NUnit");

        Assert.AreNotEqual(System.IntPtr.Zero, context);
    }

    [Test]
    public void Test_Native_Get_Djvu_Version()
    {
        var pointer = Native.ddjvu_get_version_string();
        var version = Marshal.PtrToStringAnsi(pointer);

        Assert.AreEqual("DjVuLibre-3.5.27", version);
    }

    [TestCase(1ul)]
    [TestCase(50ul)]
    [TestCase(100000ul)]
    public void Test_DjvuContextCacheProperty(ulong size)
    {
        using (var context = new DjvuContext("NUnit Test"))
        {
            context.CacheSize = size;

            Assert.AreEqual(size, context.CacheSize);
        }
    }
}