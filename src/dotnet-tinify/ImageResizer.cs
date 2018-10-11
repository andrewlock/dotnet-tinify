using McMaster.Extensions.CommandLineUtils;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using TinifyAPI;
using SysException = System.Exception;

namespace ImageOptimiser
{
    [Command(
          Name = "dotnet tinify",
          FullName = "dotnet-tinify",
          Description = "Uses the TinyPNG API to squash images",
          ExtendedHelpText = Constants.ExtendedHelpText)]
    [HelpOption]
    public partial class ImageResizer
    {
        [Required(ErrorMessage = "You must specify the path to a directory or file to compress")]
        [Argument(0, Name = "path", Description = "Path to the file or directory to squash")]
        [FileOrDirectoryExists]
        public string Path { get; }

        [Option(CommandOptionType.SingleValue, Description = "Your TinyPNG API key")]
        public string ApiKey { get; }

        public async Task<int> OnExecute(CommandLineApplication app, IConsole console)
        {
            if (!await SetApiKeyAsync(console))
            {
                app.ShowHelp();
                return Program.ERROR;
            }

            var optimiser = new ImageOptimiser(console);
            await optimiser.OptimiseFileAsync(GetFilesToSquash(console, Path));

            console.WriteLine($"Compression complete.");
            console.WriteLine($"{Tinify.CompressionCount} compressions this month");

            return Program.OK;
        }

        async Task<bool> SetApiKeyAsync(IConsole console)
        {
            try
            {
                var apiKey = string.IsNullOrEmpty(ApiKey)
                    ? Environment.GetEnvironmentVariable(Constants.ApiKeyEnvironmentVariable)
                    : ApiKey;

                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    console.Error.WriteLine("Error: No API Key provided");
                    return false;
                }

                Tinify.Key = apiKey;
                await Tinify.Validate();
                console.WriteLine("TinyPng API Key verified");
                return true;
            }
            catch (SysException ex)
            {
                console.Error.WriteLine("Validation of TinyPng API key failed.");
                console.Error.WriteLine(ex);
                return false;
            }
        }

        static string[] GetFilesToSquash(IConsole console, string path)
        {
            console.WriteLine($"Checking '{path}'...");
            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                console.WriteLine($"Path '{path}' is a directory, squashing all files");
                return Directory.GetFiles(path);
            }
            else
            {
                console.WriteLine($"Path '{path}' is a file");
                return new[] { path };
            }
        }
    }
}
