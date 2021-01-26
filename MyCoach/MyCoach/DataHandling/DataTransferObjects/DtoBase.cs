using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.DataHandling.DataTransferObjects
{
    public class DtoBase : IDataTransferObject, ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
