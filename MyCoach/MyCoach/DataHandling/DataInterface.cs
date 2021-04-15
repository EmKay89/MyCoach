using MyCoach.DataHandling.DataManager;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyCoach.DataHandling
{
    /// <summary>
    ///     Zentrale Stelle zum Laden und Speichern von Daten sowie dem Export und Import von Übungssätzen (Singleton).
    /// </summary>
    public class DataInterface
    {
        private static DataInterface instance;
        private static IDataManager dataManager;

        private DataInterface(IDataManager manager)
        {
            dataManager = manager;
        }

        public string ErrorAfterInitialLoading => dataManager.ErrorMessageInitialLoading;

        public string ErrorMessageSaving => dataManager.ErrorMessageSaving;

        public static DataInterface GetInstance()
        {
            if(instance == null)
            {
                instance = new DataInterface(dataManager ?? new XmlFileDataManager(new XmlFileReaderWriter()));
            }

            return instance;
        }

        public static void SetDataManager(IDataManager manager)
        {
            dataManager = manager;
        }

        public ObservableCollection<T> GetData<T>() where T : IDataTransferObject
        {
            return dataManager.GetData<T>();
        }

        public void SetDefaults<T>() where T : IDataTransferObject
        {
            dataManager.SetDefaults<T>();
        }

        public bool SaveData<T>() where T : IDataTransferObject
        {
            return dataManager.SaveData<T>();
        }

        public bool ExportExerciseSet(string path)
        {
            return dataManager.TryExportExerciseSet(path);
        }

        public bool ImportExerciseSet(string path)
        {
            return dataManager.TryImportExerciseSet(path);
        }
    }
}
