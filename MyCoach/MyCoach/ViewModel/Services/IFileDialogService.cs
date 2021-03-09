using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.Services
{
    public interface IFileDialogService
    {
        string OpenFile(string initialDirectory, string filter, int filterIndex);
        string SaveFile(string initialDirectory, string filter, int filterIndex);
    }
}
