using MyCoach.ViewModel.TrainingEvaluation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.TrainingGeneration
{
    public class Training : ObservableCollection<TrainingExerciseViewModel>
    {
        private bool isActive;

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

                isActive = value;
                this.TrainingActiveChanged.Invoke(this, new EventArgs());
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
    }
}
