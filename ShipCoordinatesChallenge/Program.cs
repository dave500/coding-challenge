using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ShipCoordinatesChallenge
{
    class Program
    {

        // Test Input strings
        // 5 3 1 1 E RFRFRFRF
        // 5 3 3 2 N FRRFLLFFRRFLL
        // 5 3 0 3 W LLFFFLFLFL

        static void Main(string[] args)
        {
            List<WarningCoords> warnings = new List<WarningCoords>();

           
                Console.WriteLine("Enter the 6 Args of Your Journey  ");

                string input = Console.ReadLine().ToUpper();
                string[] ia = input.Split(' ');

                if (ia.Length != 6)
                {
                    throw new InvalidOperationException("Expected 6 arguments like:\n5 3 1 1 E RFRFRFRF");
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

                // Insure OrientationIndex is set correctly first time out
                // TODO Better way to do this for 1 char
                char[] o = startOrientation.ToCharArray();
                current.OrientationIndex = Array.IndexOf(orientationKeys, o[0]);

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

            // 3rd test Input string breaks this. Limit to range 0 - 3
            // TODO Extract to method
            current.OrientationIndex += movements[movementKey].Item1;
            current.OrientationIndex = (current.OrientationIndex < 0) ? 3 : current.OrientationIndex;
            current.OrientationIndex = (current.OrientationIndex > 3) ? 0 : current.OrientationIndex;

            var move = movements[movementKey].Item2;

            if (move)
            {
                var (newX, newY) = Orientations(current.Postion.Item1, current.Postion.Item2, current.OrientationIndex);

                if (newX < 0 || newX > current.GridSize.X || newY < 0 || newY > current.GridSize.Y)
                {
                    //Check / Store Warning Details
                    WarningCoords wc = new WarningCoords();
                    wc.OrientationIndex = current.OrientationIndex;
                    wc.Postion = current.Postion;
                    bool warningRecieved = StoreWarningCoords.CheckWarningCoords(wc);

                    if (!warningRecieved)
                    {
                        Console.WriteLine($"Current position: {current.Postion}, {orientationKeys[current.OrientationIndex]}, LOST");
                        return (current, false);
                    }
                    else
                    {
                        Console.WriteLine($"Movement: {movementKey}, Current position: {current.Postion}, Orientation: {current.OrientationIndex}, Move: {move}, ALERT");
                        return (current, true);
                    }
                }

                current.Postion = (newX, newY);
                Console.WriteLine($"Movement: {movementKey}, Current position: {current.Postion}, Orientation: {current.OrientationIndex}, Move: {move}");
               

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
