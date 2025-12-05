namespace AdventOfCode2025.Solutions
{
    internal class Day2
    {
        public static long Day2First()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay2.txt");
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
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay2.txt");
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
    }
}
