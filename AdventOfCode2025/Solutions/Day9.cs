#nullable enable

using System;
using System.Collections.Immutable;

namespace AdventOfCode2025.Solutions
{
    internal class Day9
    {
        public static long Day9First()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "TestInputDay9.txt");
            string[] lines = File.ReadAllLines(path);

            (int, int)[] coords = new (int, int)[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(',');
                coords[i] = (Convert.ToInt32(line[0]), Convert.ToInt32(line[1]));
            }

            List<int> areas = new List<int>();

            for (int i = 0; i < lines.Length - 1; i++)
            {
                for (int j = 0; j < lines.Length; j++)
                {
                    (int,int) first = coords[i];
                    (int, int) second = coords[j];

                    int area = Math.Abs(first.Item1 - second.Item1 + 1) * Math.Abs(first.Item2 - second.Item2 + 1);
                    areas.Add(area);
                }
            }

            return areas.Max();
        }


        public static long Day9Second()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay9.txt");
            string[] lines = File.ReadAllLines(path);

            
            return 0;
        }
    }
}
