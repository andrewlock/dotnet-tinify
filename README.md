dotnet-tinify
============

[![AppVeyor build status][appveyor-badge]](https://ci.appveyor.com/project/andrewlock/dotnet-tinify/branch/master)

[appveyor-badge]: https://img.shields.io/appveyor/ci/andrewlock/dotnet-tinify/master.svg?label=appveyor&style=flat-square

[![NuGet][main-nuget-badge]][main-nuget] [![MyGet][main-myget-badge]][main-myget]

[main-nuget]: https://www.nuget.org/packages/dotnet-tinify/
[main-nuget-badge]: https://img.shields.io/nuget/v/dotnet-tinify.svg?style=flat-square&label=nuget
[main-myget]: https://www.myget.org/feed/andrewlock-ci/package/nuget/dotnet-tinify
[main-myget-badge]: https://img.shields.io/www.myget/andrewlock-ci/vpre/dotnet-tinify.svg?style=flat-square&label=myget

A simple tool for squashing PNG and JPEG files using the [TinyPNG API](https://tinypng.com/).

Can compress all the PNG or JPEG files in a directory or just single files.

## Installation

The latest release of dotnet-tinify requires the [2.1.300](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.300) .NET Core 2.1 SDK or newer.
Once installed, run this command:

```
dotnet tool install --global dotnet-tinify
```

## Usage

```
Usage: dotnet tinify [arguments] [options]

Arguments:
  path  Path to the file or directory to squash

Options:
  -?|-h|--help            Show help information
  -a|--api-key <API_KEY>  Your TinyPNG API key

You must provide your TinyPNG API key to use this tool
(see https://tinypng.com/developers for details). This
can be provided either as an argument, or by setting the
TINYPNG_APIKEY environment variable.
Only png, jpeg, and jpg, extensions are supported.
```
