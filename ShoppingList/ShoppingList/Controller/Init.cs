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
    public class Init : ListActivity
    {

        Button btnNewList;
        Button btnDeleteAllElelements;
       // ListView myListView;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Init);

            btnNewList = FindViewById<Button>(Resource.Id.newList);
            btnDeleteAllElelements = FindViewById<Button>(Resource.Id.deleteAllElements);
           

            btnNewList.Click += btnNewList_Click;
            btnDeleteAllElelements.Click += btnDeleteAllElelements_Click;

            ListView.ItemLongClick += ListView_ItemLongClick;
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
                listItems.Add(new Item { NameItem = str });

            }

            string[] arrayString = listItems.Select(x => x.NameItem).ToArray();
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arrayString);
            
            // Sacar lista con boton 
            /*String listNames = Preferences.getString(this, Preferences.getLists());
            ArrayList lists = new ArrayList();
           
            lists.AddRange(listNames.Split('|'));
            
            List<Item> listItems = new List<Item>();

            foreach (String str in lists)
            {
                listItems.Add(new Item { NameItem = str});

            }

            myListView.Adapter = new ButtonAdapter(this, listItems);
            */
        }
       
        private void btnNewList_Click(object sender, EventArgs e)
        {
            Intent newIntent = new Intent(this, typeof(NewList));
            StartActivity(newIntent);
        }

        private void btnDeleteAllElelements_Click(object sender, EventArgs e)
        {
            Preferences.setString(this, Preferences.getLists(), "");
            OnResume();
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            Intent viewList = new Intent(this, typeof(ViewList));
            StartActivity(viewList);
        }

        private void ListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            string rowDeleteName = (string)this.ListAdapter.GetItem(e.Position);
                    
            String listNames = Preferences.getString(this, Preferences.getLists());
            //Remplace the name for "".
            listNames = listNames.Replace(rowDeleteName, "");
            // Structure of the string --> name|name|name|name|name ...
            listNames = listNames.Replace("||", "|");
            listNames = listNames.TrimEnd('|');   //Remove | to end String 
            listNames = listNames.TrimStart('|'); // Remove | to start string
            Preferences.setString(this, Preferences.getLists(), listNames);
            OnResume();
        }
    }
}