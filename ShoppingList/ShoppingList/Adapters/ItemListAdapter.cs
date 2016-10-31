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
using ShoppingList.Models;

namespace ShoppingList.Adapters
{
    class ItemListAdapter : BaseAdapter<Item>
    {
        private readonly Activity _context;
        private readonly List<Item> _items;

        public ItemListAdapter(Activity context, List<Item> items) : base()
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

            if (convertView == null)
            {
                convertView = _context.LayoutInflater.Inflate(Resource.Layout.view_row, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.tvViewRow).Text =  item.NameItem;

            return convertView;
        }

        public override int Count => _items.Count;

        public override Item this[int position]
            => _items[position];
    }
}