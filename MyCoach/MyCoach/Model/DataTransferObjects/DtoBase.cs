using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyCoach.Model.DataTransferObjects
{
    public abstract class DtoBase : ICloneable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Erzeugt eine neue Instanz des Objekts mit kopierten Werten, jedoch ohne Abonnenten 
        ///     auf PropertyChangedEvents.
        /// </summary>
        public virtual object Clone()
        {
            var clone = MemberwiseClone() as DtoBase;
            clone.ResetSubscriptions();
            return clone;
        }

        /// <summary>
        ///     Kopiert die Werte des Objekts auf eine andere Objektinstanz desselben Typs.
        /// </summary>
        public abstract void CopyValuesTo(DtoBase target);

        /// <summary>
        ///     Entfernt alle Abonnenten aller PropertyChangedEvents.
        /// </summary>
        public void ResetSubscriptions() => PropertyChanged = null;

        /// <summary>
        ///     Vergleicht das DTO auf Gleichheit aller Werte mit einem anderen DTO.
        /// </summary>
        /// <param name="dto">Das andere DTO.</param>
        /// <returns>True, wenn alle Properties gleiche Werte haben, ansonsten false.</returns>
        public abstract bool ValuesAreEqual(DtoBase dto);

        protected void InvokePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
