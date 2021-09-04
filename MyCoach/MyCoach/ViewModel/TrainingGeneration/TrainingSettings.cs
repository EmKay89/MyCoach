using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.TrainingGeneration
{
    /// <summary>
    ///     DTO that holds all information to create a <see cref="Training"/> using the <see cref="TrainingGenerator"/>.
    /// </summary>
    public class TrainingSettings
    {
        public TrainingSettings(
            TrainingMode trainingMode,
            ushort lapCount,
            ushort exercisesPerLap, Category
            categoryInFocus,
            List<Category> categoriesEnabledForTraining)
        {
            this.TrainingMode = trainingMode;
            this.LapCount = lapCount;
            this.ExercisesPerLap = exercisesPerLap;
            this.CategoryInFocus = categoryInFocus;
            this.CategoriesEnabledForTraining = categoriesEnabledForTraining;
        }

        public TrainingMode TrainingMode { get; }

        public ushort LapCount { get; }

        public ushort ExercisesPerLap { get; }

        public Category CategoryInFocus { get; }

        public List<Category> CategoriesEnabledForTraining { get; }
    }
}
