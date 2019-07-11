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

            bool success = false;

            /// OK Now make some moves
            for (var i = 0; i < journey.Length; i++)
            {
                (current, success) = Step(current, i, journey);

                if (!success)
                {
                    break;
                }
            }


            Console.ReadLine();
        }

        private static (Current, bool) Step(Current current, int i, string journey)
        {
            // Get direction of travel
            char movementKey = journey[i];
            if (!movements.ContainsKey(movementKey))
            {
                throw new InvalidOperationException($"Invalid movement: {movementKey}");
            }

            current.Orientation = movementKey.ToString();

            current.OrientationIndex += movements[movementKey].Item1;
            var move = movements[movementKey].Item2;


            if (move)
            {
                var (newX, newY) = Orientations(current.Postion.Item1, current.Postion.Item2, current.OrientationIndex);

                if (newX < 0 || newX > current.GridSize.X || newY < 0 || newY > current.GridSize.Y)
                {
                    Console.WriteLine("Gone over the edge!");
                    return (current, false);
                }

                Console.WriteLine($"Movement: {movementKey}, Current position: {current.Postion}, Orientation: {current.OrientationIndex}, Move: {move}");
                current.Postion = (newX, newY);

            }

            return (current, true);
        }

        private static  (int, int) Orientations(int x, int y, int orientation)
        {
            
            switch (orientation)
            {
                case 0:
                    y = y + 1;
                    return (x, y);
                case 1:
                    x = x + 1;
                    return (x, y);
                case 2:
                    y = y - 1;
                    return (x, y);
                case 3:
                    x = x - 1;
                    return (x, y);
                default:
                    break;
            }

            throw new NotImplementedException();

        }

        /// <summary>
        /// Movements Dictionaty based on movement char 
        /// returns numeric rotation and bool to represent if a Step
        /// takes place
        /// </summary>
        private static readonly IDictionary<char, (int, bool)> movements = new Dictionary<char, (int, bool)>
        {
            {'F', (0, true) },
            {'R', (1, false) },
            {'L', (-1, false)},
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
            public int OrientationIndex { get; set; }
            public (int, int) Postion { get; set; }
            public (int X, int Y) GridSize { get; set; }
        }

        private static char[] orientationKeys = new[] { 'N', 'E', 'S', 'W' };
    }
}
