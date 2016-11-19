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
using ListManager.Models;
using ListManager.Adapters;

namespace ListManager.Controller
{
    [Activity(Label = "List Manager")]
    public class ViewList : Activity
    {

       
       // ListView myListView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewList);


        }

        protected override void OnResume()
        {
            base.OnResume();
      
            //viewList();
        }

       /* public void viewList()
        {
            String itemsList = Preferences.getString(this, list);
            ArrayList lists = new ArrayList();

            lists.AddRange(itemsList.Split('|'));
            List<Item> listItems = new List<Item>();

            foreach (String str in lists)
            {
                listItems.Add(new Item { NameItem = str });          
            }

           // myListView.Adapter = new ButtonAdapter(this, listItems);

        }*/
    }
}