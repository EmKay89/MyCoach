﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.DataHandling.DataTransferObjects
{
    public abstract class DtoBase : ICloneable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Erzeugt eine neue Instanz des Objekts mit kopierten Werten.
        /// </summary>
        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        ///     Kopiert die Werte des Objekts auf eine andere Objektinstanz desselben Typs.
        /// </summary>
        public abstract void CopyValuesTo(DtoBase target);

        protected void InvokePropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
