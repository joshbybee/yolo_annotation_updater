using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace YoloAnnotationUpdater.Lib
{
    public class YoloConverter
    {
        private readonly YoloOptions options;
        private Regex yoloRegex = new Regex(@"\d*\s[\d\s\.]+");
        public YoloConverter(YoloOptions options)
        {
            this.options = options;
        }

        public void Run()
        {
            //make sure the stuff you sent me is actually good
            //put it in some usable things
            //make the changes
            //save dah changes
            var oldClasses = File.ReadAllLines(options.OldClasses);
            var newClasses = File.ReadAllLines(options.NewClasses);
            var conversionDictionary = new Dictionary<int, int>();

            for (var index = 0; index < oldClasses.Length; index++)
            {
                var oldClass = oldClasses[index];
                for (var i = 0; i < newClasses.Length; i++)
                {
                    var newClass = newClasses[i];
                    if (newClass == oldClass)
                        conversionDictionary.Add(index, i);
                }
            }

            var directory = new DirectoryInfo(options.Directory);

            foreach (var file in directory.GetFiles())
            {
                if(file.Extension.ToLower() == ".txt" && file.Name != "classes.txt")
                {
                    var fileLines = File.ReadAllLines(file.FullName);
                    var newFileLines = new List<string>();
                    foreach (var fileLine in fileLines)
                    {
                        var newFileLine = fileLine;
                        if (yoloRegex.IsMatch(fileLine))
                        {
                            var firstDigit = Convert.ToInt32(fileLine.Substring(0, 1));
                            var foundMatch = conversionDictionary.TryGetValue(firstDigit, out var newIndex);
                            if (foundMatch)
                                newFileLine = newIndex + fileLine.Substring(1, fileLine.Length - 1);
                        }
                        newFileLines.Add(newFileLine);
                    }
                    File.WriteAllLines(file.FullName,newFileLines);
                }
            }
        }
    }
}
