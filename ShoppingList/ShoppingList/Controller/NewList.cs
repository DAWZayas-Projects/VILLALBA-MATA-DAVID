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
using ShoppingList.Controller;
using Android.Preferences;
using ShoppingList.Models;
using ShoppingList.Adapters;


namespace ShoppingList.Controller
{
    [Activity(Label = "NewList")]
    public class NewList : Activity
    {
        //vars
        Button btnSave;
        Button btnBack;
        EditText newList;
        ListView myListView;
        Boolean rep = false;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.NewList);

            btnSave = FindViewById<Button>  (Resource.Id.save);
            newList = FindViewById<EditText>(Resource.Id.newList);
            btnBack = FindViewById<Button>  (Resource.Id.back);
            myListView = FindViewById <ListView>(Resource.Id.listView);

            btnSave.Click += btnSave_Click;
            btnBack.Click += btnBack_Click;
            

        }


        protected override void OnResume()
        {
            base.OnResume();
            if (rep == true)
            {
                viewList();
            }
        }
         public void viewList()
        {
           
            List<Item> listItems = new List<Item>();                          
            listItems.Add(new Item { NameItem = newList.Text });
            myListView.Adapter = new ButtonAdapter(this, listItems);
            newList.Text = "";
            newList.RequestFocus();

        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            Finish();
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //vars 
            Boolean bl;          
            String conversion;        
  
            //Accedo a las listas
            String listNames = Preferences.getString(this, Preferences.getLists());
            ArrayList lists = new ArrayList();

            if (listNames != "")
            {
                lists.AddRange(listNames.Split('|'));
            }
            
            //Compruebo si existe el nombre
            bl = ExistList(lists, newList.Text);

            //Si no existe el nombre (False) los guardamos.
            if(bl == false)
            {                
                lists.Add(newList.Text);
            } else {
                Toast.MakeText(this, "already exist name "+newList.Text, ToastLength.Long).Show();               
                rep = true;
                OnResume();

            }

            conversion = ConversionToString(lists);

            Preferences.setString(this, Preferences.getLists(), conversion);

            newList.Text = "";
        }

        public String ConversionToString(ArrayList arrayList)
        {
            String listNames="";
            foreach (var item in arrayList)
            {
                listNames += (item) + "|";

            }

            listNames = listNames.TrimEnd('|');

            return listNames;
        }

        public Boolean ExistList(ArrayList array, String name)
        {
            int exist;

            exist = array.IndexOf(name);

            if (exist >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }     
    }
}