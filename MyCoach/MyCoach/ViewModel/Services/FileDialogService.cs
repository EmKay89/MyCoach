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
        public string OpenFile(string initialDirectory, string filter, int filterIndex, out bool okSelected)
        {
            string path = null;
            var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = initialDirectory,
                Filter = filter,
                FilterIndex = filterIndex
            };

            okSelected = openFileDialog.ShowDialog() == true;

            if (okSelected)
            {
                path = openFileDialog.FileName;
            }

            return path;
        }

        public string SaveFile(string initialDirectory, string filter, int filterIndex, out bool okSelected)
        {
            string result = null;
            var saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = initialDirectory,
                Filter = filter,
                FilterIndex = filterIndex
            };

            okSelected = saveFileDialog.ShowDialog() == true;

            if (okSelected)
            {
                result = saveFileDialog.FileName;
            }

            return result;
        }
    }
}
