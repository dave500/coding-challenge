using System;
using System.Collections.Generic;

namespace ShipCoordinatesChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the 6 Args of Your Journey  ");

            string input = Console.ReadLine();
            string[] ia = input.Split(' ');

            if (ia.Length != 6)
            {
                throw new InvalidOperationException("Expected 6 arguments like:\n5 3 1 1 E RFRFRF");
            }

            var gridSize = validateGridSize(ia[0], ia[1]);
            var startPos = validateStartPos(ia[2], ia[3]);
            var startOrientation = ia[4];
            var journey = ia[5];

            Console.WriteLine($"gridSize: {gridSize}, startPos: {startPos}, startOrientation: {startOrientation}, journey: {journey}");

            var current = new Current
            {
                Orientation = startOrientation,
                Postion = startPos,
                GridSize = gridSize
            };

            /// OK Now make some moves
            for (var i = 0; i < journey.Length; i++)
            {
                var success = Step(current, i, journey);

                if (!success)
                {
                    break;
                }
            }


            Console.ReadLine();
        }

        private static bool Step(Current current, int i, string journey)
        {
            throw new NotImplementedException();
        }

        private static  (int, int) Orientations(int x, int y)
        {
            // returns y & x+ 1 etc

            throw new NotImplementedException();

        }

        private static readonly IDictionary<char, int> movements = new Dictionary<char, int>
        {
            {'F', 0 },
            {'R', 1 },
            {'L', 2},
        };

        private static (int, int) validateGridSize(string y, string x)
        {
            int retY = int.TryParse(y, out var numY) ? numY : throw new InvalidOperationException("expected int for y axis of grid");
            int retX = int.TryParse(x, out var numX) ? numX : throw new InvalidOperationException("expected int for x axis of grid");

            return (retY, retX);
        }

        private static (int, int) validateStartPos(string y, string x)
        {
            int retY = int.TryParse(y, out var numY) ? numY : throw new InvalidOperationException("expected int for y axis start Pos");
            int retX = int.TryParse(x, out var numX) ? numX : throw new InvalidOperationException("expected int for x axis start Pos");

            return (retY, retX);
        }

        public struct Current
        {
            public string Orientation { get; set; }
            public (int, int) Postion { get; set; }
            public (int X, int Y) GridSize { get; set; }
        }

        private static char[] orientationKeys = new[] { 'N', 'E', 'S', 'W' };
    }
}
