using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyCoachTests.DataHandling.DataManager
{
    public class XmlFileReaderWriterMock : IXmlFileReaderWriter
    {
        public string InnerXml { get; set; }
        public string Path { get; set; }
        public Exception Exception { get; set; }

        public string ReadXmlFromFile(string path)
        {
            if(this.Exception != null)
            {
                throw this.Exception;
            }
                        
            return this.InnerXml;
        }

        public void WriteXmlToFile(string xmlString, string path)
        {
            if (this.Exception != null)
            {
                throw this.Exception;
            }

            this.Path = path;
            this.InnerXml = xmlString;
        }

        public void SetUpInnerXml(object collection)
        {
            string dtos;
            XmlSerializer serializer;

            switch (collection)
            {
                case ExerciseSet _:
                    serializer = new XmlSerializer(typeof(ExerciseSet));
                    break;
                default:
                    serializer = new XmlSerializer(typeof(DtoCollection));
                    break;
            }

            using (StringWriter sw = new StringWriter())
            {
                serializer.Serialize(sw, collection);
                dtos = sw.ToString();
            }

            this.InnerXml = dtos;
        }
    }
}
