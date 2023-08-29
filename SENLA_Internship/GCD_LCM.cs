using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENLA_Internship {
    // reads two positive integer numbers
    // and calculates their GCD and LCM
    internal class GCD_LCM {
        // reads two numbers
        (int, int) NumbersInput() {
            string inputString1 = Console.ReadLine().Trim(' ');

            uint number1, number2;

            // if two numbers were entered line by line
            if (inputString1.IndexOf(' ') == -1) {
                string inputString2 = Console.ReadLine().Trim(' ');

                number1 = Convert.ToUInt32(inputString1);
                number2 = Convert.ToUInt32(inputString2);
            }
            // if two numbers were entered by space
            else {
                string[] inputSplit = inputString1.Split(' ');

                number1 = Convert.ToUInt32(inputSplit[0]);
                number2 = Convert.ToUInt32(inputSplit[1]);
            }

            if (number1 == 0 || number2 == 0)
                throw new System.FormatException();

            return ((int) number1, (int) number2);
        }
        // calcs greatest common divisor
        int GCD(int number1, int number2) {
            while (number2 != 0) {
                number1 %= number2;
                (number1, number2) = (number2, number1);
            }
            return number1;
        }
        // calcs least common multilpe
        long LCM(int number1, int number2) {

            return ((long) number1) * number2 / GCD(number1, number2);
        }
        // main function
        public void Run() {
            Console.Write("Input two positive integer number: ");

            int number1, number2;

            // attempt to read two int32
            try {
                (number1, number2) = NumbersInput();
            }
            catch {
                Console.WriteLine("Error: One or two entered values " +
                    "aren't correct positive integer numbers.\n");
                return;
            }

            // GCD and LCM
            int gcd = GCD(number1, number2);
            long lcm = LCM(number1, number2);

            Console.WriteLine($"GCD of entered numbers: {gcd}\n" + 
                $"LCM of entered numbers: {lcm}");
        }
    }
}
