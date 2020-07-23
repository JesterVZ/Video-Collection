using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using VideoCollection.Model;

namespace VideoCollection.View
{
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
        public async void OpenFileSystemAsync()
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
            FillingListView();
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
                AddVideoInMediaElement(new Uri(videoDataTempleteList[this.DataListView.SelectedIndex].Directory));
                TitleTextBox.DataContext = new VideoDataTemplete()
                {
                    VideoName = videoDataTempleteList[this.DataListView.SelectedIndex].VideoName
                };
                SizeTextBox.DataContext = new VideoDataTemplete()
                {
                    Size = videoDataTempleteList[this.DataListView.SelectedIndex].Size
                };
                DateTextBox.DataContext = new VideoDataTemplete()
                {
                    CreationTime = videoDataTempleteList[this.DataListView.SelectedIndex].CreationTime
                };
                VideoOutput.Volume = (double)VolumeSlider.Value;
                VideoOutput.Stop();
                IsPaused = true;
                StateIcon.Kind = PackIconKind.Play;
            }
            catch
            {

            }

        }
        private void AddVideoInMediaElement(Uri value)
        {
            VideoOutput.DataContext = new VideoDataTemplete()
            {
                SourcePath = value
            };
        }
        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VideoOutput.Volume = (double)(VolumeSlider.Value / 10);
            if (VideoOutput.Volume >= 0.9)
            {
                VolumeIcon.Kind = PackIconKind.VolumeHigh;
                return;
            }
            if (VideoOutput.Volume >= 0.7 && VideoOutput.Volume <= 0.9)
            {
                VolumeIcon.Kind = PackIconKind.VolumeMedium;
                return;
            }
            if (VideoOutput.Volume >= 0.5 && VideoOutput.Volume <= 0.7)
            {
                VolumeIcon.Kind = PackIconKind.VolumeLow;
                return;
            }
            if (VideoOutput.Volume == 0)
            {
                VolumeIcon.Kind = PackIconKind.VolumeMute;
                return;
            }
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
                StateIcon.Kind = PackIconKind.Pause;

            }
            else
            {
                IsPaused = true;
                VideoOutput.Pause();
                StateIcon.Kind = PackIconKind.Play;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileSystemAsync();
        }

        private void VideoOutput_MediaEnded(object sender, RoutedEventArgs e)
        {
            if (DataListView.Items.Count > DataListView.SelectedIndex+1)
            {
                DataListView.SelectedIndex = DataListView.SelectedIndex + 1;
                AddVideoInMediaElement(new Uri(videoDataTempleteList[this.DataListView.SelectedIndex].Directory));
                VideoOutput.Play();
            } else
            {
                VideoOutput.Stop();
            }
        }
        void Window_Closing(object sender, global::System.ComponentModel.CancelEventArgs e)
        {
            string jsonData = JsonConvert.SerializeObject(videoDataTempleteList[1]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileSystemAsync();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //write in json
            string jsonDataString = "[";
            for (int i = 0; i < videoDataTempleteList.Count; i++)
            {
                if(i < videoDataTempleteList.Count - 1)
                {
                    jsonDataString += (JsonConvert.SerializeObject(videoDataTempleteList[i]).ToString() + ",");
                } else
                {
                    jsonDataString += JsonConvert.SerializeObject(videoDataTempleteList[i]).ToString();
                }
            }
            jsonDataString += "]";
            File.WriteAllText("videos.json", jsonDataString);

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //read json
            var videosList = File.Exists("videos.json");
            if (videosList)
            {
                var jsonData = JsonConvert.DeserializeObject<List<VideoDataTemplete>>(File.ReadAllText("videos.json"));
                if(jsonData != null)
                {
                    for(int i = 0; i < jsonData.Count; i++)
                    {
                        FillingVideoDataTempleteListFromJson(jsonData[i].Directory, jsonData[i].VideoName, jsonData[i].Size, jsonData[i].CreationTime);
                    }
                    FillingListView();
                }
            } else
            {
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, "videos.json");
                File.Create(path);
            }

        }
        private void FillingVideoDataTempleteListFromJson(string directory, string videoName, string size, DateTime creationTime)
        {
            videoDataTempleteList.Add(new VideoDataTemplete()
            {
                Directory = directory,
                VideoName = videoName,
                Size = size,
                CreationTime = creationTime
            });

        }
        private void FillingListView()
        {
            for (int i = 0; i < videoDataTempleteList.Count; i++)
            {
                DataListView.Items.Add(videoDataTempleteList[i]);
            }
        }
    }
}
