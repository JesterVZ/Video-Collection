using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoCollection.View
{
    /// <summary>
    /// Логика взаимодействия для ContentView.xaml
    /// </summary>
    public struct VideoData
    {
        public Uri SourcePath { get; set; }
    }
    public partial class ContentView : UserControl
    {
        private bool IsPaused = true;
        public ContentView()
        {
            DataContext = this;
            InitializeComponent();
            VideoOutput.DataContext = new VideoData()
            {
                SourcePath = new Uri(@"C:\\Users\\vladi\\Videos\\music\\Different Heaven - Nekozilla [NCS Release].mp4")
            };
            VideoOutput.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsPaused)
            {
                IsPaused = false;
                VideoOutput.Play();
                VideoControlButton.Content = "Pause";

            } else
            {
                IsPaused = true;
                VideoOutput.Pause();
                VideoControlButton.Content = "Play";
            }
        }
    }
}
