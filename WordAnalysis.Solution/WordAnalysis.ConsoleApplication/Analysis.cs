using System;
using System.Collections.Generic;
using System.Linq;

namespace WordAnalysis.ConsoleApplication
{
    public class Analysis
    {
        static Dictionary<char, Letter>  LetterByChar = new Dictionary<char, Letter>();
        public static List<Letter> AnalyseWords(List<string> words, int totalWords)
        {
            var letters = new List<Letter>();

            foreach (var letter in Letters)
            {
                var startingWith = StartingWithCount(letter, words);
                var endingWith = EndingWithCount(letter, words);
                var newLetter = new Letter()
                {
                    Value = letter,
                    StartingWith = startingWith,
                    StartingWithPercentage = StartingWithPercentage(startingWith, totalWords, words),
                    EndingWith = endingWith,
                    EndingWithPercentage = EndingWithPercentage(endingWith, totalWords, words),
//                    DoubleLetters = DoubleLetterCount(letter, words)
                };
                letters.Add(newLetter);
                LetterByChar.Add(letter, newLetter);
            }

            FindDoubleLetters(words);
            return letters;
        }



        public static IEnumerable<string> FindWords(string letters, List<string> words)
        {
            var matchingWords = new List<string>();

            Console.WriteLine();
            Console.WriteLine($"== Solving ({letters.ToUpper()}) ==");

            var match = true;

            foreach (var word in words)
            {
                match = true;

                if (word.Length != letters.Length)
                {
                    match = false;
                    continue;
                }

                foreach (var letter in letters)
                {
                    if (!word.Contains(letter))
                    {
                        match = false;
                        continue;
                    }
                }

                if (match)
                {
                    matchingWords.Add(word.ToUpper());
                    Console.WriteLine(word.ToUpper());
                }
            }

            return matchingWords;
        }

        private static double EndingWithPercentage(double endingWith, int totalWords, List<string> words)
        {
            return (endingWith / totalWords) * 100;
        }

        private static double StartingWithPercentage(double  startingWithCount,  int totalWords, List<string> words)
        {
            return (startingWithCount / totalWords) * 100;
        }

        static int StartingWithCount(char letter, List<string> words)
        {
            return words.Count(x => x[0] == letter);
        }

        static int EndingWithCount(char  letter, List<string> words)
        {
            return words.Count(x => x[x.Length-1] == letter);
        }

        static int DoubleLetterCount(char letter, List<string> words)
        {
            var doublel = new string(letter, 2);
            return words.Count(x => x.Contains(doublel));
        }
        private static void FindDoubleLetters(List<string> words)
        {
            foreach(string word in words)
            foreach (char l in DoubledLetters(word))
            {
                LetterByChar[l].DoubleLetters++;
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

        public static string Letters => "abcdefghijklmnopqrstuvwxyz";
    }
}
