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
using ListManager.Controller;
using Android.Preferences;
using ListManager.Models;
using ListManager.Adapters;


namespace ListManager.Controller
{
    [Activity(Label = "NewList")]
    public class AddElement : ListActivity
    {
        //vars
        Button btnSave;
        Button btnBack;
        Button btnEmptyList;
        EditText addElement;
        
        Boolean rep = false;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddElement);

            btnSave = FindViewById<Button>(Resource.Id.save);
            addElement = FindViewById<EditText>(Resource.Id.addElement);
            btnBack = FindViewById<Button>(Resource.Id.back);
            btnEmptyList = FindViewById<Button>(Resource.Id.btnEmptyList);     


            btnSave.Click += btnSave_Click;
            btnBack.Click += btnBack_Click;

            btnEmptyList.Visibility = ViewStates.Invisible;

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
            listItems.Add(new Item { NameItem = addElement.Text });
            string[] arrayString = listItems.Select(x => x.NameItem).ToArray();
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arrayString);

        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            Finish();
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //vars               
            String conversion;        
  
            //Accedo a las listas
            String listNames = Preferences.getString(this, Preferences.getLists());
            ArrayList lists = new ArrayList();

            if (listNames != "")
            {
                lists.AddRange(listNames.Split('|'));
            }

            //Compruebo si existe el nombre. Si no existe el nombre los guardamos.
            String element = addElement.Text.ToLower();            

            if (!listNames.Contains(element))
            {             
                lists.Add(element);
            }
            else
            {
                Toast.MakeText(this, "already exist name " + element, ToastLength.Long).Show();
                OnResume();

            }

            conversion = ConversionToString(lists);

            Preferences.setString(this, Preferences.getLists(), conversion);

            addElement.Text = "";
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

       /* public Boolean ExistList(ArrayList array, String name)
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
        }*/
     

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            Intent viewList = new Intent(this, typeof(ViewList));
            StartActivity(viewList);
        }

        private void ListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

           

        }
    }
}