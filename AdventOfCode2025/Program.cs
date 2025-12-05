namespace AdventOfCode2025
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //int day1FirstAnswer = Day1First();
            //Console.WriteLine($"Day 1 First Answer: {day1FirstAnswer}");

            //int day1SecondAnswer = Day1Second();
            //Console.WriteLine($"Day 1 Second Answer: {day1SecondAnswer}");

            //long day2FirstAnswer = Day2First();
            //Console.WriteLine($"Day 2 First Answer: {day2FirstAnswer}");

            //long day2SecondAnswer = Day2Second();
            //Console.WriteLine($"Day 2 Second Answer: {day2SecondAnswer}");

            //long day3FirstAnswer = Day3First();
            //Console.WriteLine($"Day 3 First Answer: {day3FirstAnswer}");

            //long day3SecondAnswer = Day3Second();
            //Console.WriteLine($"Day 3 Second Answer: {day3SecondAnswer}");

            long day4FirstAnswer = Day4First();
            Console.WriteLine($"Day 4 First Answer: {day4FirstAnswer}");

            long day4SecondAnswer = Day4Second();
            Console.WriteLine($"Day 4 Second Answer: {day4SecondAnswer}");
        }

        public static long Day4First()
        {
            string path = @"C:\Projects\Personal\AdventOfCode2025\AdventOfCode2025\PuzzleInputDay4.txt";
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

            //for (int i = 0; i < newLines.Length; i++)
            //{
            //    Console.WriteLine(newLines[i]);
            //}

            // now we've surrounded everything with dots in newLines

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
            string path = @"C:\Projects\Personal\AdventOfCode2025\AdventOfCode2025\PuzzleInputDay4.txt";
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

            bool removedARoll = true;

            while (removedARoll) {
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

        public static bool Day4SecondHelper(string[] lines, int row, int col)
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



        public static int Day3First()
        {
            string path = @"C:\Projects\Personal\AdventOfCode2025\AdventOfCode2025\PuzzleInputDay3.txt";
            string[] lines = File.ReadAllLines(path);

            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                int tensMax = -1;
                int tensMaxLocation = -1;

                // get left-most max (don't asses the last char)
                for (int j = 0; j < line.Length - 1; j++)
                {
                    int num = Convert.ToInt32(line[j].ToString());

                    if (num > tensMax)
                    {
                        tensMax = num;
                        tensMaxLocation = j;
                    }
                }

                // starting from char AFTER the left-most max, get the next max
                int onesMax = -1;
                int onesMaxLocation = -1;

                for (int j = tensMaxLocation + 1; j < line.Length; j++)
                {
                    int num = Convert.ToInt32(line[j].ToString());

                    if (num > onesMax)
                    {
                        onesMax = num;
                        //onesMaxLocation = j;
                    }
                }

                sum += Convert.ToInt32(tensMax.ToString() + onesMax.ToString());
            }

            return sum;
        }

        public static long Day3Second()
        {
            string path = @"C:\Projects\Personal\AdventOfCode2025\AdventOfCode2025\PuzzleInputDay3.txt";
            string[] lines = File.ReadAllLines(path);

            long sum = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                int startIndex = 0;
                int buffer = 12;
                int leftMax = -1;
                int leftMaxLocation = -1;

                string voltageBuilder = "";

                // repeat 12 times getting the left-most max that's at least buffer chars away from the end of the string
                for (int j = 0; j < 12; j++)
                {
                    (leftMax, leftMaxLocation) = Day3SecondHelper(line, startIndex, buffer);

                    voltageBuilder += leftMax.ToString();
                    startIndex = leftMaxLocation + 1;
                    buffer -= 1;
                }

                sum += Convert.ToInt64(voltageBuilder);
            }

            return sum;
        }

        public static (int,int) Day3SecondHelper(string line, int startIndex, int buffer)
        {
            int max = -1;
            int maxLocation = -1;

            for (int i = startIndex; i < line.Length - buffer + 1; i++)
            {
                int num = Convert.ToInt32(line[i].ToString());

                if (num > max)
                {
                    max = num;
                    maxLocation = i;
                }
            }

            return (max,maxLocation);
        }

        public static long Day2First()
        {
            string path = @"C:\Projects\Personal\AdventOfCode2025\AdventOfCode2025\PuzzleInputDay2.txt";
            string text = File.ReadAllText(path);

            string[] ranges = text.Split(',');

            long sum = 0;

            for (int i = 0; i < ranges.Length; i++)
            {
                string range = ranges[i];
                int splitIndex = range.IndexOf('-');
                long low = Convert.ToInt64(range.Substring(0, splitIndex));
                long high = Convert.ToInt64(range.Substring(splitIndex + 1));

                for (long j = low; j <= high; j++)
                {
                    string num = j.ToString();

                    if (num.Length == 1)
                        continue;

                    int mid = num.Length / 2;

                    if (num.Length % 2 == 0) // evens
                    {
                        string front = num.Substring(0, mid);
                        string back = num.Substring(mid);

                        if (front == back)
                            sum += Convert.ToInt64(num);
                    }
                }
            }

            return sum;
        }

        public static long Day2Second()
        {
            string path = @"C:\Projects\Personal\AdventOfCode2025\AdventOfCode2025\PuzzleInputDay2.txt";
            string text = File.ReadAllText(path);

            string[] ranges = text.Split(',');

            long sum = 0;

            for (int i = 0; i < ranges.Length; i++)
            {
                string range = ranges[i];
                int splitIndex = range.IndexOf('-');
                long low = Convert.ToInt64(range.Substring(0, splitIndex));
                long high = Convert.ToInt64(range.Substring(splitIndex + 1));

                for (long j = low; j <= high; j++)
                {
                    string num = j.ToString();

                    if (num.Length == 1)
                        continue;

                    int mid = num.Length / 2; // easy for now

                    for (int x = 1; x <= mid; x++)
                    {
                        string seq = num.Substring(0, x);

                        if (num.Split(seq).All(x => x == ""))
                        {
                            sum += Convert.ToInt64(num);
                            break;
                        }
                    }
                }
            }

            return sum;
        }


        public static int Day1First()
        {
            string path = @"C:\Projects\Personal\AdventOfCode2025\AdventOfCode2025\PuzzleInputDay1.txt";
            string[] lines = File.ReadAllLines(path);

            int location = 50;
            int tracker = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                char direction = line[0];
                int clicks = Convert.ToInt32(line.Substring(1));

                if (direction == 'L')
                    location = ((location - clicks) % 100 + 100) % 100;
                else
                    location = (location + clicks) % 100;

                if (location == 0)
                    tracker++;
            }

            return tracker;
        }

        public static int Day1Second()
        {
            string path = @"C:\Projects\Personal\AdventOfCode2025\AdventOfCode2025\PuzzleInputDay1.txt";
            string[] lines = File.ReadAllLines(path);

            int location = 50;
            int tracker = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                char direction = line[0];
                int clicks = Convert.ToInt32(line.Substring(1));

                if (direction == 'L')
                {
                    if (location - clicks < 0)
                    {
                        int offset = location == 0 ? 0 : 1;
                        tracker += Math.Abs((location - clicks) / 100) + offset;

                        if ((location - clicks) % 100 == 0) // get caught later
                            tracker--;
                    }

                    location = ((location - clicks) % 100 + 100) % 100;
                }
                else
                {
                    if (location + clicks > 100)
                    {
                        tracker += (location + clicks) / 100;

                        if ((location + clicks) % 100 == 0) // get caught later
                            tracker--;
                    }

                    location = (location + clicks) % 100;
                }

                if (location == 0)
                    tracker++;
            }

            return tracker;
        }
    }
}
