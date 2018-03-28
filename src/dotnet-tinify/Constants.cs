using System;
using System.Collections.Generic;

namespace ImageOptimiser
{
    public class Constants
    {
        public const string ApiKeyEnvironmentVariable = "TINYPNG_APIKEY";

        public static ISet<string> SupportedExtensions { get; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".png",
            ".jpeg",
            ".jpg",
        };

        public const string ExtendedHelpText = @"
You must provide your TinyPNG API key to use this tool 
(see https://tinypng.com/developers for details). This 
can be provided either as an argument, or by setting the 
" + ApiKeyEnvironmentVariable + @" environment variable. 
Only png, jpeg, and jpg, extensions are supported
";
    }
}
