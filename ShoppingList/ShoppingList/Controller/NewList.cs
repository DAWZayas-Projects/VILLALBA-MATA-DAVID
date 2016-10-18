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
using Java.Util;
using System.Collections;

namespace ShoppingList.Controller
{
    [Activity(Label = "NewList")]
    public class NewList : Activity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewList);

            EditText textToAdd = FindViewById<EditText>(Resource.Id.editText);
            Button addToListButton = FindViewById<Button>(Resource.Id.add);
            ListView mListView = FindViewById<ListView>(Resource.Id.listView1);

            List<string> mItems = new List<string>();

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);

            mListView.Adapter = adapter;

            addToListButton.Click += (sender, e) => {
                if (textToAdd.Text.Length > 0)
                {
                    adapter.Add(textToAdd.Text); //tendre aki que añadir la nueva vista.
                }
 

            };
        }
    }
}
