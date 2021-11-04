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
    /// Collection of training elements that may be of type <see cref="TrainingExerciseViewModel"/> or see <see cref="LapSeparator"/>.
    /// </summary>
    public class Training : ObservableCollection<ITrainingElement>
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
                TrainingActiveChanged.Invoke(this, new EventArgs());
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
                if (item is TrainingExerciseViewModel vm)
                {
                    vm.PropertyChanged += OnTrainingExerciseChanged;
                }
            }
        }

        private void OnTrainingExerciseChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TrainingExerciseViewModel.Completed)
                && this.All(element => element.Completed))
            {
                Finish();
            }
        }
    }
}
