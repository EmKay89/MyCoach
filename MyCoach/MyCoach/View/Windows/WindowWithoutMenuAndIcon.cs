using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace MyCoach.View.Windows
{
    /// <summary>
    ///     Base class for windows that are not supposed to show icons or menu buttons.
    ///     Imlements <see cref="WindowWithoutMenuAndIcon.Window_MouseLeftButtonDown(object, MouseButtonEventArgs)"/>
    ///     to move the window via drag and drop.
    ///     Implements <see cref="WindowWithoutMenuAndIcon.CloseButton_Click(object, RoutedEventArgs)"/> to close the
    ///     window by an extra button that can be added to the dericed window.
    /// </summary>
    public class WindowWithoutMenuAndIcon : Window
    {
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public WindowWithoutMenuAndIcon()
        {
            this.Loaded += this.OnWindowLoaded;
        }

        public void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Hide menu bar and icon
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }
    }
}
