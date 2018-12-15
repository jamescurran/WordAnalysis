using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WordAnalysis.ConsoleApplication
{
    class Program
    {
        static void Main()
        {
            var sw = new Stopwatch();
            sw.Start();
            var words = ReadFile();
            var analyse = new Analysis(words);

            Console.WriteLine($"Elapsed time: {sw.Elapsed}");

            analyse.AnalyseWords();
            Output.Overview(analyse);

            Console.WriteLine($"Elapsed time: {sw.Elapsed}");

            Output.OutputLetterStartsWith(analyse);
            Output.OutputLetterEndsWith(analyse);
            var orderedWordLengths = Output.OutputWordLengths(analyse.WordLengthFrequency);
            Output.OutputLongestWord(analyse);
            var orderedLetterFrequency = Output.OutputLetterFrequency(analyse);
            Output.OutputDoubleLetterFrequency(analyse);
            Output.OutputWordsEndingWithIng(analyse);
            Console.WriteLine($"Elapsed time: {sw.Elapsed}");

            ChartBuilder.SaveLetterFrequencyChart(orderedLetterFrequency);
            ChartBuilder.SaveLetterStartingWithChart(analyse);
            ChartBuilder.SaveLetterEndingWithChart(analyse);
            ChartBuilder.SaveWordLengthFrequencyChart(orderedWordLengths);
            ChartBuilder.SaveDoubleLetterFrequencyChart(analyse);

            analyse.FindWords("barnslieu");

        }

        static List<string> ReadFile()
        {
            return File.ReadLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/words.txt"))
                .Select(w => w.Trim().ToLower())
                .ToList();
        }
    }
}
