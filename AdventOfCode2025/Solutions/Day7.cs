namespace AdventOfCode2025.Solutions
{
    internal class Day7
    {
        public static long Day7First()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay7.txt");
            string[] lines = File.ReadAllLines(path);

            int count = 0;
            int rowLength = lines[0].Length;

            // will be modifying chars directly, which will be easier to do in a 2D char array vs an array of strings
            char[][] charLines = new char[lines.Length][];
            for (int i = 0; i < charLines.Length; i++)
            {
                charLines[i] = lines[i].ToCharArray();
            }

            // draw beam below S

            int indexS = lines[0].IndexOf('S');
            charLines[1][indexS] = '|';

            // start one 3rd row b/c that's where the first splitter is
            // end after second-to-last row because that's where the final splitters are
            for (int row = 2; row < lines.Length - 1; row++)
            {
                for (int col = 0; col < rowLength; col++)
                {
                    // if there's a beam above,
                        // and current char is a splitter,
                            // add 1 to counter
                            // add beams to the left and right of the splitter
                        // else (we're at an empty space)
                            // drag the beam down to the current char

                    if (charLines[row - 1][col] == '|')
                    {
                        if (charLines[row][col] == '^') // hit a splitter
                        {
                            count++;
                            charLines[row][col - 1] = '|';
                            charLines[row][col + 1] = '|';
                        }
                        else // empty space
                        {
                            charLines[row][col] = '|';
                        }
                    }
                }
            }

            return count;
        }

        public static long Day7Second()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay7.txt");
            string[] lines = File.ReadAllLines(path);

            int rowLength = lines[0].Length;

            // turn array of strings into array of char arrays

            char[][] charLines = new char[lines.Length][];
            long[][] timelineTracker = new long[lines.Length][];

            for (int i = 0; i < charLines.Length; i++)
            {
                charLines[i] = new char[rowLength];
                timelineTracker[i] = new long[rowLength];
                for (int j = 0; j < rowLength; j++)
                {
                    charLines[i][j] = lines[i][j];
                    timelineTracker[i][j] = charLines[i][j] == '^' ? -1 : 0;
                }
            }

            // idea: keep track of how many timelines can get to any point from top -> bottom. Then sum everything at the end.

            // timelineTracker stores a map identical to charLines but instead of storing chars it stores a number at each location
            // that represents how many timelines could have gotten to a given point OR a -1 to indicate a splitter

            // once we've mapped all splitters to a -1, we can just sum the number of different timelines each end position could be reached from
            // to get the total number of possible timelines

            // instead of drawing lines downward, we'll draw # of timelines downward.
            // Need to keep in mind when a splitter splits into an already downward-cascading beam from other timelines

            // draw beam == 1 below S to get things going
            // Start loop at third row again given this ^

            int indexS = lines[0].IndexOf('S');
            timelineTracker[1][indexS] = 1;

            for (int row = 2; row < lines.Length; row++)
            {
                for (int col = 0; col < rowLength; col++)
                {
                    long aboveTimelines = timelineTracker[row - 1][col];

                    if (aboveTimelines > 0) // beam above
                    {
                        if (timelineTracker[row][col] < 0) // current char is splitter
                        {
                            // apply possible timelines to left and right of splitter
                            timelineTracker[row][col - 1] += aboveTimelines;
                            timelineTracker[row][col + 1] += aboveTimelines;
                        }
                        else // timelines above in addition to current ones -- should add
                        {
                            timelineTracker[row][col] += aboveTimelines;
                        }
                    }
                }
            }

            long numTimelines = timelineTracker[lines.Length - 1].Sum();

            return numTimelines;
        }
    }
}
