using MyCoach.Model.Defines;

namespace MyCoach.Model.DataTransferObjects
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
        ///     Ruft den Namen der Übungskategorie auf oder legt ihn fest.
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
        ///     Ruft den Übungstyp auf oder legt ihn fest.
        /// </summary>
        public ExerciseType Type
        {
            get => type;
            set
            {
                if (value == type)
                {
                    return;
                }

                type = value;
                InvokePropertyChanged();
            }
        }

        /// <summary>
        ///     Ruft die Anzahl der Übungen der Übungskategorie in einem Training auf oder legt sie fest. Dieser Wert ist nur
        ///     relevant für die Übungstypen WarmUp und CoolDown. Für den Typ Training wird der Wert in der GUI beim Start des
        ///     Trainings festgelegt.
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
        ///     Gibt an, ob eine Übungskategorie aktiv ist. Nur Übungen einer aktiven Kategorie werden in ein Training eingeplant.
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
            if (target is Category targetCategory)
            {
                targetCategory.ID = ID;
                targetCategory.Name = Name;
                targetCategory.Type = Type;
                targetCategory.Count = Count;
                targetCategory.Active = Active;
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
