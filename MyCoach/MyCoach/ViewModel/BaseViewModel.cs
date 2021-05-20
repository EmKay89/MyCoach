using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public const string SAVING_ERROR_CAPTION = "Speichern fehlgeschlagen";
        public const string SAVING_ERROR_TEXT = "Speichern fehlgeschlagen. Die Änderungen werden beim nächsten Neustart des Programms nicht mehr zur Verfügung stehen.";

        protected void InvokePropertiesChanged(params string[] properties)
        {
            foreach (var property in properties)
            {
                this.InvokePropertyChanged(property);
            }
        }

        protected void InvokePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
