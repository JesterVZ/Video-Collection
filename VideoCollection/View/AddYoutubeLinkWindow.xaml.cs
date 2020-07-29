using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VideoCollection.Model;
using VideoLibrary;

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
