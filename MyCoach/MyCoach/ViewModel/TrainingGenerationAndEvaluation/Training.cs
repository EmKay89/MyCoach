using MyExtensions.IEnumerable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                if (value == isActive)
                {
                    return;
                }

                isActive = value;
                TrainingActiveChanged?.Invoke(this, new EventArgs());

                if (IsActive)
                {
                    this.Foreach(element => element.Activate());
                }                
            }
        }

        public void Start()
        {
            IsActive = true;
        }

        public void Finish()
        {
            IsActive = false;
            TrainingEvaluator.Evaluate(this);
            this.Clear();
        }

        private void OnBaseCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }

            foreach (var item in e.NewItems)
            {
                var vm = item as TrainingElementViewModel;

                if (vm.Type == TrainingElementType.Exercise)
                {
                    vm.PropertyChanged += OnTrainingExerciseChanged;
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
    }
}
