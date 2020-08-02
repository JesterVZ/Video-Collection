using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace VideoCollection.Model
{
    interface IListViewIndex
    {
        int GetHoverIndex(ListView listView);
        void FillingListView(ListView listView, List<VideoDataTemplete> videoDataTempletesList);
    }
}
