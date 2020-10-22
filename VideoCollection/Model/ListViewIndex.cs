using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace VideoCollection.Model
{
    class ListViewIndex : IListViewIndex
    {
        public void FillingListView(ListView listView, List<VideoDataTemplete> videoDataTempletesList)
        {
            listView.ItemsSource = videoDataTempletesList;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listView.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("PressingFrequency", ListSortDirection.Descending));
            //CollectionViewSource.GetDefaultView(listView.ItemsSource).Refresh();
        }

        public int GetHoverIndex(ListView listView)
        {
            var item = VisualTreeHelper.HitTest(listView, Mouse.GetPosition(listView)).VisualHit;
            int index = 0;
            while (item != null && !(item is ListViewItem))
            {
                item = VisualTreeHelper.GetParent(item);
            }
            if (item != null)
            {
                index = listView.Items.IndexOf(((ListBoxItem)item).DataContext);
            }
            return index;
        }
    }
}
