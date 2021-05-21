using MyCoach.Defines;

namespace MyCoach.DataHandling.DataTransferObjects
{
    /// <summary>
    ///     Repräsentiert eine Übungskategorie. Übungen können in Typen unterteilt werden, die Typen wiederum in Kategorien. Die Typen
    ///     WarmUp und CoolDown haben jeweils nur eine gleichnamige Kategorie.
    /// </summary>
    public class Category : DtoBase, IDataTransferObject
    {
        private ExerciseCategory iD;
        private string name;
        private ExerciseType type;
        private ushort count;
        private bool active;

        /// <summary>
        ///     Ruft den eindeutigen Identifizierer der Übungskategorie auf oder legt ihn fest.
        /// </summary>
        public ExerciseCategory ID
        {
            get => this.iD;
            set
            {
                if (value == this.iD)
                {
                    return;
                }

                this.iD = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft den Namen der Übungskategorie auf oder legt ihn fest.
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
        ///     Ruft den Übungstyp auf oder legt ihn fest.
        /// </summary>
        public ExerciseType Type
        {
            get => this.type;
            set
            {
                if (value == this.type)
                {
                    return;
                }

                this.type = value;
                this.InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die Anzahl der Übungen der Übungskategorie in einem Training auf oder legt sie fest. Dieser Wert ist nur
        ///     relevant für die Übungstypen WarmUp und CoolDown. Für den Typ Training wird der Wert in der GUI beim Start des
        ///     Trainings festgelegt.
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
        ///     Gibt an, ob eine Übungskategorie aktiv ist. Nur Übungen einer aktiven Kategorie werden in ein Training eingeplant.
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
            if (target is Category targetCategory)
            {
                targetCategory.ID = this.ID;
                targetCategory.Name = this.Name;
                targetCategory.Type = this.Type;
                targetCategory.Count = this.Count;
                targetCategory.Active = this.Active;
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
