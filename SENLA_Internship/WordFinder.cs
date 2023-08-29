using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENLA_Internship {
    // find all occurences of a word in a sentence
    internal class WordFinder {
        // reads a string (sentence or word)
        string StringInput() {
            return Console.ReadLine().Trim(' ').ToLower();
        }
        // z-function linear algorithm
        int[] ZFunc(string str) {
            int sz = str.Length;
            int[] zFunc = new int[sz];

            for (int i = 1, l = 0, r = 0; i < sz; i++) {
                if (i <= r) zFunc[i] = Math.Min(zFunc[i - l], r - i + 1);

                while (i + zFunc[i] < sz && str[zFunc[i]] == str[i + zFunc[i]]) zFunc[i]++;

                if (i + zFunc[i] - 1 > r) {
                    l = i;
                    r = i + zFunc[i] - 1;
                }
            }

            return zFunc;
        }
        // finds all occurence of pattern in text
        List<int> FindAll(string pattern, string text) {
            // result list
            List<int> matchIndices = new List<int>();

            // pattern$text
            string patternSText = pattern + "$" + text;

            // z-funtion of pattern$text
            int[] zFunc = ZFunc(patternSText);

            // if zFunc[i] == pattern.Length then
            // i is index of first symbol of pattern in text
            for (int i = pattern.Length + 1; i < patternSText.Length; i++)
                if (zFunc[i] == pattern.Length)
                    matchIndices.Add(i - pattern.Length - 1);

            return matchIndices;
        }
        // main function
        public void Run() {
            Console.Write("Input a sentence: ");

            // input sentence
            string sentence = StringInput();

            Console.Write("Input a word: ");

            // input word
            string word = StringInput();

            // all matches
            List<int> matchIndices = FindAll(word, sentence);
            int matchesCount = matchIndices.Count;

            Console.WriteLine($"Count of matches: {matchesCount}");
            
            Console.Write("Match indices: ");
            foreach (int index in matchIndices)
                Console.Write($"{index} ");
        }
    }
}
