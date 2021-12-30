using NUnit.Framework;
using Djvulibre.Internal;
using DjvuSharp.Message;
using System;

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

    /// <summary>
    /// Tests if functions return correct page numbers in djvu document.
    /// </summary>
    /// <param name="filename">The path to djvu file</param>
    /// <param name="pages">No. of pages.</param>
    /// 
    [TestCase("./assets/boy_and_chicken.djvu", 2)]
    [TestCase("./assets/Djvu3Spec.djvu", 71)]
    public void Test_DocumentPageNumber(string filename, int pages)
    {
        using (var context = new DjvuContext("NunitTest"))
        {
            using (var document = context.CreateDjvuDocument(filename, true))
            {
                context.WaitMessage();

                DDjvuMessage msg;

                while ((msg = context.PeekMessage()) != null)
                {
                    if (msg.MessageAny.Tag == MessageTag.DDJVU_DOCINFO)
                    {
                        int actualPages = document.GetPageNumber();

                        Assert.AreEqual(pages, actualPages);
                    }

                    context.PopMessage();
                }
            }
        }
    }


    /// <summary>
    /// Tests the capability of job IsDone() function.
    /// </summary>
    /// <param name="filename">The path to djvu file</param>
    /// <param name="type">Type of the document.</param>
    /// 
    [TestCase("./assets/boy_and_chicken.djvu", 2)]
    [TestCase("./assets/DjVu3Spec.djvu", 2)]
    public void TestDocumentDoneMethod(string filename, int type)
    {
        using (var context = new DjvuContext("NunitTest"))
        {
            using (var document = context.CreateDjvuDocument(filename, true))
            {
                while (!document.IsDecodingDone())
                {
                    DDjvuMessage message = context.WaitMessage();

                    while ((message = context.PeekMessage()) != null)
                    {
                        switch (message.MessageAny.Tag)
                        {
                            case MessageTag.DDJVU_ERROR:
                                TestContext.Out.WriteLine($"ddjvu: {message.M_Error.message}");

                                if (message.M_Error.filename != null)
                                    TestContext.Out.WriteLine($"ddjvu: {message.M_Error.filename}");

                                throw new Exception(message.M_Error.message);
                        }

                        context.PopMessage();
                    }
                }

                int actualType = (int)document.GetDocumentType();

                Assert.AreEqual(type, actualType);
            }
        }
    }
}