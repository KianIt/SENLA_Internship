using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENLA_Internship {
    // counts words in a sentence
    internal class WordCounter {
        // reads a sentence
        string SentenceInput() {
            return Console.ReadLine().Trim(' ');
        }
        // splits a sentence into words separated by spaces
        string[] GetWords(string sentence) {
            return sentence.Split(' ');
        }
        // capitalizes each word
        string[] Capitalize(string[] words) {
            for (int i = 0; i < words.Length; i++)
                words[i] = char.ToUpper(words[i][0]) +
                    words[i].Substring(1);
            return words;
        }
        // man function
        public void Run() {
            Console.Write("Input a sentence: "); 

            // input sentence
            string sentence = SentenceInput();

            // single words
            string[] words = GetWords(sentence);

            // word count
            int wordCount = (int) words.Length;
            Console.WriteLine($"Sentence contains: {wordCount} words");

            // sorted words
            Array.Sort(words);

            // modified words
            words = Capitalize(words); 

            Console.Write("Modified sentence: ");
            Console.WriteLine(String.Join(" ", words));
        }
    }
}
