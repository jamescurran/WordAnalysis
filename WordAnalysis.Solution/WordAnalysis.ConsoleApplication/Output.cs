using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAnalysis.ConsoleApplication
{
    public static class Output
    {
        public static void Overview(Analysis analysis)
        {
            Console.WriteLine();
            Console.WriteLine($"Total words: {analysis.Words.Count:N0}");
            Console.WriteLine($"Total letters: {analysis.TotalLetters:N0}");
        }

        public static IList<Counter<int>> OutputWordLengths(int[] wordLengthFrequency)
        {
            Console.WriteLine();
            Console.WriteLine("== Word Lengths ==");

            var orderedWordLengths =
                Enumerable.Range(0, wordLengthFrequency.Length)
                    .Zip(wordLengthFrequency, (l, r) => new Counter<int>(l, r))
                    .Where(c => c.Count > 0)
                    .OrderByDescending(c => c.Count)
                    .ToList();

            foreach (var length in orderedWordLengths)
                Console.WriteLine($"{length.Key,2:N0} {length.Count:N0}");

            return orderedWordLengths;
        }

        public static IList<Counter<char>> OutputLetterFrequency(Analysis analysis)
        {
            Console.WriteLine();
            Console.WriteLine("== Letter Frequency ==");

            var orderedLetterFrequency = analysis.GetOrderedLetterFrequency();


            var totalLetters = (double) analysis.TotalLetters;
            foreach (var length in orderedLetterFrequency)
            {
                var percentage = (length.Count / totalLetters) * 100;

                Console.WriteLine($"{length.Key} {length.Count:N0} ({percentage:N1}%)");
            }

            return orderedLetterFrequency;
        }

        public static void OutputLetterStartsWith(Analysis analysis)
        {
            Console.WriteLine();
            Console.WriteLine("== Starting With ==");

            foreach (var letter in analysis.Letters.OrderByDescending(x => x.StartingWith))
                Console.WriteLine($"{letter.Value} {letter.StartingWith:N0} {letter.StartingWithPercentage:N1}%");
        }

        public static void OutputLetterEndsWith(Analysis analysis)
        {
            Console.WriteLine();
            Console.WriteLine("== Ending With ==");

            foreach (var letter in analysis.Letters.OrderByDescending(x => x.EndingWith))
                Console.WriteLine($"{letter.Value} {letter.EndingWith:N0} {letter.EndingWithPercentage:N1}%");
        }

        public static void OutputDoubleLetterFrequency(Analysis analysis)
        {
            Console.WriteLine();
            Console.WriteLine("== Double Letters ==");

            foreach (var letter in analysis.Letters.OrderByDescending(x => x.DoubleLetters))
                Console.WriteLine($"{letter.Value}{letter.Value} {letter.DoubleLetters:N0}");
        }

        public static void OutputLongestWord(Analysis analyse)
        {
            Console.WriteLine("");
            Console.WriteLine("== Longest Word(s) ==");

            var longestWordLength = analyse.Words.Max(x => x.Length);
            var longestWords = analyse.Words.Where(x => x.Length == longestWordLength);

            foreach (var word in longestWords)
            {
                Console.WriteLine($"word ({word.Length} letters)");
                Console.WriteLine($"{word}");
            }
        }

        public static void OutputWordsEndingWithIng(Analysis analyse)
        {
            Console.WriteLine("");
            Console.WriteLine("== Ending with ING ==");

            var count = analyse.Words.Count(x => x.EndsWith("ing"));
            Console.WriteLine(count);
        }

    }
}
