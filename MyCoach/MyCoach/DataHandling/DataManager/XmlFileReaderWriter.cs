using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyCoach.DataHandling.DataManager
{
    /// <summary>
    ///     Kapselt das Lesen und Schreiben von XML-Strings aus/in Dateien im XmlFileDataManager unter Implementierung
    ///     eines Interfaces. Das ermöglicht das Mocken von Dateiein- und Ausgaben in Unittests.
    /// </summary>
    public class XmlFileReaderWriter : IXmlFileReaderWriter
    {
        public void WriteXmlToFile(string xmlString, string path)
        {
            using (var writer = new StreamWriter(path, false))
            {
                writer.Write(xmlString);
            }
        }

        public string ReadXmlFromFile(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
