using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENLA_Internship {
    // reads a non-negative integer number
    // and checks whether it's odd/even and prime/non-prime
    internal class NumberChecker {
        // reads a number
        int NumberInput() {
            string inputString = Console.ReadLine().Trim(' ');
            return (int) Convert.ToUInt32(inputString);
        }
        // checks whether a number is odd
        bool IsOdd(int number) {
            // if number % 2 == 1 then number is odd otherwise it's even
            return number % 2 == 1;
        }
        // checks whether a number is prime
        bool IsPrime(int number) {
            // if number <= 1 then it cant' be prime othrwise it's assumed to be prime
            if (number <= 1) return false;

            // if number has a divisor greater than 1 and
            // less or equal sqrt(number) then it's not prime
            for (int div = 2; div * div <= number; div++)
                if (number % div == 0) {
                    return false;
                }

            return true;
        }
        // main function
        public void Run() {
            Console.Write("Input a non-negative integer number: ");

            int number;

            // attempt to read an int32
            try {
                 number = NumberInput();
            }
            catch {
                Console.WriteLine("Error: Entered value is not a " +
                    "correct non-negative integer number.\n");
                return;
            }

            string oddString = IsOdd(number) ? "odd" : "even", 
                primeString = IsPrime(number) ? "prime" : "non-prime";

            Console.WriteLine($"Input number is {oddString} and {primeString}");
        }
    }
}
