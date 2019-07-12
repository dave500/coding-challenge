using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ShipCoordinatesChallenge
{
    public static class StoreWarningCoords
    {

        public static bool CheckWarningCoords(WarningCoords wc)
        {
            List<WarningCoords> deserializedWarningCoords = new List<WarningCoords>();
            bool alreadyInList = false;

            if (!Directory.Exists(@"C:\Dev"))
            {
                throw new Exception(@"Directory Does Not Exist Please add C:\Dev");
            }

            if (File.Exists(@"C:\Dev\store.xml"))
            {
                using (FileStream fs = new FileStream(@"C:\Dev\store.xml", FileMode.Open))
                {
                    XmlSerializer _xSer = new XmlSerializer(typeof(List<WarningCoords>));
                    var listWarningCoords = (List<WarningCoords>)_xSer.Deserialize(fs);
                   
                    // Upgrade to c# 7.3 to allow Tuple match in this context...
                    alreadyInList = listWarningCoords.Any(item => item.OrientationIndex == wc.OrientationIndex && item.Postion.Item1 == wc.Postion.Item1 && item.Postion.Item2 == wc.Postion.Item2);
                    deserializedWarningCoords = listWarningCoords;
                }

                if (!alreadyInList)
                {
                    deserializedWarningCoords.Add(wc);
                    SerializeAndWriteList(deserializedWarningCoords);
                }
            }
            else
            {

                var listWarningCoords = new List<WarningCoords>();

                listWarningCoords.Add(wc);
                StoreWarningCoords.SerializeAndWriteList(listWarningCoords);
            }

            return alreadyInList;
        }




        public static void SerializeAndWriteList(List<WarningCoords> listWarningCoords)
        {
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(List<WarningCoords>));
            using (FileStream fs = new FileStream(@"C:\Dev\store.xml", FileMode.OpenOrCreate))
            {
                xs.Serialize(fs, listWarningCoords);
            }
        }
    }
}
