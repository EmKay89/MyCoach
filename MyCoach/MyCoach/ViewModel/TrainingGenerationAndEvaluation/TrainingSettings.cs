﻿using MyCoach.DataHandling.DataTransferObjects;
using MyCoach.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.TrainingGenerationAndEvaluation
{
    /// <summary>
    ///     DTO that holds all information to create a <see cref="Training"/> using the <see cref="TrainingGenerator"/>.
    /// </summary>
    public class TrainingSettings
    {
        public TrainingSettings(
            TrainingMode trainingMode,
            ushort lapCount,
            ushort exercisesPerLap,
            ExerciseCategory categoryInFocus,
            List<ExerciseCategory> categoriesEnabledForTraining)
        {
            TrainingMode = trainingMode;
            LapCount = lapCount;
            ExercisesPerLap = exercisesPerLap;
            CategoryInFocus = categoryInFocus;
            CategoriesEnabledForTraining = categoriesEnabledForTraining;
        }

        public TrainingMode TrainingMode { get; }

        public ushort LapCount { get; }

        public ushort ExercisesPerLap { get; }

        public ExerciseCategory CategoryInFocus { get; }

        public List<ExerciseCategory> CategoriesEnabledForTraining { get; }
    }
}