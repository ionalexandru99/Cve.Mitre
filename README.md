# Cve.Mitre [![.NET](https://github.com/ionalexandru99/Cve.Mitre/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ionalexandru99/Cve.Mitre/actions/workflows/dotnet.yml) [![Release](https://github.com/ionalexandru99/Cve.Mitre/actions/workflows/release.yml/badge.svg)](https://github.com/ionalexandru99/Cve.Mitre/actions/workflows/release.yml) ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Cve.Mitre) ![Nuget](https://img.shields.io/nuget/dt/Cve.Mitre)

This library is meant to be used as a means of extracting data from the 'cve.mitre.org' website and converting the HTML code into a format that is easier to work it inside .Net applications.

## How to install the library

To add this library to your project, simply add a reference to your nuget package

## How to use it

The main entry point in the library is the class ```CveProcessor``` which implements the interface ```ICveProcessor```.
For creating the class you will be required to utilize an instance of the ```SearchResultsParser``` class, ```VulnerabilityParser``` class, and ```CveHttpClient``` class. 

A quick way of initialising the ```CveProcessor``` is as follows
```cs
IParser<SearchResult> searchResultParser = new SearchResultParser();
IParser<Vulnerability> vulnerabilityParser = new VulnerabilityParser();
ICveHttpClient cveHttpClient = new CveHttpClient();
ICveProcessor cveProcessor = new CveProcessor(searchResultParser, vulnerabilityParser, cveHttpClient);
```

Now you can use the instance of the class to make request to 'cve.mitre.org' and search for any vulnerability.

### Using dependency injection

In most of the cases you will want to use the library in a way that is more decoupled.

To add the library to the Asp.Net framework just add the following lines of code in the ```Program.cs``` file (The version of the Asp.Net framework used in this documentation is the one from .Net 6)

```cs
builder.Services.AddTransient<IParser<SearchResult>, SearchResultParser>();
builder.Services.AddTransient<IParser<Vulnerability>, VulnerabilityParser>();
builder.Services.AddTransient<ICveHttpClient, CveHttpClient>();
builder.Services.AddTransient<ICveProcessor, CveProcessor>();
```

All that needs to be done next is to use the ```ICveProcessor``` interface in the implementation which require the library.
```cs
private readonly ICveProcessor _cveProcessor;

public ClassContructor(ICveProcessor cveProcessor)
{
    _cveProcessor = cveProcessor;
}
```

## Contribute

If you see any problems in the library or encounter any issues, let me know by creating a bug or an issue to the repository. Any new ideas are welcome for extending this project and making it better.

Thanks!
