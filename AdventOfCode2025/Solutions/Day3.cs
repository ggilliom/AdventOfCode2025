namespace AdventOfCode2025.Solutions
{
    internal class Day3
    {
        public static int Day3First()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay3.txt");
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

                for (int j = tensMaxLocation + 1; j < line.Length; j++)
                {
                    int num = Convert.ToInt32(line[j].ToString());

                    if (num > onesMax)
                    {
                        onesMax = num;
                    }
                }

                sum += Convert.ToInt32(tensMax.ToString() + onesMax.ToString());
            }

            return sum;
        }

        public static long Day3Second()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay3.txt");
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

        public static (int, int) Day3SecondHelper(string line, int startIndex, int buffer)
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

            return (max, maxLocation);
        }
    }
}
