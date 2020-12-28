using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModels
{
    public class MainViewModel : SuperViewModel
    {
        public MainViewModel() : base()
        {
            this.SelectedViewModel = new TrainingViewModel();
        }
    }
}
