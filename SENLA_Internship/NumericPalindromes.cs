using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENLA_Internship {
    // finds all palindromes in an array of numbers
    internal class NumericPalindromes {
        // reads a number
        int NumberInput() {
            string inputString = Console.ReadLine().Trim(' ');
            return (int) Convert.ToUInt32(inputString);
        }
        // gets an array from 0 to N - 1
        int[] GetArray(int N) {
            // result array
            int[] numberArray = new int[N];

            for (int i = 0; i < N; i++) numberArray[i] = i;

            return numberArray;
        }
        // checks whether a string is a palindrome
        bool IsPal(string str) {
            // string length
            int length = str.Length;

            // if (str[i] != str[length - 1] then
            // string is not apalindrome
            for (int i = 0; i < length / 2; i++)
                if (str[i] != str[length - i - 1])
                    return false;

            return true;
        }
        // checks whether a number is a palindrome
        bool IsPal(int number) {
            string stringNumber = "";

            // reversed string from a number
            while (number != 0) {
                stringNumber += (char)('0' + (number % 10));
                number /= 10;
            }

            return IsPal(stringNumber);
        }
        // gets all palindromes from array
        List<int> GetPalindromes(int N, int[] numberArray) {
            // result array
            List<int> palArray = new List<int>(); 
            
            for (int i = 0; i < N; i++) {
                if (IsPal(numberArray[i]))
                    palArray.Add(numberArray[i]);
            }

            return palArray;
        }
        // main function
        public void Run() {
            Console.Write("Input a count of numbers: ");

            int N;

            // attempt to read an int32
            try {
                N = NumberInput();
            }
            catch {
                Console.WriteLine("Error: Entered value is not a " +
                    "correct count of numbers.\n");
                return;
            }

            // array of integers from 0 to N - 1
            int[] numberArray = GetArray(N);

            // all palindromes from array
            List<int> numberPals = GetPalindromes(N, numberArray);
            int palCount = numberPals.Count;

            Console.WriteLine($"Count of palindromes: {palCount}");

            Console.Write("Palindromes: ");
            foreach (int number in numberPals)
                Console.Write($"{number} ");

        }
    }
}
