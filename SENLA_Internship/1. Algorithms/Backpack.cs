using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENLA_Internship {
    // calculates most optimal backpack
    internal class Backpack {
        // reads a number
        int NumberInput() {
            string inputString = Console.ReadLine().Trim(' ');
            return Convert.ToInt32(inputString);
        }
        // reads an array
        int[] ArrayInput(int N) {
            // result array
            int[] array = new int[N];

            int n = 0;
            while (n < N) {
                string inputString = Console.ReadLine().Trim(' ');
                string[] inputSplit = inputString.Split(' ');

                foreach (string numberString in inputSplit) {
                    array[n++] = Convert.ToInt32(numberString);

                    if (n == N) break;
                }
            }
       
            return array;
        }
        // calcs backpack dynamic programming array
        int[,] BackpackDynamic(int N, int W, int[] weights, int[] prices) {
            int[,] backpackDP = new int[N, W + 1];

            // filling array with 0
            for (int i = 0; i < N; i++)
                for (int j = 0; j < W + 1; j++)
                    backpackDP[i, j] = 0;

            // seting first line of array
            for (int j = 0; j < W + 1; j++)
                if (weights[0] <= j)
                    backpackDP[0, j] = prices[0];

            // setting other lines of array
            for (int i = 1; i < N; i++)
                for (int j = 0; j < W + 1; j++) {
                    backpackDP[i, j] = backpackDP[i - 1, j];
                    if (weights[i] <= j)
                        backpackDP[i, j] = Math.Max(backpackDP[i, j], 
                            backpackDP[i - 1, j - weights[i]] + prices[i]);
                }

            return backpackDP;
        }
        // gets an optimal set of items in backpack
        List<int> BackpackItems(int N, int W, int[] weights, int[,] backpackDP) {
            // result list
            List<int> backpackItems = new List<int>();

            // getting items in backpack in reversed order
            int weight = W;
            for (int i = N - 1; i >= 0; i--)
                if (i == 0 && backpackDP[i, weight] != 0) {
                    backpackItems.Add(i);
                }
                else if (i != 0 && backpackDP[i, weight] != backpackDP[i - 1, weight]) {
                    backpackItems.Add(i);
                    weight -= weights[i];
                }

            // reversing order
            backpackItems.Reverse();

            return backpackItems;
        }
        // main function
        public void Run() {
            Console.Write("Input a count of items: ");

            int N;

            // attempt to read an int32
            try {
                N = NumberInput();
            }
            catch {
                Console.WriteLine("Error: Entered value is not a " +
                    "correct count of items.\n");
                return;
            }

            Console.Write($"Input an array of {N} item weights: ");

            int[] weights = new int[N];

            // attempt to read an array of int32
            try {
                weights = ArrayInput(N);
            }
            catch {
                Console.WriteLine("Error: Error while reading " +
                    "an array of item weights.\n");
                return;
            }

            Console.Write($"Input an array of {N} item prces: ");

            int[] prices = new int[N];

            // attempt to read an array of int32
            try {
                prices = ArrayInput(N);
            }
            catch {
                Console.WriteLine("Error: Error while reading " +
                    "an array of item prices.\n");
                return;
            }

            Console.Write("Input a maximum weight: ");

            int W;

            // attempt to read an int32
            try {
                W = NumberInput();
            }
            catch {
                Console.WriteLine("Error: Entered value is not a " +
                    "correct maximum weight.\n");
                return;
            }

            // dynamic backpack
            int[,] backpackDP = BackpackDynamic(N, W, weights, prices);

            // list of items
            List<int> backpackItems = BackpackItems(N, W, weights, backpackDP);

            int backpackPrice = backpackDP[N - 1, W], backpackWeight = 0;

            foreach (int item in backpackItems)
                backpackWeight += weights[item];

            Console.WriteLine($"Backpack price: {backpackPrice}");
            Console.WriteLine($"Backpack weight: {backpackWeight}");

            int backpackSize = backpackItems.Count;
            Console.WriteLine($"Backpack size: {backpackSize}");

            Console.Write($"Backpack items: ");
                foreach (int item in backpackItems)
                    Console.Write($"{item} ");
        }
    }
}
