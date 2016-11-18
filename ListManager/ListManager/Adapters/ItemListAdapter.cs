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
using ListManager.Controller;
using Java.Util;

namespace ListManager.Adapters
{
    class ButtonAdapter : BaseAdapter<Item>
    {
        private readonly Activity _context;
        private readonly List<Item> _items;
        private readonly String _newKey;

        
        public ButtonAdapter(Activity context, List<Item> items, String newKey) : base()
        {
            _context = context;
            _items = items;
            _newKey = newKey;
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
                if (item.NameItem != "")
                {
                    convertView = _context.LayoutInflater.Inflate(Resource.Layout.View_row, null);
                    convertView.FindViewById<TextView>(Resource.Id.tvViewRow).Text = item.NameItem;
                    Item item2 = this._items[position];
                    String name = item2.NameItem;

                    convertView.FindViewById<TextView>(Resource.Id.tvViewRow).Text = name;

                    Button btnDelete = convertView.FindViewById<Button>(Resource.Id.btnDelete);
                    btnDelete.Tag = name;
                    btnDelete.SetOnClickListener(new ButtonClickListener(this._context, this._newKey));

                }
                else
                {
                    convertView = _context.LayoutInflater.Inflate(Resource.Layout.Simple_row, null);
                    convertView.FindViewById<TextView>(Resource.Id.NameSimpleRow).Text = item.NameItem;
                }
            }

            


            return convertView;
        }

        private class ButtonClickListener : Java.Lang.Object, View.IOnClickListener
        {
            private Activity activity;
            private String newKey;

            public ButtonClickListener(Activity activity, String newKey)
            {
                this.activity = activity;
                this.newKey = newKey;
            }

            public void OnClick(View v)
            {

                String rowDeleteName = (String)v.Tag;
                String itemsList = Preferences.getString(this.activity, this.newKey);
                var bundle = new Bundle();

                //Remplace the name for "".
                itemsList = itemsList.Replace(rowDeleteName, "");
                // Structure of the string --> name|name|name|name|name ...
                itemsList = itemsList.Replace("||", "|");
                itemsList = itemsList.TrimEnd('|');   //Remove | to end String 
                itemsList = itemsList.TrimStart('|'); // Remove | to start string
                Preferences.setString(this.activity, this.newKey, itemsList);
                Intent modify = new Intent(this.activity, typeof(ModifyList));
                bundle.PutString("key", this.newKey);
                modify.PutExtras(bundle);
                this.activity.StartActivity(modify);


            }
        }

        public override int Count => _items.Count;

        public override Item this[int position]
            => _items[position];
    }
}

