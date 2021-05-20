using MyCoach.Defines;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert eine Übung.
    /// </summary>
    public class Exercise : DtoBase, IDataTransferObject
    {
        private ExerciseCategory category;
        private ushort count;
        private string name;
        private ExerciseCategory relatedCategory;
        private ushort scores;
        private string info;
        private bool active;

        /// <summary>
        ///     Ruft die Übungskategorie auf oder legt sie fest.
        /// </summary>
        public ExerciseCategory Category
        {
            get => this.category;
            set
            {
                if (value == this.category)
                {
                    return;
                }

                this.category = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die standardmäßige Anzahl der Wiederholungen einer Übung in einem Training auf oder legt sie fest.
        /// </summary>
        public ushort Count
        {
            get => this.count;

            set
            {
                if (value == this.count)
                {
                    return;
                }

                this.count = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft den Namen der Übung auf, oder legt ihn fest.
        /// </summary>
        public string Name
        {
            get => this.name;

            set
            {
                if (value == this.name)
                {
                    return;
                }

                this.name = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die Übungskategorie, auf die sich die Übung bezieht, auf, oder legt sie fest. Übungen der Kategorien 
        ///     WarmUp oder CoolDown können sich auf eine Kategorie des Typs Training beziehen. Übungen des Typs Training
        ///     können sich nur auf ihre eigene Kategorie beziehen.
        /// </summary>
        public ExerciseCategory RelatedCategory
        {
            get => this.relatedCategory;
            set
            {
                if (value == this.relatedCategory)
                {
                    return;
                }

                this.relatedCategory = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die standardmäßige Anzahl an Punkten auf, die für das Absolvieren einer Übung vergeben wird, oder legt sie fest.
        /// </summary>
        public ushort Scores
        {
            get => this.scores;

            set
            {
                if (value == this.scores)
                {
                    return;
                }

                this.scores = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft eine Beschreibung der Übung auf, oder legt sie fest.
        /// </summary>
        public string Info
        {
            get => this.info;

            set
            {
                if (value == this.info)
                {
                    return;
                }

                this.info = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Gibt an, ob eine Übung aktiv ist. Nur aktive Übungen werden in ein Training eingeplant.
        /// </summary>
        public bool Active
        {
            get => this.active; 
            
            set
            {
                if (value == this.active)
                {
                    return;
                }

                this.active = value;
                this.InvokePropertyChanged();
            }
        }

        /// <inheritdoc/>
        public override void CopyValuesTo(DtoBase target)
        {
            if (target is Exercise targetExercise)
            {
                targetExercise.Category = this.Category;
                targetExercise.Count = this.Count;
                targetExercise.Name = this.Name;
                targetExercise.RelatedCategory = this.RelatedCategory;
                targetExercise.Scores = this.Scores;
                targetExercise.Info = this.Info;
                targetExercise.Active = this.Active;
            }
        }

        /// <summary>
        ///     <see cref="ToString"/> Methode ist überschrieben und gibt den Namen der Kategorie zurück.
        /// </summary>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
