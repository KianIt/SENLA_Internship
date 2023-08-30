using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SENLA_Internship {
    // constructs tables of collections efficiency
    internal class CollectionsCompairing {
        // table class
        class CollactionTable {
            // constructs a string centered in a field
            static string CenteredString(string str, int width) {
                if (str.Length >= width) {
                    return str;
                }

                int length = str.Length;
                return new string(' ', (width - length + 1) / 2) +
                    str +
                    new string(' ', width - length - (width - length + 1) / 2);
            }

            // width of a cell
            public int cellWidth;
            // number of cells in a row
            public int rowLength;
            // table row array
            List<string> rows;

            // constructs a table object
            public CollactionTable(int cellWidth = 9, int rowLength = 6) {
                this.cellWidth = cellWidth;
                this.rowLength = rowLength;
                rows = new List<string>();
            }

            // prints a line to separate rows
            void PrintSeparator() {
                Console.WriteLine(new string('-', rows[0].Length));
            }
            // adds a row into a table
            public void AddRow(string[] rowValues) {
                string row = "";

                foreach (string value in rowValues)
                    row += String.Format("{0}|", CenteredString(value, cellWidth));

                rows.Add(row);
            }

            // prints a whole table
            public void PrintTable() {
                foreach (string row in rows) {
                    Console.WriteLine(row);
                    PrintSeparator();
                }
            }
        }

        // count of trials for each operation
        const int trialsCount = (int)(2e4);
        // maximum value of items in collections
        const int maxItemValue = (int)(1e9);

        // random generator
        Random rand = new Random();
        // stopwatch for time measurements
        Stopwatch stopWatch = new Stopwatch();

        // main function
        public void Run() {
            // analyzed operations
            string[] operations = new string[] 
            { " ", "Add", "Insert", "Find", "Sort", "Remove" };

            // efficiency table
            var table = new CollactionTable();
            table.AddRow(operations);

            ////////////////////////////////////////////
            // List
            ///////////////////////////////////////////

            // table row for list
            string[] rowList = new string[table.rowLength];
            for (int i = 0; i < table.rowLength; i++) rowList[i] = "";
            rowList[0] = "List";

            // tested list
            List<int> testList = new List<int>();

            int testNumber = 1;

            // add testing
            stopWatch.Start();
            for (int i = 0; i < trialsCount; i++)
                testList.Add(rand.Next((int)(1e9)));
            stopWatch.Stop();
            rowList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // insert testing
            testList.Clear();
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testList.Insert(rand.Next(testList.Count), rand.Next(maxItemValue));
            stopWatch.Stop();
            rowList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // find testing
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testList.Contains(rand.Next(maxItemValue));
            stopWatch.Stop();
            rowList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // sort testing
            stopWatch.Restart();
            testList.Sort();
            stopWatch.Stop();
            rowList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // remove testing
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testList.Remove(rand.Next(testList.Count));
            stopWatch.Stop();
            rowList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            table.AddRow(rowList);
            
            ////////////////////////////////////////////
            // ArrayList
            ///////////////////////////////////////////

            // table row for array list
            string[] rowArrayList = new string[table.rowLength];
            for (int i = 0; i < table.rowLength; i++) rowArrayList[i] = "";
            rowArrayList[0] = "ArrayList";

            // tested array list
            ArrayList testArrayList = new ArrayList();

            testNumber = 1;

            // add testing
            stopWatch.Start();
            for (int i = 0; i < trialsCount; i++)
                testArrayList.Add(rand.Next((int) (1e9)));
            stopWatch.Stop();
            rowArrayList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // insert testing
            testList.Clear();
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testArrayList.Insert(rand.Next(testList.Count), rand.Next(maxItemValue));
            stopWatch.Stop();
            rowArrayList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // find testing
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testArrayList.Contains(rand.Next(maxItemValue));
            stopWatch.Stop();
            rowArrayList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // sort testing
            stopWatch.Restart();
            testArrayList.Sort();
            stopWatch.Stop();
            rowArrayList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // remove testing
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testArrayList.Remove(rand.Next(testList.Count));
            stopWatch.Stop();
            rowArrayList[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            table.AddRow(rowArrayList);

            ////////////////////////////////////////////
            // SortedSet
            ///////////////////////////////////////////

            // table row for sorted set
            string[] rowSortedSet = new string[table.rowLength];
            for (int i = 0; i < table.rowLength; i++) rowSortedSet[i] = "";
            rowSortedSet[0] = "SortedSet";

            // tested sorted set
            SortedSet<int> testSortedSet = new SortedSet<int>();

            testNumber = 1;

            // add testing
            stopWatch.Start();
            for (int i = 0; i < trialsCount; i++)
                testSortedSet.Add(rand.Next(maxItemValue));
            stopWatch.Stop();
            rowSortedSet[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // insert testing
            rowSortedSet[testNumber++] = "";

            // find testing
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testSortedSet.Contains(rand.Next(maxItemValue));
            stopWatch.Stop();
            rowSortedSet[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // sort testing
            rowSortedSet[testNumber++] = "";

            // remove testing
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testSortedSet.Remove(rand.Next(maxItemValue));
            stopWatch.Stop();
            rowSortedSet[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            table.AddRow(rowSortedSet);

            ////////////////////////////////////////////
            // Hashtable
            ///////////////////////////////////////////

            // table row for hastable
            string[] rowHashtable = new string[table.rowLength];
            for (int i = 0; i < table.rowLength; i++) rowHashtable[i] = "";
            rowHashtable[0] = "Hashtable";

            // tested hashtable
            Hashtable testHashtable = new Hashtable();

            testNumber = 1;

            // add testing
            stopWatch.Start();
            for (int i = 0; i < trialsCount; i++)
                testHashtable[rand.Next(maxItemValue)] = rand.Next(maxItemValue);
            stopWatch.Stop();
            rowHashtable[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // insert testing
            rowHashtable[testNumber++] = "";

            // find testing
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testHashtable.Contains(rand.Next(maxItemValue));
            stopWatch.Stop();
            rowHashtable[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            // sort testing
            rowHashtable[testNumber++] = "";

            // remove testing
            stopWatch.Restart();
            for (int i = 0; i < trialsCount; i++)
                testHashtable.Remove(rand.Next(maxItemValue));
            stopWatch.Stop();
            rowHashtable[testNumber++] = stopWatch.ElapsedMilliseconds.ToString();

            table.AddRow(rowHashtable);

            table.PrintTable();
        }
    }
}
