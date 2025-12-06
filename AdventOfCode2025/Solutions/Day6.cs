using System.Reflection;

namespace AdventOfCode2025.Solutions
{
    internal class Day6
    {
        public static long Day6First()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay6.txt");
            string[] lines = File.ReadAllLines(path);

            List<List<string>> linesData = new List<List<string>>();

            // split input into a grid: x rows of numbers, followed by a row of operators
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split(' ');

                List<string> linedata = parts.Where(x => !string.IsNullOrEmpty(x)).ToList();

                linesData.Add(linedata);
            }

            int numRows = linesData.Count;
            int numCols = linesData[0].Count;

            long sum = 0;

            // iterate across the grid by column and calculate each column's total and add it to the final sum
            for (int i = 0; i < numCols; i++)
            {
                string operation = linesData[numRows - 1][i]; // opreator is on last row
                long currTotal = 0;

                if (operation == "+")
                {
                    currTotal = 0;
                    for (int j = 0; j < numRows - 1; j++)
                    {
                        currTotal += Convert.ToInt64(linesData[j][i]);
                    }
                }
                else
                {
                    currTotal = 1;
                    for (int j = 0; j < numRows - 1; j++)
                    {
                        currTotal *= Convert.ToInt64(linesData[j][i]);
                    }
                }
                sum += currTotal;
            }

            return sum;
        }

        public static long Day6Second()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay6.txt");
            string[] lines = File.ReadAllLines(path);

            // padding at end of each row to help finish processing
            for (int i = 0; i < lines.Count(); i++)
            {
                lines[i] += " ";
            }

            int rowCount = lines.Count();
            int lineLength = lines[0].Length;

            List<long> totals = new List<long>();

            char currOperator = lines[rowCount - 1][0];
            long currTotal = currOperator == '+' ? 0 : 1;

            for (int i = 0; i < lineLength - 1; i++)
            {
                string currNumString = "";

                // get vertical number's string
                for (int j = 0; j < rowCount - 1; j++)
                {
                    if (lines[j][i] != ' ')
                    {
                        currNumString += lines[j][i].ToString();
                    }
                }

                // if no numbers in column, then we're switching to a new opreator
                if (currNumString == "")
                {
                    totals = totals.Prepend(currTotal).ToList();

                    if (lines[rowCount - 1][i + 1] == '+')
                    {
                        currOperator = '+';
                        currTotal = 0;
                    }
                    else
                    {
                        currOperator = '*';
                        currTotal = 1;
                    }

                    continue;
                }

                // get number and apply it to the total based on the current operator
                int num = Convert.ToInt32(currNumString);

                if (currOperator == '+')
                {
                    currTotal += num;
                }
                else
                {
                    currTotal *= num;
                }
            }

            // algo breaks out before currTotal can be added to the totals. So just add here
            return totals.Sum() + currTotal;
        }
    }
}
