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
        public string NameVideo, SizeVideo, DateTimeVideo, CommentVideo;
        public int Index;
        public EditWindow(VideoDataTemplete videoDataTemplete, int hoverIndex)
        {
            InitializeComponent();
            NameTextBox.Text = videoDataTemplete.VideoName;
            SizeTextBox.Text = videoDataTemplete.Size;
            DateTimeTextBox.Text = videoDataTemplete.CreationTime.ToString();
            if(videoDataTemplete.Comment != null)
            {
                CommentTextBox.Text = videoDataTemplete.Comment;
            } else
            {
                CommentTextBox.Text = "Комментарий пустой.";
            }
            Index = hoverIndex;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //VideoDataTempleteFinal.VideoName = NameTextBox.Text;
            NameVideo = NameTextBox.Text;
            SizeVideo = SizeTextBox.Text;
            DateTimeVideo = DateTimeTextBox.Text;
            CommentVideo = CommentTextBox.Text;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
