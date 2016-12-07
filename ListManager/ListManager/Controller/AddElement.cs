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
using System.Globalization;

namespace ListManager.Controller
{
    [Activity(Label = "List Manager")]
    public class AddElement : ListActivity
    {
        //vars
        Button btnSave;
        Button btnBack;
        Button btnEmptyList;
        EditText addElement;
        LinearLayout action;
       

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddElement);

            btnSave = FindViewById<Button>(Resource.Id.save);
            addElement = FindViewById<EditText>(Resource.Id.addElement);
            btnBack = FindViewById<Button>(Resource.Id.back);
            btnEmptyList = FindViewById<Button>(Resource.Id.btnEmptyList);
            action = FindViewById<LinearLayout>(Resource.Id.linearLayoutAction);


            btnSave.Click += BtnSave_Click;
            btnBack.Click += BtnBack_Click;

            btnEmptyList.Visibility = ViewStates.Invisible;
            action.Visibility = ViewStates.Gone;

        }


        protected override void OnResume()
        {
            base.OnResume();                     
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();        
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //vars               
            String conversion;
            String element = addElement.Text;
            ArrayList lists = new ArrayList();

            //Accedo a las listas
            String listNames = Preferences.getString(this, Preferences.getLists());
           
            if (listNames != "")
            {
                lists.AddRange(listNames.Split('|'));
            }

            //Compruebo si existe el nombre. Si no existe el nombre los guardamos.
                     

            if (!listNames.Contains(element))
            {             
                lists.Add(element);
                Toast.MakeText(this, "Save list " + element, ToastLength.Long).Show();
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
    }
}