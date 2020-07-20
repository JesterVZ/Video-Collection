using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using VideoCollection.Model;

namespace VideoCollection.View
{
    /// <summary>
    /// Логика взаимодействия для ContentView.xaml
    /// </summary>

    public partial class ContentView : UserControl
    {
        private bool IsPaused = true;
        private List<VideoDataTemplete> videoDataTempleteList = new List<VideoDataTemplete>();
        bool? dialogOk;
        OpenFileDialog fileDialog;
        DispatcherTimer timer;

        public ContentView()
        {
            DataContext = this;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(Timer_tick);
            InitializeComponent();
        }

        void Timer_tick(object sender, EventArgs e)
        {
            TimeBarSlider.Value = VideoOutput.Position.TotalSeconds;
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
        private async void OpenFileSystemAsync()
        {
            await Task.Run(() => OpenFileSystem()); //вызов диалогового окна асинхронно
            if(dialogOk == true)
            {
                FillingList(); //заполнение листа объектов
            }
        }
        private void OpenFileSystem()
        {
            fileDialog = new OpenFileDialog
            {
                Multiselect = true
            };
            dialogOk = fileDialog.ShowDialog();
        }
        private void FillingList()
        {
            DataListView.Items.Clear();
            Model.SizeConverter sizeConverter = new Model.SizeConverter();
            foreach (string sFileName in fileDialog.FileNames)
            {
                videoDataTempleteList.Add(new VideoDataTemplete()
                {
                    Directory = sFileName,
                    VideoName = RemoveSlash(sFileName),
                    Size = sizeConverter.FileSizeConvert(new FileInfo(sFileName).Length),
                    CreationTime = new FileInfo(sFileName).CreationTime
                });
            }
            for (int i = 0; i < videoDataTempleteList.Count; i++)
            {
                DataListView.Items.Add(videoDataTempleteList[i]);
            }
        }
        private string RemoveSlash(string value)
        {
            char[] pathCharArray;
            string name = "";
            List<char> pathCharList = new List<char>();
            pathCharArray = value.ToCharArray();
            pathCharArray.Reverse();
            for (int i = value.Length - 1; i > 0; i--)
            {
                if(pathCharArray[i] != '\\')
                {
                    pathCharList.Add(pathCharArray[i]);
                } else
                {
                    pathCharList.Reverse();
                    for(int j = 0; j < pathCharList.Count; j++)
                    {
                        name += pathCharList[j];
                    }
                    break;
                }
            }
            return name;
        }

        private void DataListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                VideoOutput.DataContext = new VideoDataTemplete()
                {
                    SourcePath = new Uri(videoDataTempleteList[this.DataListView.SelectedIndex].Directory)
                };
                TitleTextBox.DataContext = new VideoDataTemplete()
                {
                    VideoName = videoDataTempleteList[this.DataListView.SelectedIndex].VideoName
                };
                SizeTextBox.DataContext = new VideoDataTemplete()
                {
                    Size = videoDataTempleteList[this.DataListView.SelectedIndex].Size
                };
                VideoOutput.Volume = (double)VolumeSlider.Value;
                VideoOutput.Stop();
                IsPaused = true;
                VideoControlButton.Content = "Play";
            }
            catch
            {

            }

        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VideoOutput.Volume = (double)VolumeSlider.Value;
        }

        private void TimeBarSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VideoOutput.Position = TimeSpan.FromSeconds(TimeBarSlider.Value);
        }

        private void VideoOutput_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan time = VideoOutput.NaturalDuration.TimeSpan;
            TimeBarSlider.Maximum = time.TotalSeconds;
            timer.Start();
        }

        private void VideoOutput_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsPaused)
            {
                IsPaused = false;
                VideoOutput.Play();
                VideoControlButton.Content = "Pause";

            }
            else
            {
                IsPaused = true;
                VideoOutput.Pause();
                VideoControlButton.Content = "Play";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileSystemAsync();
        }
    }
}
