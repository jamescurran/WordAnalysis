using System;
using System.Collections.Generic;
using System.Linq;

namespace WordAnalysis.ConsoleApplication
{
    public class Analysis
    {
        public int[] WordLengthFrequency { get; } = new int[100];
        public int TotalLetters { get; private set; } = 0;
        public int[] LetterFrequency { get; } = new int[127];
        public List<string> Words { get; }

        public List<Letter> Letters { get; private set; } = new List<Letter>();

        private  const string _lowercaseLetters = "abcdefghijklmnopqrstuvwxyz";

        public Analysis(List<string> words)
        {
            Words = words;
        }

        private readonly Dictionary<char, Letter> _letterByChar = new Dictionary<char, Letter>();

        public void AnalyseWords()
        {
            foreach (string word in Words)
            {
                var wordTrimmedLength = word.Length;

                // Update Word Length Frequency
                WordLengthFrequency[wordTrimmedLength]++;

                // Update Letter Frequency
                foreach (var lowercaseLetter in word)
                {
                    if (Char.IsLower(lowercaseLetter))
                        LetterFrequency[lowercaseLetter]++;
                }

                TotalLetters += wordTrimmedLength;
            }


            Letters = new List<Letter>();

            foreach (var letter in _lowercaseLetters)
            {
                var startingWith = StartingWithCount(letter);
                var endingWith = EndingWithCount(letter);
                var newLetter = new Letter()
                {
                    Value = letter,
                    StartingWith = startingWith,
                    StartingWithPercentage = Percentage(startingWith),
                    EndingWith = endingWith,
                    EndingWithPercentage = Percentage(endingWith),
                };
                Letters.Add(newLetter);
                _letterByChar.Add(letter, newLetter);
            }

            FindDoubleLetters();
        }

        internal List<Counter<char>> GetOrderedLetterFrequency()
        {
            return _lowercaseLetters
                .Zip(LetterFrequency.Skip('a'), (l, r) => new Counter<char>(l, r))
                .OrderByDescending(c => c.Count)
                .ToList();
        }

        public IEnumerable<string> FindWords(string letters)
        {
            var matchingWords = new List<string>();

            Console.WriteLine();
            Console.WriteLine($"== Solving ({letters.ToUpper()}) ==");

            foreach (var word in Words)
            {
                var match = true;

                if (word.Length != letters.Length)
                {
                    continue;
                }

                foreach (var letter in letters)
                {
                    if (word.Contains(letter))
                        continue;
                    match = false;
                    break;
                }

                if (!match)
                    continue;
                matchingWords.Add(word.ToUpper());
                Console.WriteLine(word.ToUpper());
            }

            return matchingWords;
        }

        private  double Percentage(double  startingWithCount)
        {
            return (startingWithCount / Words.Count) * 100;
        }

         int StartingWithCount(char letter)
        {
            return Words.Count(x => x[0] == letter);
        }

        int EndingWithCount(char  letter)
        {
            return Words.Count(x => x[x.Length-1] == letter);
        }

        private void FindDoubleLetters()
        {
            foreach(string word in Words)
            foreach (char l in DoubledLetters(word))
            {
                _letterByChar[l].DoubleLetters++;
            }
        }

        static IEnumerable<char> DoubledLetters(string word)
        {
            var lastLetter = 0;

            foreach (char l in word)
            {
                if (Char.IsLetter(l) && l == lastLetter)
                {
                    yield return l;
                    lastLetter = 0;     // don't count triple letters
                }
                else
                    lastLetter = l;
            }
        }
    }
}
