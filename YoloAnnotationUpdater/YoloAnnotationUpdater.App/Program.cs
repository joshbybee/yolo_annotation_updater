using System;
using CommandLine;
using YoloAnnotationUpdater.Lib;

namespace YoloAnnotationUpdater.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<YoloOptions>(args)
                .WithParsed(x =>
                {
                    var converter = new YoloConverter(x);
                    converter.Run();
                });
        }
    }
}
