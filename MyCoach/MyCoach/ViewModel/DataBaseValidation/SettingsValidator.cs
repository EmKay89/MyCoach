using MyCoach.DataHandling;
using MyCoach.Defines;
using MyCoach.Model.DataTransferObjects;
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
            var settings = DataInterface.GetInstance().GetData<Settings>();

            if (settings == null)
            {
                settings = new ObservableCollection<Settings>();
            }

            if (settings.Any() == false)
            {
                settings.Add(DefaultDtos.Settings.First());
            }

            var dublicates = settings.Skip(1);

            foreach (var setting in dublicates)
            {
                settings.Remove(setting);
            }
            
            DataInterface.GetInstance().SaveData<Settings>();
        }
    }
}
