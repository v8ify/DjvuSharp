using NUnit.Framework;
using System;
using DjvuSharp.Enums;

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
        using var document = DjvuDocument.Create(filename);
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
        using var document = DjvuDocument.Create(filename);

        Assert.AreEqual(pages, document.PageNumber);
    }

    const string DocumentDump =
@"  FORM:DJVM [11465] 
    DIRM [30]         Document directory (bundled, 2 files 2 pages)
    FORM:DJVU [3159] {p0} [P1]
      INFO [10]         DjVu 192x256, v24, 100 dpi, gamma=2.2, orientation=0
      BG44 [3129]       IW4 data #1, 90 slices, v1.2 (color), 192x256
    FORM:DJVU [8247] {p1} [P2]
      INFO [10]         DjVu 181x240, v24, 100 dpi, gamma=2.2, orientation=0
      BG44 [8217]       IW4 data #1, 90 slices, v1.2 (color), 181x240
";

    /*[TestCase("./assets/boy_and_chicken.djvu")]
    public void Test_If_Document_GetDump_Return_Correct_Output(string filename)
    {
        using var document = DjvuDocument.Create(filename);

        string textDump = document.GetDump(false);

        Assert.AreEqual(DocumentDump, textDump);
    }*/

    const string DocumentDumpJson =
@"{ ""$type"":""DjvuNet.Serialization.DjvuDoc"", ""DjvuData"":
    { ""$type"": ""DjvuNet.Serialization.DjvmForm"", ""ID"": ""FORM:DJVM"", ""NodeOffset"": 0, ""Size"": 11465, ""Children"": [
            { ""$type"": ""DjvuNet.Serialization.Dirm"", ""ID"": ""DIRM"", ""NodeOffset"": 16, ""Size"": 30, ""Description"": ""Document directory"", ""DocumentType"": ""bundled"", ""FileCount"": 2, ""PageCount"": 2 },
            { ""$type"": ""DjvuNet.Serialization.DjvuForm"", ""ID"": ""FORM:DJVU"", ""NodeOffset"": 54, ""Size"": 3159, ""Children"": [
                    { ""$type"": ""DjvuNet.Serialization.Info"", ""ID"": ""INFO"", ""NodeOffset"": 66, ""Size"": 10, ""Width"": 192, ""Height"": 256, ""Version"": 24, ""Dpi"": 100, ""Gamma"": 2.2, ""Orientation"": 0 },
                    { ""$type"": ""DjvuNet.Serialization.BG44"", ""ID"": ""BG44"", ""NodeOffset"": 84, ""Size"": 3129, ""Description"": ""IW4 data #1"", ""Slices"": 90, ""Version"": 1.2, ""Color"": ""True"", ""Width"": 192, ""Height"": 256 }
                ]
            },
            { ""$type"": ""DjvuNet.Serialization.DjvuForm"", ""ID"": ""FORM:DJVU"", ""NodeOffset"": 3222, ""Size"": 8247, ""Children"": [
                    { ""$type"": ""DjvuNet.Serialization.Info"", ""ID"": ""INFO"", ""NodeOffset"": 3234, ""Size"": 10, ""Width"": 181, ""Height"": 240, ""Version"": 24, ""Dpi"": 100, ""Gamma"": 2.2, ""Orientation"": 0 },
                    { ""$type"": ""DjvuNet.Serialization.BG44"", ""ID"": ""BG44"", ""NodeOffset"": 3252, ""Size"": 8217, ""Description"": ""IW4 data #1"", ""Slices"": 90, ""Version"": 1.2, ""Color"": ""True"", ""Width"": 181, ""Height"": 240 }
                ]
            }
        ]
    }
}
";
    /*[TestCase("./assets/boy_and_chicken.djvu")]
    public void Test_If_Document_GetDump_Return_Correct_Output_JSON(string filename)
    {
        using var document = DjvuDocument.Create(filename);

        string jsonDump = document.GetDump(true);

        Assert.AreEqual(DocumentDumpJson, jsonDump);
    }*/

    /// <summary>
    /// Testing the Document.GetFileNumber() method if it returns the correct number of files
    /// in a document.
    /// </summary>
    /// <param name="filePath">The file path to the djvu documents which we are testing</param>
    /// <param name="fileNum">The number of files in the document </param>
    /// <exception cref="Exception">Raised if an error occures while decoding the document</exception>
    [TestCase("./assets/boy_and_chicken.djvu", 2)]
    [TestCase("./assets/DjVu3Spec.djvu", 75)]
    public void Test_Document_Get_FileNum(string filePath, int fileNum)
    {
        using var document = DjvuDocument.Create(filePath);

        Assert.AreEqual(document.FileNumber, fileNum);
    }
}
