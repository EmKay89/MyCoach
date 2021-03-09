using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCoach.ViewModel.Services
{
    public class FileDialogService : IFileDialogService
    {
        public string OpenFile(string initialDirectory, string filter, int filterIndex)
        {
            string result = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = initialDirectory;
            openFileDialog.Filter = filter;
            openFileDialog.FilterIndex = filterIndex;
            if (openFileDialog.ShowDialog() == true)
            {
                result = openFileDialog.FileName;
            }

            return result;
        }

        public string SaveFile(string initialDirectory, string filter, int filterIndex)
        {
            string result = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = initialDirectory;
            saveFileDialog.Filter = filter;
            saveFileDialog.FilterIndex = filterIndex;
            if (saveFileDialog.ShowDialog() == true)
            {
                result = saveFileDialog.FileName;
            }

            return result;
        }
    }
}
