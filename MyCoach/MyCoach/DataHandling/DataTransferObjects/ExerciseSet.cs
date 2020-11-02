using System;
using System.Collections.Generic;
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
        public List<Category> Categories { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
