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

            // loop through all the ranges
            for (int i = 0; i < ranges.Length; i++)
            {
                string range = ranges[i];
                int splitIndex = range.IndexOf('-');
                long low = Convert.ToInt64(range.Substring(0, splitIndex));
                long high = Convert.ToInt64(range.Substring(splitIndex + 1));

                // check if the first half of each number in the range == second half
                for (long j = low; j <= high; j++)
                {
                    string num = j.ToString();

                    // numbers of odd lengths are never valid
                    if (num.Length % 2 == 1)
                        continue;

                    // num must have an even length
                    int mid = num.Length / 2;

                    string front = num.Substring(0, mid);
                    string back = num.Substring(mid);

                    if (front == back)
                        sum += Convert.ToInt64(num);
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

            // loop through all the ranges
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

                    // check if each substring starting at the beginning is repeated throughout the entire number
                    for (int x = 1; x <= mid; x++)
                    {
                        string seq = num.Substring(0, x);

                        if (num.Split(seq).All(x => x == "")) // checks if current number is the repeated substring
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
