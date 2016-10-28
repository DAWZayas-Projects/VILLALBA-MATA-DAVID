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
using System.Collections;
using ShoppingList.Models;

namespace ShoppingList.Controller
{
    [Activity(Label = "Init")]
    public class Init : Activity
    {

        Button btnNewList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Init);

            btnNewList = FindViewById<Button>(Resource.Id.NewList);

            btnNewList.Click += btnNewList_Click;

            viewList();


        }

        private void btnNewList_Click(object sender, EventArgs e)
        {
            Intent newIntent = new Intent(this, typeof(NewList));
            StartActivity(newIntent);
        }

        public void viewList()
        {
            String listNames = Preferences.getString(this, Preferences.getLists());
            ArrayList lists = new ArrayList();

            if (listNames == "")
            {
                lists[0] = "No lists";
            } else {
                lists.AddRange(listNames.Split('|'));
            }
            List<Item> listItems = new List<Item>();

            foreach (String str in lists)
            {
                listItems.Add(new Item{ NameItem = str});

            }
            // Capturar el ListView de axml por su ID
            // Primera tabla de sinceo
        }
    }
}