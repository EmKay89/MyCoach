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
    /// <summary>
    ///     DataManager, der seine Daten in einer XML Speicherdatei zwischenspeichert bzw. sie aus dieser läd.
    /// </summary>
    public class XmlFileDataManager : DataManagerBase, IDataManager
    {
        public XmlFileDataManager(IXmlFileReaderWriter xmlFileReaderWriter) : base(xmlFileReaderWriter) { }

        /// <summary>
        ///     Pfad zum Laden und Speichern des gesamten Buffers.
        /// </summary>
        public string SaveFileDirectory { get; set; }

        /// <summary>
        ///     Gibt die im Buffer gespeicherten DataTransferObjects eines gewählten Typs als ObservableCollection zurück.
        /// </summary>
        /// <typeparam name="T">Der Typ des DataTransferObjects.</typeparam>
        /// <returns>Die ObservableCollection des gewählten Typs.</returns>
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
        ///     Setzt die Werte eines DataTransferObjects im Buffer auf die Standardwerte zurück und speichert dann den gesamten Buffer
        ///     in die Speicherdatei.
        /// </summary>
        /// <typeparam name="T">Der Typ des DataTransferObjects.</typeparam>
        public override void SetDefaults<T>()
        {
            base.SetDefaults<T>();
            this.TrySaveBufferToFile();
        }

        /// <summary>
        ///     Speichert eine ObservableCollection eines DataTransferObjects im Buffer und speichert dann den gesamten Buffer
        ///     in die Speicherdatei.
        /// </summary>
        /// <typeparam name="T">Der Typ des DataTransferObjects.</typeparam>
        /// <param name="dataTransferObjects">Die ObservableCollection des DataTransferObjects.</param>
        /// <returns>True, wenn das Speichern erfolgreich war, andernfalls false.</returns>
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

        /// <summary>
        ///     Führt das initiale Laden der gespeicherten Daten aus der Speicherdatei aus und füllt den internen Buffer
        ///     mit den geladenen Daten.
        /// </summary>
        /// <returns>True, wenn die Daten erfolgreich geladen werden konnten, ansonsten false.</returns>
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