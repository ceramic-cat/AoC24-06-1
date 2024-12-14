namespace AoC06_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * What are we doing?
             * - Reading a map into array of arrays
             * - Checking for a guy
             * - Making guy move
             * - Saving where guy is to a list
             * - Checking where he is going
             * - Can he go there? y/n
             * 
             * Until he leaves the map
             * 
             * 
             */


            string[] indata = File.ReadAllLines("map.txt");
            Console.WriteLine(indata[0]);

            Map newMap = new Map(indata);

            Console.WriteLine(newMap.GuardCoordinates.ToString());
            Console.WriteLine(newMap.FacingDirection);

            while (!newMap.ReachedEdge())
            {
                if (newMap.NoObstaclesInFront())
                {
                    newMap.Step();
                }
                else {
                    newMap.Turn();
                }
            }
            //Last step outside the map
            newMap.Step();

            Console.WriteLine($"Visited locations: {newMap.CountLocations()}");



        }


        public enum Direction
        {
            Up,
            Right,
            Down,
            Left
        }

        class Map
        {
            public int[] GuardCoordinates { get; set; }
            public Direction FacingDirection { get; set; }
            public List<int[]> VisitedLocations { get; set; }
            public List<List<char>> MapData { get; set; }
            public int Length { get; set; }

            public Map(string[] inputData)
            {
                FacingDirection = Direction.Up;

                List<List<char>> mapData = new List<List<char>>();

                List<int[]> _visitedLocations = new List<int[]>();


                for (int i = 0; i < inputData.Length; i++)
                {
                    mapData.Add(inputData[i].ToList());
                }

                for (int i = 0; i < mapData.Count; i++)
                {
                    for (int j = 0; j < mapData[i].Count; j++)
                    {
                        if (mapData[i][j] == '^')
                        {
                            GuardCoordinates = [i, j];
                        }
                    }
                }

                _visitedLocations.Add(GuardCoordinates);
                VisitedLocations = _visitedLocations;
                MapData = mapData;
                Length = MapData[0].Count;
            }

            public Direction Next()
            {
                Direction direction = Direction.Up;
                switch (this.FacingDirection)
                {
                    case Direction.Left:
                        return Direction.Up;
                    case Direction.Up:
                        return Direction.Right;
                    case Direction.Right:
                        return Direction.Down;
                    case Direction.Down:
                        return Direction.Left;
                }
                return direction;
            }
            public bool ReachedEdge()
            {
                int[] co = CoordinatesInFront();
                if (co[0] < 0 || co[1] < 0 || co[0] >= this.Length || co[1] >= this.Length)
                    return true;
                else return false;

            }

            public void Turn()
            {
                this.FacingDirection = Next();
            }

            public char ReadMap(int[] coord)
            {
                int x = coord[0];
                int y = coord[1];
                char c = this.MapData[x][y];
                return c;
            }

            public bool NoObstaclesInFront()
            {
                bool isFreeInFront = false;
                int[] co = CoordinatesInFront();

                if (this.ReadMap(co) == '#')
                {
                    isFreeInFront = false;
                }
                else
                {
                    isFreeInFront = true;
                }

                return isFreeInFront;

            }

            public void Step()
            {
                // Make a footstep
                int x = this.GuardCoordinates[0];
                int y = this.GuardCoordinates[1];
                this.MapData[x][y] = 'X';

                GuardCoordinates = CoordinatesInFront();
                
                VisitedLocations.Add(GuardCoordinates);


            }
            public int CountLocations()
            {
                int count = 0; 
                foreach (List<char> list in this.MapData)
                {
                    foreach(char c in list)
                    {
                        if (c == 'X')
                            count++;
                    }
                }
                return count;
            }

            public int[] CoordinatesInFront()
            {
                int[] coord = this.GuardCoordinates.ToArray();

                switch (this.FacingDirection)
                {
                    case Direction.Up:
                        coord[0] = coord[0] - 1;
                        break;
                    case Direction.Right:
                        coord[1] = coord[1] + 1;
                        break;
                    case Direction.Down:
                        coord[0] = coord[0] + 1;
                        break;
                    case Direction.Left:
                        coord[1] = coord[1] - 1;
                        break;

                    default:
                        Console.WriteLine("Something is not right here");
                        break;

                }
                return coord;
            }

        }
    }
}
