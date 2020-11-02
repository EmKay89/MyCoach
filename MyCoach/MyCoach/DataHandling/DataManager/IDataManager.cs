using MyCoach.DataHandling.DataTransferObjects;
using System.Collections.Generic;

namespace MyCoach.DataHandling.DataManager
{
    public interface IDataManager
    {
        string ErrorMessageExerciseSetExport { get; }

        string ErrorMessageExerciseSetImport { get; }

        string ErrorMessageInitialLoading { get; }

        string ErrorMessageSaving { get; }

        List<T> GetDataTransferObjects<T>() where T : IDataTransferObject;
        
        bool SetDataTransferObjects<T>(List<T> dataTransferObjects) where T : IDataTransferObject;

        bool TryExportExerciseSet(string path);

        bool TryImportExerciseSet(string path);
    }
}
