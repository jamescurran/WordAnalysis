﻿using System;
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
        static SortedDictionary<int, int> _wordLengthFrequency = new SortedDictionary<int, int>();
        static SortedDictionary<string, int> _letterFrequency = new SortedDictionary<string, int>();
        static SortedDictionary<string, int> _doubleLetterFrequency = new SortedDictionary<string, int>();
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
            Output.OutputWordLengths(_wordLengthFrequency);
            Output.OutputLongestWord(_words);
            Output.OutputLetterFrequency(_letterFrequency, _totalLetters);
            Output.OutputDoubleLetterFrequency(_letters);
            Output.OutputWordsEndingWithIng(_words);
            Console.WriteLine($"Elapsed time: {sw.Elapsed}");

            ChartBuilder.SaveLetterFrequencyChart(_letterFrequency);
            ChartBuilder.SaveLetterStartingWithChart(_letters);
            ChartBuilder.SaveLetterEndingWithChart(_letters);
            ChartBuilder.SaveWordLengthFrequencyChart(_wordLengthFrequency);
            ChartBuilder.SaveDoubleLetterFrequencyChart(_doubleLetterFrequency);

            Analysis.FindWords("barnslieu", _words);

        }

        static void ReadFile()
        {
            foreach (string word in File.ReadLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/words.txt")))
            {
                var wordTrimmedLength = word.Trim().Length;

                // Update Word Length Frequency
                if (_wordLengthFrequency.ContainsKey(wordTrimmedLength))
                {
                    _wordLengthFrequency[wordTrimmedLength]++;
                }
                else
                {
                    _wordLengthFrequency.Add(wordTrimmedLength, 1);
                }

                // Update Letter Frequency
                foreach (var letter in word)
                {
                    var lowercaseLetter = letter.ToString().ToLower();

                    if (_letterFrequency.ContainsKey(lowercaseLetter))
                    {
                        _letterFrequency[lowercaseLetter]++;
                    }
                    else
                    {
                        if (Analysis.GetLetters().Contains(lowercaseLetter))
                            _letterFrequency.Add(lowercaseLetter, 1);
                    }
                }

                // Update Double Letter Frequency
                foreach (var letter in Analysis.GetLetters())
                {
                    if (word.Contains(letter + letter))
                    {
                        if (_doubleLetterFrequency.ContainsKey(letter + letter))
                        {
                            _doubleLetterFrequency[letter + letter]++;
                        }
                        else
                        {
                            _doubleLetterFrequency.Add(letter + letter, 1);
                        }
                    }
                }

                _totalLetters += word.Length;

                _words.Add(word.Trim().ToLower());
            }

            _totalWords = _words.Count();

            Console.WriteLine();
            Console.WriteLine($"Total words: {_totalWords.ToString("N0")}");
            Console.WriteLine($"Total letters: {_totalLetters.ToString("N0")}");
        }
    }
}
