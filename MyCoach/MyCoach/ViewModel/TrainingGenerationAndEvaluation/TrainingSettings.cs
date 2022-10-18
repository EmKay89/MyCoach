﻿using MyCoach.Model.Defines;
using System.Collections.Generic;

namespace MyCoach.ViewModel.TrainingGenerationAndEvaluation
{
    /// <summary>
    ///     DTO that holds all non permanently saved training settings provided by the <see cref="TrainingViewModel"/>.
    /// </summary>
    public class TrainingSettings
    {
        public TrainingSettings(
            TrainingMode trainingMode,
            ushort lapCount,
            ushort exercisesPerLap,
            ushort multiplyer,
            ExerciseCategory? categoryInFocus,
            List<ExerciseCategory> categoriesEnabledForTraining)
        {
            TrainingMode = trainingMode;
            LapCount = lapCount;
            ExercisesPerLap = exercisesPerLap;
            Multiplyer = multiplyer;
            CategoryInFocus = categoryInFocus;
            CategoriesEnabledForTraining = categoriesEnabledForTraining;
        }

        public TrainingMode TrainingMode { get; }

        public ushort LapCount { get; }

        public ushort ExercisesPerLap { get; }

        public ushort Multiplyer { get; }

        public ExerciseCategory? CategoryInFocus { get; }

        public List<ExerciseCategory> CategoriesEnabledForTraining { get; }
    }
}
