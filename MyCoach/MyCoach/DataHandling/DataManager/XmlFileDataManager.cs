using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace MyCoach.DataHandling.DataManager
{
    public class XmlFileDataManager : DataManagerBase, IDataManager
    {
        public XmlFileDataManager(IXmlFileReaderWriter xmlFileReaderWriter) : base(xmlFileReaderWriter) { }

        /// <summary>
        ///     Pfad zum Laden und Speichern des gesamten Buffers.
        /// </summary>
        public string SaveFileDirectory { get; set; }

        public ObservableCollection<T> GetDataTransferObjects<T>() where T : IDataTransferObject
        {
            switch (typeof(T).Name)
            {
                case nameof(Category):
                    return this.Buffer.Categories as ObservableCollection<T>;
                case nameof(Exercise):
                    return this.Buffer.Exercises as ObservableCollection<T>;
                case nameof(Settings):
                    return this.Buffer.Settings as ObservableCollection<T>;
                case nameof(TrainingSchedule):
                    return this.Buffer.TrainingSchedules as ObservableCollection<T>;
                case nameof(TrainingScore):
                    return this.Buffer.TrainingScores as ObservableCollection<T>;
                default:
                    return new ObservableCollection<T>();
            }
        }

        /// <summary>
        ///     
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTransferObjects"></param>
        /// <returns></returns>
        public bool SetDataTransferObjects<T>(ObservableCollection<T> dataTransferObjects) where T : IDataTransferObject
        {
            switch (dataTransferObjects)
            {
                case ObservableCollection<Category> list:
                    this.Buffer.Categories = list;
                    break;
                case ObservableCollection<Exercise> list:
                    this.Buffer.Exercises = list;
                    break;
                case ObservableCollection<Settings> list:
                    this.Buffer.Settings = list;
                    break;
                case ObservableCollection<TrainingSchedule> list:
                    this.Buffer.TrainingSchedules = list;
                    break;
                case ObservableCollection<TrainingScore> list:
                    this.Buffer.TrainingScores = list;
                    break;
            }

            return this.TrySaveBufferToFile();
        }

        /// <summary>
        ///     Läd einen Übungssatz aus einer Datei unter dem angegebenen Pfad und speichert alle Daten.
        /// </summary>
        public override bool TryImportExerciseSet(string exerciseSetPath)
        {
            if (base.TryImportExerciseSet(exerciseSetPath))
            {
                var success = this.TrySaveBufferToFile();
                if (success == false)
                {
                    this.ErrorMessageExerciseSetImport = "Der Übungssatz wurde erfolgreich importiert," +
                        $"jedoch konnten die Änderungen am Übungssatz nicht gespeichert werden: { this.ErrorMessageSaving }";
                }

                return success;
            }
            else
            {
                return false;
            }
        }

        protected override bool TryInitialLoading()
        {
            try
            {
                this.SaveFileDirectory = Path.GetDirectoryName(Assembly.GetE­xecutingAssem­bly().CodeBase);
                this.SaveFileDirectory = this.SaveFileDirectory.Replace("file:\\", string.Empty);
                XmlSerializer serializer = new XmlSerializer(typeof(DtoCollection));
                var readXml = this.XmlFileReaderWriter.ReadXmlFromFile(this.SaveFileDirectory + "\\Save.xml");
                using (StringReader sr = new StringReader(readXml))
                {
                    this.Buffer = (DtoCollection)serializer.Deserialize(sr);
                }

                return true;
            }
            catch
            {
                // ToDo: Erweiterte Fehlermeldung
                this.ErrorMessageInitialLoading = "Laden fehlgeschlagen.";
                return false;
            }
        }

        private bool TrySaveBufferToFile()
        {
            try
            {
                this.SaveFileDirectory = Path.GetDirectoryName(Assembly.GetE­xecutingAssem­bly().CodeBase);
                this.SaveFileDirectory = this.SaveFileDirectory.Replace("file:\\", string.Empty);
                XmlSerializer serializer = new XmlSerializer(typeof(DtoCollection));
                using (StringWriter sw = new StringWriter())
                {
                    serializer.Serialize(sw, this.Buffer);
                    this.XmlFileReaderWriter.WriteXmlToFile(sw.ToString(), this.SaveFileDirectory + "\\Save.xml");
                }

                return true;
            }
            catch
            {
                // ToDo: Erweiterte Fehlermeldung
                this.ErrorMessageSaving = "Speichern fehlgeschlagen";
                return false;
            }
        }
    }
}