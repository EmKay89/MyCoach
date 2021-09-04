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
        public void Start()
        {

        }

        public void Finish()
        {
            TrainingEvaluator.Evaluate(this);
            this.Clear();
        }
    }
}
