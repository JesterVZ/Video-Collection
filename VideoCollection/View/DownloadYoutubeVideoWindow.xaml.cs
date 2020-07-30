using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using YoutubeExtractor;

namespace VideoCollection.View
{
    /// <summary>
    /// Логика взаимодействия для DownloadYoutubeVideoWindow.xaml
    /// </summary>
    public partial class DownloadYoutubeVideoWindow : Window
    {
        public DownloadYoutubeVideoWindow()
        {
            InitializeComponent();
            ResolutionCombobox.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProgressBarLoad.Minimum = 0;
            ProgressBarLoad.Maximum = 100;

            IEnumerable<VideoInfo> videos = DownloadUrlResolver.GetDownloadUrls(UriTextBox.Text);

            VideoInfo video = videos.First(p => p.VideoType == VideoType.Mp4 && p.Resolution == Convert.ToInt32(ResolutionCombobox.Text));
            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }
            VideoDownloader downloader = new VideoDownloader(video, System.IO.Path.Combine(Environment.CurrentDirectory));
            downloader.DownloadProgressChanged += DownloadProgressChanged;
            Thread thread = new Thread(() =>
            {
                downloader.Execute();
            })
            {
                IsBackground = true
            };
            thread.Start();
        }

        private void DownloadProgressChanged(object sender, ProgressEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(delegate
            {
                ProgressBarLoad.Value = (int)e.ProgressPercentage;
                ProgressBarText.Content = $"{string.Format("{0:0.##}", e.ProgressPercentage)}%";
                ProgressBarLoad.UpdateLayout();
            }));

        }
    }
}
