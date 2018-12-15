using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordAnalysis.ConsoleApplication
{
    public class Counter<T>
    {
        public T Key { get; set; }
        public int Count { get; set; }
        public string AsString { get; set; }
        public Counter(T key, int cnt)
        {
            Key = key;
            Count = cnt;
            AsString = key.ToString();
        }
    }


    public class Output
    {
        public static IList<Counter<int>> OutputWordLengths(int[] wordLengthFrequency)
        {
            Console.WriteLine();
            Console.WriteLine("== Word Lengths ==");

            var orderedWordLengths = 
                Enumerable.Range(0, wordLengthFrequency.Length)
                .Zip(wordLengthFrequency,(l, r) => new Counter<int>(l, r))
                .Where(c=>c.Count > 0)
                .OrderByDescending(c => c.Count)
                .ToList();

            foreach (var length in orderedWordLengths)
                Console.WriteLine($"{length.Key,2:N0} {length.Count:N0}");

            return orderedWordLengths;
        }

        public static IList<Counter<char>> OutputLetterFrequency(int[] letterFrequency, int totalLetters)
        {
            Console.WriteLine();
            Console.WriteLine("== Letter Frequency ==");

            var orderedLetterFrequency =
                Analysis.Letters
                    .Zip(letterFrequency.Skip('a'), (l, r) => new Counter<char>(l, r))
                    .OrderByDescending(c => c.Count)
                    .ToList();

            foreach (var length in orderedLetterFrequency)
            {
                var percentage = ((double)length.Count / (double)totalLetters) * 100;

                Console.WriteLine($"{length.Key} {length.Count:N0} ({percentage:N1}%)");
            }

            return orderedLetterFrequency;
        }

        public static void OutputLetterStartsWith(List<Letter> letters)
        {
            Console.WriteLine();
            Console.WriteLine("== Starting With ==");

            foreach (var letter in letters.OrderByDescending(x => x.StartingWith))
                Console.WriteLine($"{letter.Value} {letter.StartingWith.ToString("N0")} {letter.StartingWithPercentage.ToString("N1")}%");
        }

        public static void OutputLetterEndsWith(List<Letter> letters)
        {
            Console.WriteLine();
            Console.WriteLine("== Ending With ==");

            foreach (var letter in letters.OrderByDescending(x => x.EndingWith))
                Console.WriteLine($"{letter.Value} {letter.EndingWith.ToString("N0")} {letter.EndingWithPercentage.ToString("N1")}%");
        }

        public static void OutputDoubleLetterFrequency(List<Letter> letters)
        {
            Console.WriteLine();
            Console.WriteLine("== Double Letters ==");

            foreach (var letter in letters.OrderByDescending(x => x.DoubleLetters))
                Console.WriteLine($"{letter.Value}{letter.Value} {letter.DoubleLetters:N0}");
        }

        public static void OutputLongestWord(List<string> words)
        {
            Console.WriteLine("");
            Console.WriteLine("== Longest Word(s) ==");

            var longestWordLength = words.Max(x => x.Length);
            var longestWords = words.Where(x => x.Length == longestWordLength);

            foreach (var word in longestWords)
            {
                Console.WriteLine($"word ({word.Length} letters)");
                Console.WriteLine($"{word}");
            }
        }

        public static void OutputWordsEndingWithIng(List<string> words)
        {
            Console.WriteLine("");
            Console.WriteLine("== Ending with ING ==");

            var count = words.Count(x => x.EndsWith("ing"));
            Console.WriteLine(count);
        }

 }
}
