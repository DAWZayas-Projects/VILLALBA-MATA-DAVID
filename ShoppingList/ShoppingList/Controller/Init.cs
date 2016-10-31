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
using ShoppingList.Adapters;


namespace ShoppingList.Controller
{
    [Activity(Label = "Init")]
    public class Init : Activity
    {

        Button btnNewList;
        Button btnDeleteAllElelements;
        ListView myListView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Init);

            btnNewList = FindViewById<Button>(Resource.Id.NewList);
            btnDeleteAllElelements = FindViewById<Button>(Resource.Id.deleteAllElements);

            btnNewList.Click += btnNewList_Click;
            btnDeleteAllElelements.Click += btnDeleteAllElelements_Click;

            viewList();
        }
  

        protected override void OnResume()
        {
            base.OnResume();
            viewList();
        }

        public void viewList()
        {
            String listNames = Preferences.getString(this, Preferences.getLists());
            ArrayList lists = new ArrayList();
           
            lists.AddRange(listNames.Split('|'));
            
            List<Item> listItems = new List<Item>();

            foreach (String str in lists)
            {
                listItems.Add(new Item { NameItem = str});

            }

            if (listItems[0].NameItem != "")
            {
                myListView = FindViewById<ListView>(Resource.Id.idViewList);
                myListView.Adapter = new ItemListAdapter(this, listItems);
            }
        }

        private void btnNewList_Click(object sender, EventArgs e)
        {
            Intent newIntent = new Intent(this, typeof(NewList));
            StartActivity(newIntent);
        }

        private void btnDeleteAllElelements_Click(object sender, EventArgs e)
        {
            Preferences.setString(this, Preferences.getLists(), "");
        }
    }
}