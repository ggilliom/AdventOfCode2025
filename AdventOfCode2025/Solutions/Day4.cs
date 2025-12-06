namespace AdventOfCode2025.Solutions
{
    internal class Day4
    {
        public static long Day4First()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay4.txt");
            string[] lines = File.ReadAllLines(path);

            string[] newLines = lines;

            for (int i = 0; i < lines.Length; i++)
            {
                string newLine = "." + newLines[i] + ".";
                lines[i] = newLine;
            }

            int sum = 0;

            string dots = "";

            for (int i = 0; i < newLines[0].Length; i++)
            {
                dots += ".";
            }

            newLines = newLines.Append(dots).ToArray();
            newLines = newLines.Prepend(dots).ToArray();

            // now we've surrounded everything with dots in newLines
                // ^ avoids needing to check for index boundary errors

            for (int row = 1; row < newLines.Length - 1; row++)
            {
                for (int col = 1; col < newLines[row].Length - 1; col++)
                {
                    if (newLines[row][col] == '@' && Day4FirstHelper(newLines, row, col))
                    {
                        sum += 1;
                    }
                }
            }

            return sum;
        }

        public static bool Day4FirstHelper(string[] lines, int row, int col)
        {
            bool[] checks = {
                lines[row - 1][col - 1] == '@',
                lines[row - 1][col] == '@',
                lines[row - 1][col + 1] == '@',
                lines[row][col - 1] == '@',
                lines[row][col + 1] == '@',
                lines[row + 1][col - 1] == '@',
                lines[row + 1][col] == '@',
                lines[row + 1][col + 1] == '@',
            };

            return checks.Where(x => x).Count() < 4;
        }

        public static long Day4Second()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay4.txt");
            string[] lines = File.ReadAllLines(path);

            string[] newLines = lines;

            for (int i = 0; i < lines.Length; i++)
            {
                string newLine = "." + newLines[i] + ".";
                lines[i] = newLine;
            }

            int sum = 0;

            string dots = "";

            for (int i = 0; i < newLines[0].Length; i++)
            {
                dots += ".";
            }

            newLines = newLines.Append(dots).ToArray();
            newLines = newLines.Prepend(dots).ToArray();

            // now we've surrounded everything with dots in newLines
                // ^ avoids needing to check for index boundary errors

            bool removedARoll = true;

            // could also propogate out recursively...but this is fine too
            while (removedARoll)
            {
                removedARoll = false;

                for (int row = 1; row < newLines.Length - 1; row++)
                {
                    for (int col = 1; col < newLines[row].Length - 1; col++)
                    {
                        if (newLines[row][col] == '@' && Day4FirstHelper(newLines, row, col))
                        {
                            sum += 1;
                            newLines[row] = newLines[row].Substring(0, col) + "x" + newLines[row].Substring(col + 1);
                            removedARoll = true;
                        }
                    }
                }
            }

            return sum;
        }
    }
}
