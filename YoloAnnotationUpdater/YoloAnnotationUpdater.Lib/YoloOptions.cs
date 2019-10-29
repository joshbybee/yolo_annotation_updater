using CommandLine;

namespace YoloAnnotationUpdater.Lib
{
    public class YoloOptions
    {
        [Option('o', "oldClasses", Required = true, HelpText = "path to file of old classes")]
        public string OldClasses { get; set; }

        [Option('n', "newClasses", Required = true, HelpText = "path to file of new classes")]
        public string NewClasses { get; set; }

        [Option('d', "directory", Required = true, HelpText = "directory to convert")]
        public string Directory { get; set; }

        [Option('s', "includeSubDirs", Required = false, HelpText = "include sub directories in conversion Y/n (default n)")]
        public bool IncludeSubDirectories { get; set; }
    }
}
