using MyCoach.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoachTests.ViewModel
{
    public abstract class ViewModelTestBase : DataInterfaceTestBase
    {
        protected List<string> PropertyChangedEvents;

        protected new void Initialize()
        {
            this.PropertyChangedEvents = new List<string>();
            base.Initialize();
        }

        protected virtual void OnSutPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PropertyChangedEvents.Add(e.PropertyName);
        }
    }
}
