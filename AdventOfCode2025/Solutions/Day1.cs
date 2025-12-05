namespace AdventOfCode2025.Solutions
{
    internal class Day1
    {
        public static int Day1First()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay1.txt");

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
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay1.txt");
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
