using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ContentView()
        {
            DataContext = this;
            InitializeComponent();
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            bool? dialogOk = fileDialog.ShowDialog();
            if (dialogOk == true)
            {
                foreach (string sFileName in fileDialog.FileNames)
                {
                    videoDataTempleteList.Add(new VideoDataTemplete()
                    {
                        Directory = sFileName,
                        VideoName = RemoveSlash(sFileName)
                    });
                }
                for(int i = 0; i < videoDataTempleteList.Count; i++)
                {
                    DataListView.Items.Add(videoDataTempleteList[i]);
                }
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
            VideoOutput.DataContext = new VideoDataTemplete()
            {
                SourcePath = new Uri(videoDataTempleteList[this.DataListView.SelectedIndex].Directory)
            };
            VideoOutput.Stop();
            IsPaused = true;
            VideoControlButton.Content = "Play";
        }
    }
}
