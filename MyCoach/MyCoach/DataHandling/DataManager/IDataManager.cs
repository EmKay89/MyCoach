using MyCoach.DataHandling.DataTransferObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyCoach.DataHandling.DataManager
{
    public interface IDataManager
    {
        string ErrorMessageExerciseSetExport { get; }

        string ErrorMessageExerciseSetImport { get; }

        string ErrorMessageInitialLoading { get; }

        string ErrorMessageSaving { get; }

        ObservableCollection<T> GetDataTransferObjects<T>() where T : IDataTransferObject;

        void SetDefaults<T>() where T : IDataTransferObject;

        bool SetDataTransferObjects<T>(ObservableCollection<T> dataTransferObjects) where T : IDataTransferObject;

        bool TryExportExerciseSet(string path);

        bool TryImportExerciseSet(string path);
    }
}
