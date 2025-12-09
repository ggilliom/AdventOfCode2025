#nullable enable

using System;
using System.Collections.Immutable;

namespace AdventOfCode2025.Solutions
{
    internal class Day8
    {
        public struct Box
        {
            public int X;
            public int Y;
            public int Z;
            public int CircuitIndex;
            public int Loc;

            public Box (int x, int y, int z, int loc)
            {
                X = x;
                Y = y;
                Z = z;
                Loc = loc;
                CircuitIndex = -1;
        }

        }

        public struct Pair
        {
            public int IndexP;
            public int IndexQ;
            public double Distance;

            public Pair(int indexP, int indexQ, double distance)
            {
                IndexP = indexP;
                IndexQ = indexQ;
                Distance = distance;
            }
        }

        // create struct that represents a given box
        // re-create the input list as a list of those structs ^ IN THE SAME ORDER (add their index in the list as a property?)
        // create an empty list of pairs of either the structs or indexes of the structs
            // loop through all the combinations and add them to the list ^ ORDERED BY DISTANCE
            // start at beginning and only insert if distance is less than the next item in the list
        // once all the ordered distances are gathered, start building circuits
            // circuits are a list of a list of the boxes (we don't know how many circuits there will be, nor do we know how big each circuit will be
            // for each smallest distance, either neither box is in a circuit, one is and one isn't, both boxes are already in the same circuit, or each is in a different one
                // based on this ^, either add a brand new circuit, add one to an existing circuit, merge two circuits, or do nothing
                // when assigning each box to a circuit, each box should have a property that stores the circuit it's in to avoid searching for it each time

        public static long Day8First()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay8.txt");
            string[] lines = File.ReadAllLines(path);

            Box[] boxes = new Box[lines.Length];

            List<List<Box>> boxCircuits = new List<List<Box>>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] nums = lines[i].Split(',');
                (int,int,int) coords = (Convert.ToInt32(nums[0]), Convert.ToInt32(nums[1]), Convert.ToInt32(nums[2]));
                boxes[i] = new Box(coords.Item1, coords.Item2, coords.Item3, i);
            }

            List<Pair> pairs = new List<Pair>();
            
            for (int i = 0; i < boxes.Length - 1; i++)
            {
                for (int j = i + 1; j < boxes.Length; j++)
                {
                    Box p = boxes[i];
                    Box q = boxes[j];
                    double distance = GetDistance(p, q);
                    pairs.Add(new Pair(i, j, distance));
                }
            }

            pairs = pairs.OrderBy(x => x.Distance).ToList();

            // pairs is already sorted! iterate through starting at i=0

            // once all the ordered distances are gathered, start building circuits
                // circuits are a list of a list of the boxes (we don't know how many circuits there will be, nor do we know how big each circuit will be
            // for each smallest distance, either neither box is in a circuit, one is and one isn't, both boxes are already in the same circuit, or each is in a different one
                // based on this ^, either add a brand new circuit, add one to an existing circuit, merge two circuits, or do nothing
                // when assigning each box to a circuit, each box should have a property that stores the circuit it's in to avoid searching for it each time

            int idx = 0;
            int circuitsConnected = 0;
            while (circuitsConnected < 1000)
            {
                Pair pair = pairs[idx];
                if (boxes[pair.IndexP].CircuitIndex < 0 && boxes[pair.IndexQ].CircuitIndex < 0) // either neither box is in a circuit
                {
                    int numCircuits = boxCircuits.Count();
                    boxes[pair.IndexP].CircuitIndex = numCircuits;
                    boxes[pair.IndexQ].CircuitIndex = numCircuits;
                    List<Box> newCircuit = [boxes[pair.IndexP], boxes[pair.IndexQ]];
                    boxCircuits.Add(newCircuit);
                    circuitsConnected++;
                }
                else if (boxes[pair.IndexP].CircuitIndex < 0 && boxes[pair.IndexQ].CircuitIndex >= 0) //  one is and one isn't
                {
                    boxes[pair.IndexP].CircuitIndex = boxes[pair.IndexQ].CircuitIndex;
                    boxCircuits[boxes[pair.IndexQ].CircuitIndex].Add(boxes[pair.IndexP]);
                    circuitsConnected++;
                }
                else if (boxes[pair.IndexQ].CircuitIndex < 0 && boxes[pair.IndexP].CircuitIndex >= 0) //  one is and one isn't
                {
                    boxes[pair.IndexQ].CircuitIndex = boxes[pair.IndexP].CircuitIndex;
                    boxCircuits[boxes[pair.IndexP].CircuitIndex].Add(boxes[pair.IndexQ]);
                    circuitsConnected++;
                }
                else if (boxes[pair.IndexP].CircuitIndex >= 0 && boxes[pair.IndexQ].CircuitIndex >= 0) // both in a circuit already
                {
                    // if different circuits...
                    if (boxes[pair.IndexP].CircuitIndex != boxes[pair.IndexQ].CircuitIndex)
                    {
                        List<Box> pCircuit = boxCircuits[boxes[pair.IndexP].CircuitIndex];
                        List<Box> qCircuit = boxCircuits[boxes[pair.IndexQ].CircuitIndex];

                        List<Box> merged = pCircuit.Concat(qCircuit).ToList();

                        boxCircuits[boxes[pair.IndexP].CircuitIndex].Clear();
                        boxCircuits[boxes[pair.IndexQ].CircuitIndex].Clear();

                        boxCircuits.Add(merged);
                        int idxLastCircuit = boxCircuits.Count() - 1;
                        int numBoxesInLastCircuit = boxCircuits[idxLastCircuit].Count();
                        for (int j = 0; j < numBoxesInLastCircuit; j++)
                        {
                            Box currBox = boxCircuits[idxLastCircuit][j];
                            boxes[currBox.Loc].CircuitIndex = idxLastCircuit;
                        }
                    }
                    circuitsConnected++; // connect a circuit EVEN IF they're already within the same circuit
                }
                idx++;
            }

            List<List<Box>> sortedCircuits = boxCircuits.OrderBy(x => x.Count()).Reverse().ToList();

            return sortedCircuits[0].Count() * sortedCircuits[1].Count() * sortedCircuits[2].Count();
        }

        public static double GetDistance(Box p, Box q)
        {
            double distance = Math.Sqrt(Math.Pow(p.X - q.X, 2) + Math.Pow(p.Y - q.Y, 2) + Math.Pow(p.Z - q.Z, 2));

            return distance;
        }

        public static long Day8Second()
        {
            string path = Path.Combine(AppContext.BaseDirectory, "Inputs", "PuzzleInputDay8.txt");
            string[] lines = File.ReadAllLines(path);

            Box[] boxes = new Box[lines.Length];

            List<List<Box>> boxCircuits = new List<List<Box>>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] nums = lines[i].Split(',');
                (int, int, int) coords = (Convert.ToInt32(nums[0]), Convert.ToInt32(nums[1]), Convert.ToInt32(nums[2]));
                boxes[i] = new Box(coords.Item1, coords.Item2, coords.Item3, i);
            }

            List<Pair> pairs = new List<Pair>();

            for (int i = 0; i < boxes.Length - 1; i++)
            {
                for (int j = i + 1; j < boxes.Length; j++)
                {
                    Box p = boxes[i];
                    Box q = boxes[j];
                    double distance = GetDistance(p, q);
                    pairs.Add(new Pair(i, j, distance));
                }
            }

            pairs = pairs.OrderBy(x => x.Distance).ToList();

            // pairs is already sorted! iterate through starting at i=0

            // once all the ordered distances are gathered, start building circuits
                // circuits are a list of a list of the boxes (we don't know how many circuits there will be, nor do we know how big each circuit will be
            // for each smallest distance, either neither box is in a circuit, one is and one isn't, both boxes are already in the same circuit, or each is in a different one
                // based on this ^, either add a brand new circuit, add one to an existing circuit, merge two circuits, or do nothing
                // when assigning each box to a circuit, each box should have a property that stores the circuit it's in to avoid searching for it each time

            int idx = 0;
            int circuitsConnected = 0;
            int product = 0;

            // loop until boxCircuits has only 1 
            while (boxCircuits.Where(x => x.Count() > 0).Count() != 1 || boxes.Where(x => x.CircuitIndex == -1).Count() > 0 )
            {
                Pair pair = pairs[idx];
                if (boxes[pair.IndexP].CircuitIndex < 0 && boxes[pair.IndexQ].CircuitIndex < 0) // either neither box is in a circuit
                {
                    int numCircuits = boxCircuits.Count();
                    boxes[pair.IndexP].CircuitIndex = numCircuits;
                    boxes[pair.IndexQ].CircuitIndex = numCircuits;
                    List<Box> newCircuit = [boxes[pair.IndexP], boxes[pair.IndexQ]];
                    boxCircuits.Add(newCircuit);
                    circuitsConnected++;
                }
                else if (boxes[pair.IndexP].CircuitIndex < 0 && boxes[pair.IndexQ].CircuitIndex >= 0) //  one is and one isn't
                {
                    boxes[pair.IndexP].CircuitIndex = boxes[pair.IndexQ].CircuitIndex;
                    boxCircuits[boxes[pair.IndexQ].CircuitIndex].Add(boxes[pair.IndexP]);
                    circuitsConnected++;
                }
                else if (boxes[pair.IndexQ].CircuitIndex < 0 && boxes[pair.IndexP].CircuitIndex >= 0) //  one is and one isn't
                {
                    boxes[pair.IndexQ].CircuitIndex = boxes[pair.IndexP].CircuitIndex;
                    boxCircuits[boxes[pair.IndexP].CircuitIndex].Add(boxes[pair.IndexQ]);
                    circuitsConnected++;
                }
                else if (boxes[pair.IndexP].CircuitIndex >= 0 && boxes[pair.IndexQ].CircuitIndex >= 0) // both in a circuit already
                {
                    // if different circuits...
                    if (boxes[pair.IndexP].CircuitIndex != boxes[pair.IndexQ].CircuitIndex)
                    {
                        List<Box> pCircuit = boxCircuits[boxes[pair.IndexP].CircuitIndex];
                        List<Box> qCircuit = boxCircuits[boxes[pair.IndexQ].CircuitIndex];

                        List<Box> merged = pCircuit.Concat(qCircuit).ToList();

                        boxCircuits[boxes[pair.IndexP].CircuitIndex].Clear();
                        boxCircuits[boxes[pair.IndexQ].CircuitIndex].Clear();

                        boxCircuits.Add(merged);
                        int idxLastCircuit = boxCircuits.Count() - 1;
                        int numBoxesInLastCircuit = boxCircuits[idxLastCircuit].Count();
                        for (int j = 0; j < numBoxesInLastCircuit; j++)
                        {
                            Box currBox = boxCircuits[idxLastCircuit][j];
                            boxes[currBox.Loc].CircuitIndex = idxLastCircuit;
                        }
                    }
                    circuitsConnected++; // connect a circuit EVEN IF they're already within the same circuit
                }
                idx++;
                product = boxes[pair.IndexP].X * boxes[pair.IndexQ].X;
            }

            return product;
        }
    }
}
