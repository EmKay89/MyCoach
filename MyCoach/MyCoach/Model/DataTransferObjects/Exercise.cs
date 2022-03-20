using MyCoach.Model.Defines;

namespace MyCoach.Model.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert eine Übung.
    /// </summary>
    public class Exercise : DtoBase, IDataTransferObject
    {
        private uint iD;
        private ExerciseCategory category;
        private ushort count;
        private string name;
        private string unit;
        private ushort scores;
        private string info;
        private bool active;

        /// <summary>
        ///     Eindeutiger Identifizierer der Übung.
        /// </summary>
        public uint ID
        {
            get => iD;
            set
            {
                if (value == iD)
                {
                    return;
                }

                iD = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die Übungskategorie auf oder legt sie fest.
        /// </summary>
        public ExerciseCategory Category
        {
            get => category;
            set
            {
                if (value == category)
                {
                    return;
                }

                category = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die standardmäßige Anzahl der Wiederholungen einer Übung in einem Training auf oder legt sie fest.
        /// </summary>
        public ushort Count
        {
            get => count;

            set
            {
                if (value == count)
                {
                    return;
                }

                count = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft den Namen der Übung auf, oder legt ihn fest.
        /// </summary>
        public string Name
        {
            get => name;

            set
            {
                if (value == name)
                {
                    return;
                }

                name = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die Einheit der Übung auf, oder legt sie fest.
        /// </summary>
        public string Unit
        {
            get => unit;

            set
            {
                if (value == unit)
                {
                    return;
                }

                unit = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die standardmäßige Anzahl an Punkten auf, die für das Absolvieren einer Übung vergeben wird, oder legt sie fest.
        /// </summary>
        public ushort Scores
        {
            get => scores;

            set
            {
                if (value == scores)
                {
                    return;
                }

                scores = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft eine Beschreibung der Übung auf, oder legt sie fest.
        /// </summary>
        public string Info
        {
            get => info;

            set
            {
                if (value == info)
                {
                    return;
                }

                info = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Gibt an, ob eine Übung aktiv ist. Nur aktive Übungen werden in ein Training eingeplant.
        /// </summary>
        public bool Active
        {
            get => active;

            set
            {
                if (value == active)
                {
                    return;
                }

                active = value;
                InvokePropertyChanged();
            }
        }

        /// <inheritdoc/>
        public override void CopyValuesTo(DtoBase target)
        {
            if (target is Exercise targetExercise)
            {
                targetExercise.Category = Category;
                targetExercise.Count = Count;
                targetExercise.Name = Name;
                targetExercise.Unit = Unit;
                targetExercise.Scores = Scores;
                targetExercise.Info = Info;
                targetExercise.Active = Active;
            }
        }

        /// <summary>
        ///     <see cref="ToString"/> Methode ist überschrieben und gibt den Namen der Kategorie zurück.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
    }
}
