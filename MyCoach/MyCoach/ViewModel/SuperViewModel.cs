using MyCoach.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyCoach.ViewModel
{
    /// <summary>
    ///     Basisklasse für ein übergeordnetes ViewModel, das in seiner Property <see cref="SelectedViewModel"/> ein anderes
    ///     ViewModel halten kann und dieses über ein <see cref="UpdateViewCommand"/> tauschen kann.
    /// </summary>
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
                if (this.selectedViewModel == value)
                {
                    return;
                }

                selectedViewModel = value;
                this.InvokePropertyChanged();
            }
        }

        public ICommand UpdateViewCommand { get; set; }
    }
}
