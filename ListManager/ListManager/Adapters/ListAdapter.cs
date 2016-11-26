using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ListManager.Models;

namespace ListManager.Adapters
{
    class ListAdapter : BaseAdapter<Item>
    {
        private readonly Activity _context;
        private readonly List<Item> _items;
  
        public ListAdapter(Activity context, List<Item> items) : base()
        {
            _context = context;
            _items = items;
      
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];

            convertView = _context.LayoutInflater.Inflate(Resource.Layout.Row_list_view, null);
            convertView.FindViewById<TextView>(Resource.Id.itemList).Text = item.NameItem;

            return convertView;
        }

        public override int Count => _items.Count;

        public override Item this[int position]
            => _items[position];
    }
}