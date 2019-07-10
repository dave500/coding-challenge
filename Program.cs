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

            var gridSize = (ia[0], ia[1]);
            var startPos = (ia[2], ia[3]);
            var startOrientation = ia[4][0];
            var journey = ia[5];

           Console.WriteLine($"gridSize: {gridSize}, startPos: {startPos}, startOrientation: {startOrientation}, journey: {journey}");


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
    }
}
