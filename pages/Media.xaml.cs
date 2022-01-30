using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp.pages
{
    /// <summary>
    /// Логика взаимодействия для Media.xaml
    /// </summary>
    public partial class Media : Page
    {
        TimeLine timeLine = new TimeLine();
        public static double currentMain { get; set; } = 0;
        public Media()
        {
            InitializeComponent();
            DataContext = timeLine;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Tag)
            {
                case "Play":
                    VideoPlayer.Play();
                    timeLine.IsPlay = true;

                    break;
                case "Pause":
                    VideoPlayer.Pause();
                    timeLine.IsPlay = false;
                    break;
                case "Stop":
                    VideoPlayer.Stop();
                    timeLine.current = 0;
                    timeLine.IsPlay = false;
                    break;
            }

        }

        private void VideoPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            slPosition.Minimum = 0;
            slPosition.Maximum = VideoPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            currentMain = VideoPlayer.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void slPosition_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            VideoPlayer.Position = TimeSpan.FromSeconds(slPosition.Value);
            timeLine.IsPlay = true;
        }
    }
}
