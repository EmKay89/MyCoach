using MyCoach.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModels
{
    public abstract class SuperViewModel : BaseViewModel
    {
        private BaseViewModel selectedViewModel;

        public SuperViewModel()
        {
            this.UpdateViewCommand = new UpdateViewCommand(this);
        }

        public BaseViewModel SelectedViewModel
        {
            get
            {
                return selectedViewModel;
            }

            set
            {
                selectedViewModel = value;
            }
        }

        public ICommand UpdateViewCommand { get; set; }
    }
}
