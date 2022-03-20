namespace MyCoach.DataHandling.DataManager
{
    public interface IXmlFileReaderWriter
    {
        void WriteXmlToFile(string xmlString, string path);

        string ReadXmlFromFile(string path);
    }
}
