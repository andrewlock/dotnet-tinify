using Humanizer;
using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinifyAPI;

namespace ImageOptimiser
{
    public class ImageSquasher
    {
        public IEnumerable<string> FilesToSquash { get; }
        public IConsole Console { get; }

        public ImageSquasher(IEnumerable<string> filesToSquash, IConsole console)
        {
            FilesToSquash = filesToSquash;
            Console = console;
        }

        public async Task SquashFiles()
        {
            var compressFileTasks = FilesToSquash
                .Where(file => Constants.SupportedExtensions.Contains(Path.GetExtension(file)))
                .Select(file => CompressFile(file));

            await Task.WhenAll(compressFileTasks);
        }
        
        async Task CompressFile(string file)
        {
            try
            {
                Console.WriteLine($"Compressing {Path.GetFileName(file)}...");

                var originalSizeInBytes = new FileInfo(file).Length;

                var source = Tinify.FromFile(file);
                await source.ToFile(file);

                var newSizeInBytes = new FileInfo(file).Length;
                var percentChange = (newSizeInBytes - originalSizeInBytes) * 100.0 / originalSizeInBytes;

                Console.WriteLine($"Compression complete. {Path.GetFileName(file)} was {originalSizeInBytes.Bytes()}, now {newSizeInBytes.Bytes()} (-{percentChange:0}%)");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"An error occurred compressing {Path.GetFileName(file)}: ");
                Console.WriteLine(ex);
            }
        }
    }
}
