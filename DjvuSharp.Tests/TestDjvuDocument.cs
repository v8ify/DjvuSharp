using NUnit.Framework;
using Djvulibre.Internal;


namespace DjvuSharp.Tests;

public class TestDjvuDocument
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("./assets/boy_and_chicken.djvu")]
    [TestCase("./assets/Djvu3Spec.djvu")]
    public void Test_OpeningAndClosingDocument(string filename)
    {
        using (var context = new DjvuContext("NunitTest"))
        {
            using (var document = context.CreateDjvuDocument(filename, true))
            {
                 
            }
        }
    }

    [TestCase("./assets/boy_and_chicken.djvu", 2)]
    [TestCase("./assets/Djvu3Spec.djvu", 71)]
    public void Test_DocumentPageNumber(string filename, int pages)
    {
        using (var context = new DjvuContext("NunitTest"))
        {
            using (var document = context.CreateDjvuDocument(filename, true))
            {
                context.WaitMessage();

                ddjvu_message_s msg;

                while ((msg = context.PeekMessage()) != null)
                {
                    if (msg.m_any.tag == ddjvu_message_tag_t.DDJVU_DOCINFO)
                    {
                        int actualPages = document.GetPageNumber();

                        Assert.AreEqual(pages, actualPages);
                    }

                    context.PopMessage();
                }
            }
        }
    }
}