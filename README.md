# DjvuSharp : .NET bindings for DjvuLibre library

**DjvuSharp** is a set of .NET bindings for DjvuLibre library and targets .NET standard 2.0

It is useful to decode .djvu files. It can render a djvu page to raw pixel data. It also has features
to access and decode other djvu file data.

## Getting Started
----------------------------------------

The DjvuSharp library targets .NET Standard 2.0, thus it can be used in projects that target .NET Standard 2.0+, .NET Core 2.0+, .NET 5.0+ and possibly others. DjvuSharp includes a pre-compiled native library, which currently supports the following platforms:

- win-64
- linux-64

Support for other platforms is planned.

**Note**: you should make sure that end users on Windows install the [Microsoft Visual C++ Redistributable for Visual Studio 2015, 2017, 2019 and 2022](https://docsmicrosoft.com/en-us/cpp/windows/latest-supported-vc-redist?view=msvc-160#visual-studio-2015-2017-2019-and-2022) for their platform, otherwise they will get an error message stating that `djvulibre-21.dll` could not be loaded because a module was not found.

## Usage

### Examples

The [examples](https://github.com/Prajwal-Jadhav/DjvuSharp/tree/master/examples) folder in the repository contains some examples of how the library can be used to extract pages from document and render them to a raster to raw pixel bytes.

### DjvuSharp library

The first step when using MuPDFCore is to create a `DjvuSharp.DjvuDocument`:

```Csharp
    DjvuDocument document = DjvuDocument.Create("path/to/your/djvu/file.djvu");
```

This object is `IDisposable`, therefore you should always call the `Dispose()` method on it once you are done with it (or, better yet, wrap it in a `using` directive).

From there on, you can work with `DjvuDocument` object directly. Or you can create a `DjvuPage` object.

```Csharp
    // Here we are accessing the second page of the document
    // Since, the page numbering starts from 0 in DjvuSharp
    DjvuPage page = new DjvuPage(document, 1);
```

The `DjvuPage` is the most important object, as it has most of the useful attributes and methods.

## License

DjvuSharp is released under GPL V2. Check the [LICENSE](https://github.com/Prajwal-Jadhav/DjvuSharp/blob/master/LICENSE) file for more details.
