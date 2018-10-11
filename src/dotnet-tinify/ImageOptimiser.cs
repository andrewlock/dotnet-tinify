using Humanizer;
using McMaster.Extensions.CommandLineUtils;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TinifyAPI;
using SysException = System.Exception;

namespace ImageOptimiser
{
    public class ImageOptimiser
    {        
        public IConsole Console { get; }

        public ImageOptimiser(IConsole console) => Console = console;

        public async Task OptimiseFileAsync(string[] filesToSquash)
        {
            if (filesToSquash?.Length > 0)
            {
                var compressFileTasks = filesToSquash
                    .Where(file => Constants.SupportedExtensions.Contains(Path.GetExtension(file)))
                    .Select(file => CompressFileAsync(file));

                await Task.WhenAll(compressFileTasks);
            }            
        }
        
        async Task CompressFileAsync(string file)
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
            catch (SysException ex)
            {
                Console.WriteLine($"An error occurred compressing {Path.GetFileName(file)}: ");
                Console.WriteLine(ex);
            }
        }
    }
}
