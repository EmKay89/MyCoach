using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Übungssatz, fasst alle Übungen und Übungskategorien zusammen.
    /// </summary>
    public class ExerciseSet
    {
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Exercise> Exercises { get; set; }
    }
}
