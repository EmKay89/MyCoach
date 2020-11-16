using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyCoach.Windows
{
    /// <summary>
    /// Interaktionslogik für Timer.xaml
    /// </summary>
    public partial class Timer : Window
    {
        private bool timerActive;
        private System.Windows.Forms.Timer timer;
        private TimeSpan time = TimeSpan.FromMinutes(1);
        private TimeSpan lastUsedTime;

        private bool TimerActive
        {
            get => this.timerActive;

            set
            {
                if (value == this.timerActive)
                {
                    return;
                }

                this.timerActive = value;
                this.OnTimerActiveChanged();
            }
        }

        public Timer()
        {
            InitializeComponent();
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
            this.timer.Tick += this.OnTimerElapsed;
            this.Closing += this.OnClosing;
            this.txtTime.Text = this.time.ToString();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.TimerActive = this.TimerActive ? false : true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void OnTimerActiveChanged()
        {
            if (this.TimerActive)
            {
                this.btnStart.Content = "Stop";
                this.txtTime.IsEnabled = false;
                this.timer.Start();
            }
            else
            {
                this.btnStart.Content = "Start";
                this.txtTime.IsEnabled = true;
                this.timer.Stop();
            }
        }

        private void txtTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.TimerActive)
            {
                return;
            }

            if (TimeSpan.TryParse(this.txtTime.Text, out TimeSpan newTime)
                && newTime.TotalMilliseconds > 0)
            {
                this.time = newTime;
                this.lastUsedTime = this.time;
            }
        }

        private void OnTimerElapsed(object sender, EventArgs e)
        {
            if (this.time >= TimeSpan.FromSeconds(1))
            {
                this.time -= TimeSpan.FromSeconds(1);
                this.timer.Interval = (int)TimeSpan.FromSeconds(1).TotalMilliseconds;
                this.timer.Start();
            }
            else
            {
                this.time = this.lastUsedTime;
                SystemSounds.Beep.Play();

                if ((bool)this.chkAutoRestart.IsChecked)
                {
                    this.timer.Start();
                }
                else
                {
                    this.TimerActive = false;
                }
            }

            this.txtTime.Text = this.time.ToString();
        }

        private void OnClosing(object sender, EventArgs e)
        {
            this.TimerActive = false;
        }
    }
}
