using MyCoach.DataHandling;
using MyCoach.Model.DataTransferObjects;
using MyCoach.Model.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.DataBaseValidation
{
    public static class MonthsValidator
    {
        public static void Validate()
        {
            var months = DataInterface.GetInstance().GetData<Month>();

            if (months == null)
            {
                months = DefaultDtos.TrainingScores;
            }

            foreach (var monthNumber in Enum.GetValues(typeof(MonthNumber)).Cast<MonthNumber>())
            {
                if (months.Any(m => m.Number == monthNumber) == false)
                {
                    months.Add(new Month { Number = monthNumber });
                }

                var dublicates = months.Where(m => m.Number == monthNumber).Skip(1);

                foreach (var dublicate in dublicates)
                {
                    months.Remove(dublicate);
                }
            }

            DataInterface.GetInstance().SaveData<Month>();
        }
    }
}
