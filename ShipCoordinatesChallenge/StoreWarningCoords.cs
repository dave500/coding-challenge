using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ShipCoordinatesChallenge
{
    public static class StoreWarningCoords
    {

        public static void CheckWarningCoords(WarningCoords wc)
        {
            if (File.Exists(@"C:\Dev\store.xml"))
            {
                using (FileStream fs = new FileStream(@"C:\Dev\store.xml", FileMode.Open))
                {
                    XmlSerializer _xSer = new XmlSerializer(typeof(List<WarningCoords>));

                    var listWarningCoords = (List<WarningCoords>)_xSer.Deserialize(fs);

                    listWarningCoords.Add(wc);
                    StoreWarningCoords.SerializeAndWriteList(listWarningCoords);
                }
            }
            else
            {

                var listWarningCoords = new List<WarningCoords>();

                listWarningCoords.Add(wc);
                StoreWarningCoords.SerializeAndWriteList(listWarningCoords);
            }
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
