namespace AdventOfCode2025.Solutions
{
    internal class Day5
    {
        public static long Day5First()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay5.txt");
            string[] lines = File.ReadAllLines(path);

            string[] newLines = lines;

            List<(long, long)> ranges = new List<(long, long)>();

            bool hasSeenEmptyLine = false;

            int count = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = newLines[i];

                // process ingredients ranges
                if (!hasSeenEmptyLine && !string.IsNullOrEmpty(line))
                {
                    int dashIndex = line.IndexOf('-');
                    long first = long.Parse(line.Substring(0, dashIndex).Trim());
                    long second = long.Parse(line.Substring(dashIndex + 1).Trim());
                    ranges.Add((first, second));
                }
                else if (!string.IsNullOrEmpty(line)) // process ingredients
                {
                    long ingredientValue = long.Parse(line.Trim());

                    for (int j = 0; j < ranges.Count; j++)
                    {
                        if (ranges[j].Item1 <= ingredientValue && ingredientValue <= ranges[j].Item2)
                        {
                            count++;
                            break;
                        }
                    }
                }

                // indicates switch from ranges -> ingredients
                if (string.IsNullOrEmpty(line))
                {
                    hasSeenEmptyLine = true;
                }
            }

            return count;
        }

        public static long Day5Second()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay5.txt");
            string[] lines = File.ReadAllLines(path);

            string[] newLines = lines;

            List<(long, long)> ranges = new List<(long, long)>();

            long count = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = newLines[i];

                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                int dashIndex = line.IndexOf('-');
                long first = long.Parse(line.Substring(0, dashIndex).Trim());
                long second = long.Parse(line.Substring(dashIndex + 1).Trim());

                // of our current ranges, looking for overlaps.
                    // if no overlap, skip to the next one
                    // if overlap, expand our current range to include all of the existing range and remove the existing range
                for (int j = 0; j < ranges.Count; j++)
                {
                    (long,long) range = ranges[j];

                    // no overlap
                    if (first > range.Item2 || second < range.Item1)
                    {
                        continue;
                    }

                    // newest range extends lower than the existing one; absorb it
                    if (range.Item1 < first)
                    {
                        first = range.Item1;
                    }

                    // newest range extends higher than the existing one; absorb it
                    if (range.Item2 > second)
                    {
                        second = range.Item2;
                    }

                    ranges.RemoveAt(j);
                    j--;
                }

                // add the final no-overlap range
                ranges.Add((first, second));
            }

            for (int i = 0; i < ranges.Count; i++)
            {
                (long,long) range = ranges[i];
                count += range.Item2 - range.Item1 + 1;
            }

            return count;
        }
    }
}
