using System;
using System.Windows;
using VideoCollection.Model;

namespace VideoCollection.View
{
    /// <summary>
    /// Логика взаимодействия для AddYoutubeLinkWindow.xaml
    /// </summary>
    public partial class AddYoutubeLinkWindow : Window
    {
        private string name;
        private Uri VideoUri;
        public AddYoutubeLinkWindow()
        {
            InitializeComponent();
        }
        public Uri GetVideoUri()
        {
            return VideoUri;
        }
        public string GetName()
        {
            return name;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            YouTubeFinder youTubeFinder = new YouTubeFinder();
            youTubeFinder.GetSource(LinkTextBox.Text);
            VideoUri = youTubeFinder.GetUri();
            name = youTubeFinder.GetName();
            this.Close();
        }
    }
}
