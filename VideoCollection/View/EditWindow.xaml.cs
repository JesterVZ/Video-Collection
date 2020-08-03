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
using VideoLibrary;

namespace VideoCollection.View
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public string NameVideo, SizeVideo, DateTimeVideo, CommentVideo;
        public int Index;
        public List<TagTemplate> TagList = new List<TagTemplate>();
        private readonly ColorGenerator ColorGenerator = new ColorGenerator();
        private IListViewIndex listViewIndex = new ListViewIndex();
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddTag(TagTextBox.Text, ColorGenerator.GenerateColor());
        }

        private void TagTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                AddTag(TagTextBox.Text, ColorGenerator.GenerateColor());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TagList.RemoveAt(listViewIndex.GetHoverIndex(TagsListView));
            CollectionViewSource.GetDefaultView(TagsListView.ItemsSource).Refresh();
        }

        private void AddTag(string value, string color)
        {
            TagList.Add(new TagTemplate()
            {
                TagValue = value,
                Color = color
            });
            TagsListView.ItemsSource = TagList;
            TagTextBox.Text = "";
            CollectionViewSource.GetDefaultView(TagsListView.ItemsSource).Refresh();
        }

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
                CommentTextBox.Text = "";
            }
            if(videoDataTemplete.Tags != null)
            {
                for(int i = 0; i < videoDataTemplete.Tags.Count; i++)
                {
                    if (videoDataTemplete.Tags[i].Color != null)
                    {
                        AddTag(videoDataTemplete.Tags[i].TagValue, videoDataTemplete.Tags[i].Color);
                    } else
                    {
                        AddTag(videoDataTemplete.Tags[i].TagValue, ColorGenerator.GenerateColor());
                    }
                }
            }

            Index = hoverIndex;
            TagsListView.ItemsSource = TagList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NameVideo = NameTextBox.Text;
            SizeVideo = SizeTextBox.Text;
            DateTimeVideo = DateTimeTextBox.Text;
            CommentVideo = CommentTextBox.Text;
            this.Close();
        }
    }
}
