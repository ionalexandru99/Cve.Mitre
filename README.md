# Cve.Mitre [![.NET](https://github.com/ionalexandru99/Cve.Mitre/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ionalexandru99/Cve.Mitre/actions/workflows/dotnet.yml) [![Release](https://github.com/ionalexandru99/Cve.Mitre/actions/workflows/release.yml/badge.svg)](https://github.com/ionalexandru99/Cve.Mitre/actions/workflows/release.yml) ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Cve.Mitre) ![Nuget](https://img.shields.io/nuget/dt/Cve.Mitre)

This library is meant to be used as a means of extracting data from the 'cve.mitre.org' website and converting the HTML code into a format that is easier to work it inside .Net applications.

## How to install the library

To add this library to your project, simply add a reference to your nuget package

## How to use it

The main entry point in the library is the class ```CveProcessor``` which implements the interface ```ICveProcessor```.
For creating the class you will be required to utilize an instance of the ```SearchResultsParser``` class, ```VulnerabilityParser``` class, and ```CveHttpClient``` class. 
