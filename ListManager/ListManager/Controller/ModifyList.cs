using System;
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
using ListManager.Controller;
using ListManager.Model;
using System.Globalization;
using System.Collections.Generic;

namespace ListManager.Controller
{
    [Activity(Label = "List Manager")]
    public class ModifyList : Activity
    {


        ListView myListView;
        Button btnSave;
        Button btnEmptyList;
        Button btnBack;
        String newKey = "";
        EditText addElement;
        CustomDialog customDialog;
        Boolean bl;
        TextView action;
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddElement);

            addElement = FindViewById<EditText>(Resource.Id.addElement);
            btnSave = FindViewById<Button>(Resource.Id.save);
            myListView = FindViewById<ListView>(Resource.Id.listViewButton);
            btnEmptyList = FindViewById<Button>(Resource.Id.btnEmptyList);
            btnBack = FindViewById<Button>(Resource.Id.back);
            action = FindViewById<TextView>(Resource.Id.actionManager);

            btnSave.Click += BtnSave_Click;
            btnEmptyList.Click += BtnEmptyList_Click;
            btnBack.Click += BtnBack_Click;
        
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (this.Intent.Extras != null)
            {
                newKey = this.Intent.Extras.GetString("key");             
            }

            if (newKey == "")
            {
                action.Text = " What list do you want to modify?";
                              
            } else {
                action.Text = " You are modify " + newKey;
                addElement.Hint = "Add element";
            }

            ViewList();       
        }

        public void ViewList()
        {
            String itemsList = Preferences.getString(this, newKey);         
            ArrayList lists = new ArrayList();
           
            lists.AddRange(itemsList.Split('|'));         
            List<Item> listItems = new List<Item>();

            foreach (String str in lists)
            {
                listItems.Add(new Item { NameItem = str});
                bl = true;
            }

            myListView.Adapter = new ButtonAdapter(this, listItems, newKey);
            
        }

           private void BtnBack_Click(object sender, EventArgs e)
        {
            Intent init = new Intent(this, typeof(Init));
            StartActivity(init);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //vars 
            
            String conversion;
            String itemsList = Preferences.getString(this, newKey);       
          
            String element = addElement.Text;
           

            //Buscando Key de la lista

            if (newKey == "")
            {
                newKey = element;
                bl = SearchList();
            }

            if (!bl)
            {
                Toast.MakeText(this, "This list  " + element + " does not exist", ToastLength.Long).Show();
                newKey = "";
            }     

            ArrayList lists = new ArrayList();

            if (itemsList != "")
            {
                lists.AddRange(itemsList.Split('|'));
            }

            //Compruebo si existe el nombre. Si no existe el nombre los guardamos.        

            if (!itemsList.Contains(element))
            {
                lists.Add(element);
            } else {
                Toast.MakeText(this, "already exist name " + element, ToastLength.Long).Show();
                OnResume();
            }

            conversion = ConversionToString(lists);
            if (newKey != element && bl == true)
            {
                Preferences.setString(this, newKey, conversion);              
            }

            OnResume();
            addElement.Text = "";
        }

        public Boolean SearchList()
        {
            Boolean bl;
            String listNames = Preferences.getString(this, Preferences.getLists());
            String element = addElement.Text;
            bl = listNames.Contains(element);
            return bl;

        }

        public String ConversionToString(ArrayList arrayList)
        {
            String listNames = "";
            foreach (var item in arrayList)
            {
                listNames += (item) + "|";

            }

            listNames = listNames.TrimEnd('|');

            return listNames;
        }
   
        private void BtnEmptyList_Click(object sender, EventArgs e)
        {      
            customDialog = new CustomDialog(this);
            customDialog.yesBtn.Click += delegate
            {
                Preferences.setString(this, newKey, "");
                OnResume();
            };         
                
        }
    }
}