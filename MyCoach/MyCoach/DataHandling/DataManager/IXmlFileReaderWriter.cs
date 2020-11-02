using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.DataHandling.DataManager
{
    public interface IXmlFileReaderWriter
    {
        void WriteXmlToFile(string xmlString, string path);

        string ReadXmlFromFile(string path);
    }
}
