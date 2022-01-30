using System;
using System.ComponentModel;
using System.Windows.Threading;
using WpfApp.pages;

namespace WpfApp
{
    class TimeLine : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public double current { get; set; } = 0;
        public string currentS { get; set; } = "00:00:00";
        DispatcherTimer timer = new DispatcherTimer();
        public static TimeSpan time { get; set; } = TimeSpan.FromSeconds(1);
        public bool IsPlay
        {
            set
            {
                if (value == true) timer.Start(); else timer.Stop();
            }
        }

        public TimeLine()
        {
            timer.Interval = time;
            timer.Tick += Timer_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            current++;
            if (current >= Media.currentMain) timer.Stop();
            TimeSpan timeSpan = TimeSpan.FromSeconds((int)current);
            currentS = timeSpan.ToString("c");
            TimeSpan timeSpann = TimeSpan.FromSeconds(Media.currentMain);
            currentS += "/";
            currentS += timeSpann.ToString("c");

            PropertyChanged(this, new PropertyChangedEventArgs("current"));
            PropertyChanged(this, new PropertyChangedEventArgs("currentS"));

        }
    }
}