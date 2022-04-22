﻿using MyCoach.ViewModel.Events;
using MyExtensions.IEnumerable;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace MyCoach.ViewModel.TrainingGenerationAndEvaluation
{
    /// <summary>
    ///     Collection of training elements of type <see cref="TrainingElementViewModel"/> that either represent a lap separator
    ///     or exercise.
    /// </summary>
    public class Training : ObservableCollection<TrainingElementViewModel>
    {
        private bool isActive;

        public Training()
        {
            base.CollectionChanged += OnBaseCollectionChanged;
        }

        public event EventHandler TrainingActiveChanged;

        public bool IsActive
        {
            get => isActive;

            private set
            {
                if (value == this.isActive)
                {
                    return;
                }

                this.isActive = value;
                this.TrainingActiveChanged?.Invoke(this, new EventArgs());

                if (this.IsActive)
                {
                    this.Foreach(element => element.Activate());
                }                
            }
        }

        public void Start()
        {
            this.IsActive = true;
        }

        public void Finish()
        {
            this.IsActive = false;
            TrainingEvaluator.Evaluate(this);
            this.Clear();
        }

        private void OnBaseCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    var vm = item as TrainingElementViewModel;

                    if (vm.Type == TrainingElementType.Exercise)
                    {
                        vm.PropertyChanged += this.OnTrainingExerciseChanged;
                        vm.RemoveExerciseFromTrainingExecuted += this.OnRemoveExerciseFromTrainingExecuted;
                    }
                }
            }

            if (e.OldItems == null)
            {
                return;
            }

            foreach (var item in e.OldItems)
            {
                if (item is TrainingElementViewModel vm && vm.Type == TrainingElementType.Exercise)
                {
                    vm.PropertyChanged -= this.OnTrainingExerciseChanged;
                    vm.RemoveExerciseFromTrainingExecuted -= this.OnRemoveExerciseFromTrainingExecuted;
                }
            }
        }
               
        private void OnTrainingExerciseChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TrainingElementViewModel.Completed)
                && this.Where(element => element.Type == TrainingElementType.Exercise).All(element => element.Completed))
            {
                Finish();
            }
        }

        private void OnRemoveExerciseFromTrainingExecuted(object sender, ExerciseEventArgs e)
        {
            if (sender is TrainingElementViewModel vm)
            {
                this.Remove(vm);
            }

            if (this.Any(element => element.Type == TrainingElementType.Exercise) == false)
            {
                this.IsActive = false;
                this.Clear();
            }
        }
    }
}
