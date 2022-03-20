using MyCoach.Model.DataTransferObjects;
using System.Collections.ObjectModel;

namespace MyCoach.DataHandling.DataManager
{
    public interface IDataManager
    {
        string ErrorMessageExerciseSetExport { get; }

        string ErrorMessageExerciseSetImport { get; }

        string ErrorMessageInitialLoading { get; }

        string ErrorMessageSaving { get; }

        ObservableCollection<T> GetData<T>() where T : IDataTransferObject;

        void SetDefaults<T>() where T : IDataTransferObject;

        bool SaveData<T>() where T : IDataTransferObject;

        bool TryExportExerciseSet(string path);

        bool TryImportExerciseSet(string path);
    }
}
