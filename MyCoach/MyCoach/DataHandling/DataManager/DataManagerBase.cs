using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyCoach.DataHandling.DataManager
{
    /// <summary>
    ///     Basisklasse für jeden DataManager, implementiert eine Sammlung von DTO's als Buffer, Properties zum Anzeigen
    ///     von Fehlermeldungen sowie Methoden zum Importieren und Exportieren von Übungssätzen in eine XML Datei.
    /// </summary>
    public abstract class DataManagerBase
    {        
        /// <summary>
        ///     Triggert das initiale Laden der Daten in den Buffer.
        /// </summary>
        public DataManagerBase(IXmlFileReaderWriter xmlFileReaderWriter)
        {
            this.XmlFileReaderWriter = xmlFileReaderWriter;

            if (this.TryInitialLoading() == false)
            {
                this.Buffer = DefaultDtos.Collection;
            }            
        }

        /// <summary>
        ///     Fehlermeldung beim Exportieren des Übungssatzes.
        /// </summary>
        public virtual string ErrorMessageExerciseSetExport { get; protected set; }

        /// <summary>
        ///     Fehlermeldung beim Importieren des Übungssatzes.
        /// </summary>
        public virtual string ErrorMessageExerciseSetImport { get; protected set; }

        /// <summary>
        ///     Fehlermeldung beim initialen Laden.
        /// </summary>
        public virtual string ErrorMessageInitialLoading { get; protected set; }

        /// <summary>
        ///     Fehlermeldung beim zuletzt ausgeführten Speichervorgang.
        /// </summary>
        public virtual string ErrorMessageSaving { get; protected set; }

        /// <summary>
        ///     DtoCollection als interner Zwischenspeicher für den Datenaustausch.
        /// </summary>
        protected DtoCollection Buffer { get; set; }

        protected IXmlFileReaderWriter XmlFileReaderWriter { get; set; }

        /// <summary>
        ///     Speichert den aktuell geladenen Übungssatz in eine Datei unter dem angegebenen Pfad und gibt den Erfolg
        ///     der Aktion als boolschen Wert zurück.
        /// </summary>
        public virtual bool TryExportExerciseSet(string path)
        {
            bool success = false;
            ExerciseSet exerciseSet = new ExerciseSet()
            {
                Categories = this.Buffer.Categories,
                Exercises = this.Buffer.Exercises
            };

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(exerciseSet.GetType());
                    serializer.Serialize(writer, exerciseSet);
                    this.XmlFileReaderWriter.WriteXmlToFile(writer.ToString(), path);
                    success = true;
                }
                catch(Exception e)
                {
                    // ToDo: Fehlerfälle erweitern
                    this.ErrorMessageExerciseSetExport = Constants.ExportError;
                }
            }

            return success;
        }

        /// <summary>
        ///     Läd den aktuell geladenen Übungssatz aus einer Datei unter dem angegebenen Pfad und gibt den Erfolg
        ///     der Aktion als boolschen Wert zurück.
        /// </summary>
        public virtual bool TryImportExerciseSet(string path)
        {
            bool success = false;
            StringReader reader = null;
                        
            try
            {
                var xmlString = this.XmlFileReaderWriter.ReadXmlFromFile(path);
                reader = new StringReader(xmlString);
                XmlSerializer serializer = new XmlSerializer(typeof(ExerciseSet));                
                var exerciseSet = (ExerciseSet)(serializer.Deserialize(reader));
                this.Buffer.Categories = exerciseSet.Categories;
                this.Buffer.Exercises = exerciseSet.Exercises;
                success = true;
            }
            catch (Exception e)
            {
                // ToDo: Fehlerfälle erweitern
                this.ErrorMessageExerciseSetExport = Constants.ImportError + e.ToString();
            }
            finally
            {
                reader?.Dispose();
            }

            return success;
        }

        protected abstract bool TryInitialLoading();
    }
}
