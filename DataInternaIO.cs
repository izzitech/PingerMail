using System;
using System.IO;
using System.Xml.Serialization;

namespace PingerMail {
    public static class DataInternaIO {
        public static DataInterna dataInterna = new DataInterna();
        static Type[] Tipos = { typeof(DataInterna), typeof(Terminal), typeof(Mail) };
        static XmlSerializer serializer = new XmlSerializer(typeof(DataInterna), Tipos);
        static string location = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        static string xmlName = "\\DataInterna.xml";

        public static void write() {
            FileStream fs;
            fs = new FileStream(location + xmlName, FileMode.Create);
            serializer.Serialize(fs, dataInterna);
            fs.Close();
        }

        public static void read() {
            if (File.Exists(location + xmlName)) {
                FileStream fs;
                fs = new FileStream(location + xmlName, FileMode.Open);
                dataInterna = (DataInterna)serializer.Deserialize(fs);
                fs.Close();
            }
        }
    }
}

