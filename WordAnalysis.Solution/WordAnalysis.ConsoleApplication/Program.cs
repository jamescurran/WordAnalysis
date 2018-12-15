using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WordAnalysis.ConsoleApplication
{
    class Program
    {
        static List<string> _words = new List<string>();
        static List<Letter> _letters = new List<Letter>();
        static int[] _wordLengthFrequency = new int[100];
        static int[] _letterFrequency = new int[127];
        //static int[] _doubleLetterFrequency = new int[127];
        static int _totalWords;
        static int _totalLetters;

        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            ReadFile();

            Console.WriteLine($"Elapsed time: {sw.Elapsed}");

            _letters = Analysis.AnalyseWords(_words, _totalWords);
            Console.WriteLine($"Elapsed time: {sw.Elapsed}");

            Output.OutputLetterStartsWith(_letters);
            Output.OutputLetterEndsWith(_letters);
            var orderedWordLengths = Output.OutputWordLengths(_wordLengthFrequency);
            Output.OutputLongestWord(_words);
            var orderedLetterFrequency = Output.OutputLetterFrequency(_letterFrequency, _totalLetters);
            Output.OutputDoubleLetterFrequency(_letters);
            Output.OutputWordsEndingWithIng(_words);
            Console.WriteLine($"Elapsed time: {sw.Elapsed}");

            ChartBuilder.SaveLetterFrequencyChart(orderedLetterFrequency);
            ChartBuilder.SaveLetterStartingWithChart(_letters);
            ChartBuilder.SaveLetterEndingWithChart(_letters);
            ChartBuilder.SaveWordLengthFrequencyChart(orderedWordLengths);
            ChartBuilder.SaveDoubleLetterFrequencyChart(_letters);

            Analysis.FindWords("barnslieu", _words);

        }

        static void ReadFile()
        {
            foreach (string word1 in File.ReadLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/words.txt")))
            {
                var word = word1.Trim().ToLower();
                var wordTrimmedLength = word.Length;

                // Update Word Length Frequency
                 _wordLengthFrequency[wordTrimmedLength]++;

                // Update Letter Frequency
                foreach (var lowercaseLetter in word)
                {
                    if (Char.IsLower(lowercaseLetter))
                        _letterFrequency[lowercaseLetter]++;
                }

                _totalLetters += wordTrimmedLength;

                _words.Add(word);
            }

            _totalWords = _words.Count();

            Console.WriteLine();
            Console.WriteLine($"Total words: {_totalWords:N0}");
            Console.WriteLine($"Total letters: {_totalLetters:N0}");
        }
    }
}
