using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace VideoCollection.Model
{
    interface IListViewIndex
    {
        public int GetHoverIndex(ListView listView);
    }
}
