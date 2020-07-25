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
using System.Windows.Shapes;
using VideoCollection.Model;

namespace VideoCollection.View
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public VideoDataTemplete VideoDataTempleteFinal { get; set; }
        public EditWindow(VideoDataTemplete videoDataTemplete)
        {
            InitializeComponent();
            VideoDataTempleteFinal = videoDataTemplete;
            NameTextBox.Text = videoDataTemplete.VideoName;
            SizeTextBox.Text = videoDataTemplete.Size;
            DateTimeTextBox.Text = videoDataTemplete.CreationTime.ToString();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //VideoDataTempleteFinal.VideoName = NameTextBox.Text;

        }
    }
}
