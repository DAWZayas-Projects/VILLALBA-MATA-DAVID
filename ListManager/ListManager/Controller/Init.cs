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
using ListManager.Model;


namespace ListManager.Controller
{
    [Activity(Label = "List Manager")]
    public class Init : ListActivity
    {

        Button btnNewList;
        Button btnDeleteAllElelements;
        Button btnModifyList;
        TextView textInfoItemlist;
        CustomDialog customDialog;
    

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Load corresponding view
            SetContentView(Resource.Layout.Init);

            //Create a pointer with the ID to the corresponding element of the view
            btnNewList = FindViewById<Button>      (Resource.Id.newList);
            btnDeleteAllElelements = FindViewById<Button>      (Resource.Id.deleteAllElements);
            btnModifyList          = FindViewById<Button>      (Resource.Id.modifyList);
            textInfoItemlist       = FindViewById<TextView>    (Resource.Id.textInfoActions);

            btnNewList.Click             += btnNewList_Click;
            btnDeleteAllElelements.Click += btnDeleteAllElelements_Click;
            btnModifyList.Click          += btnModifyList_Click;
            ListView.ItemLongClick       += delete_Item_ItemLongClick;
        }
  

        protected override void OnResume()
        {
            base.OnResume();
            viewList();
            informationActionsItemList();
        }

        //Get the informative text if there is any element.
        public void informationActionsItemList()
        {
            String listNames = Preferences.getString(this, Preferences.getLists());
            if(listNames == "")
            {
                textInfoItemlist.Visibility = ViewStates.Invisible;
            } else {
                textInfoItemlist.Visibility = ViewStates.Visible;
            }
            
        }

        public void viewList()
        {
            // Structure of the string --> name|name|name|name|name ...
            String listNames = Preferences.getString(this, Preferences.getLists());
            //Create an arrayList and add the string separated by the |.
            ArrayList lists = new ArrayList();
            lists.AddRange(listNames.Split('|'));
            //create a list of items.
            List<Item> listItems = new List<Item>();

            foreach (String str in lists)
            {
                //Get the number of items from a list.
                String itemsList = Preferences.getString(this, str);
                ArrayList Quantity = new ArrayList();
                Quantity.AddRange(itemsList.Split('|'));

                //Add item to the list.
                listItems.Add(new Item { NameItem = str, Quantity= Quantity.Count.ToString() });

            }
            //Display the list with the adapter (SimpleListItem1)
            string[] arrayString = listItems.Select(x => x.NameItem+" "+"("+x.Quantity+")").ToArray();
            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arrayString);
            
          
        }
       
        private void btnNewList_Click(object sender, EventArgs e)
        {
            Intent newIntent = new Intent(this, typeof(AddElement));
            StartActivity(newIntent);
        }

        private void btnDeleteAllElelements_Click(object sender, EventArgs e)
        {
             String listNames = Preferences.getString(this, Preferences.getLists());
             if (listNames != "")
             {
                 customDialog = new CustomDialog(this);
                 customDialog.yesBtn.Click += delegate
                 {

                     ArrayList lists = new ArrayList();
                     lists.AddRange(listNames.Split('|'));          

                     foreach (String str in lists)
                     {
                         Preferences.setString(this, str, "");

                     }

                     Preferences.setString(this, Preferences.getLists(), "");
                     OnResume();
                 };
             }

             
        }

        private void btnModifyList_Click(object sender, EventArgs e)
        {
            Intent modifyList = new Intent(this, typeof(ModifyList));
            StartActivity(modifyList);

        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            String listNames = Preferences.getString(this, Preferences.getLists());
            ArrayList lists = new ArrayList();
            lists.AddRange(listNames.Split('|'));
            //Get the list according to the position of the clicked element.
            String keyList = lists[position].ToString();
            var bundle = new Bundle();
            Intent viewList = new Intent(this, typeof(ViewList));
            //Send the name of the list that will be the key to visualize.
            bundle.PutString("key", keyList);
            viewList.PutExtras(bundle);
            StartActivity(viewList);

        }

        private void delete_Item_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {

            customDialog = new CustomDialog(this);     
            customDialog.yesBtn.Click += delegate

            {
                string rowDeleteName = (string)this.ListAdapter.GetItem(e.Position);

                Preferences.setString(this, rowDeleteName, "");
                String listNames = Preferences.getString(this, Preferences.getLists());

                //Remplace the name for "".
                listNames = listNames.Replace(rowDeleteName, "");

                // Structure of the string --> name|name|name|name|name ...
                listNames = listNames.Replace("||", "|");
                listNames = listNames.TrimEnd('|');   //Remove | to end String 
                listNames = listNames.TrimStart('|'); // Remove | to start string
                Preferences.setString(this, Preferences.getLists(), listNames);
                OnResume();
            };
            
        }        
    }
}
