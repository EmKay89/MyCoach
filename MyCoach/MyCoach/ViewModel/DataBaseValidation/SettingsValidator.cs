using MyCoach.DataHandling;
using MyCoach.DataHandling.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.DataBaseValidation
{
    public class SettingsValidator
    {
        public static void Validate()
        {
            var settings = DataInterface.GetInstance().GetDataTransferObjects<Settings>();

            if (settings == null)
            {
                settings = new ObservableCollection<Settings>();
            }

            if (settings.Any() == false)
            {
                settings.Add(new Settings());
            }

            var dublicates = settings.Skip(1);

            foreach (var setting in dublicates)
            {
                settings.Remove(setting);
            }

            DataInterface.GetInstance().SetDataTransferObjects(settings);
        }
    }
}
