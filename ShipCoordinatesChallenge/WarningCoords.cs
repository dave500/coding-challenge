using System;
using System.Collections.Generic;
using System.Text;

namespace ShipCoordinatesChallenge
{
    [Serializable]
    public class WarningCoords
    { 
       public int OrientationIndex { get; set; }
       public (int, int) Postion { get; set; }
        
    }
}
