﻿using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.DataTransferObjects.CollectionExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        ///     Fehlermeldung beim Exportieren des Trainings.
        /// </summary>
        public virtual string ErrorMessageTrainingExport { get; protected set; }

        /// <summary>
        ///     Fehlermeldung beim Importieren des Trainings.
        /// </summary>
        public virtual string ErrorMessageTrainingImport { get; protected set; }

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
        ///     Setzt die Werte eines DataTransferObjects im Buffer auf die Standardwerte zurück
        /// </summary>
        /// <typeparam name="T">Der Typ des DataTransferObjects.</typeparam>
        public virtual void SetDefaults<T>() where T : IDataTransferObject
        {
            switch (typeof(T).Name)
            {
                case nameof(Category):
                    this.Buffer.Categories.ResetSubscriptions();
                    this.Buffer.Categories.Clear();
                    foreach (var category in DefaultDtos.Categories)
                    {
                        this.Buffer.Categories.Add(category);
                    }

                    break;
                case nameof(Exercise):
                    this.Buffer.Exercises.ResetSubscriptions();
                    this.Buffer.Exercises.Clear();
                    foreach (var exercise in DefaultDtos.Exercises)
                    {
                        this.Buffer.Exercises.Add(exercise);
                    }

                    break;
                case nameof(Settings):
                    DefaultDtos.Settings.First().CopyValuesTo(this.Buffer.Settings.First());
                    break;

                case nameof(TrainingSchedule):
                    DefaultDtos.TrainingSchedules.First().CopyValuesTo(this.Buffer.TrainingSchedules.First());
                    break;

                case nameof(Month):
                    this.Buffer.Months.ResetSubscriptions();
                    this.Buffer.Months.Clear();
                    foreach (var ts in DefaultDtos.Months)
                    {
                        this.Buffer.Months.Add(ts);
                    }

                    break;
            }
        }

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
                catch(Exception)
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
                        
            try
            {
                ExerciseSet exerciseSet;
                var xmlString = this.XmlFileReaderWriter.ReadXmlFromFile(path);

                using (StringReader reader = new StringReader(xmlString))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ExerciseSet));
                    exerciseSet = (ExerciseSet)(serializer.Deserialize(reader));
                }

                this.Buffer.Categories.ResetSubscriptions();
                this.Buffer.Categories.Clear();
                foreach (var category in exerciseSet.Categories)
                {
                    this.Buffer.Categories.Add(category);
                }

                this.Buffer.Exercises.ResetSubscriptions();
                this.Buffer.Exercises.Clear();
                foreach (var exercise in exerciseSet.Exercises)
                {
                    this.Buffer.Exercises.Add(exercise);
                }

                success = true;
            }
            catch (Exception e)
            {
                // ToDo: Fehlerfälle erweitern
                this.ErrorMessageExerciseSetExport = Constants.ImportError + e.ToString();
            }

            return success;
        }

        public virtual bool TryExportTraining(string path, List<TrainingElement> training)
        {
            bool success = false;

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(training.GetType());
                    serializer.Serialize(writer, training);
                    this.XmlFileReaderWriter.WriteXmlToFile(writer.ToString(), path);
                    success = true;
                }
                catch (Exception)
                {
                    // ToDo: Fehlerfälle erweitern
                    this.ErrorMessageTrainingExport = Constants.ExportError;
                }
            }

            return success;
        }

        public virtual bool TryImportTraining(string path, out List<TrainingElement> training)
        {
            bool success = false;
            training = null;

            try
            {                
                var xmlString = this.XmlFileReaderWriter.ReadXmlFromFile(path);
                using (StringReader reader = new StringReader(xmlString))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<TrainingElement>));
                    training = (List<TrainingElement>)(serializer.Deserialize(reader));
                }

                success = true;
            }
            catch (Exception e)
            {
                // ToDo: Fehlerfälle erweitern
                this.ErrorMessageTrainingImport = Constants.ImportError + e.ToString();
            }

            return success;
        }

        protected abstract bool TryInitialLoading();
    }
}
