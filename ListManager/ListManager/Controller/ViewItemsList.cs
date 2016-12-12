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
    public class ViewItemsList : ListActivity
    {

        int allElements;
        int count = -1;
        ArrayList positions;
        String key = "";
        TextView nameList;
        TextView progress;
        Button btnModifyList;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ViewList);

            nameList = FindViewById<TextView>(Resource.Id.view_list);
            progress = FindViewById<TextView>(Resource.Id.progress);
            btnModifyList = FindViewById<Button>(Resource.Id.btnModify);
            btnModifyList.Click += BtnModifyList_Click;

            positions = new ArrayList();
            

        }

        

        protected override void OnResume()
        {
            base.OnResume();

            if (this.Intent.Extras != null)
            {
                key = this.Intent.Extras.GetString("key");
                nameList.Text = key;
            }

            ViewList();
        }

        public void ViewList()
        {
            String itemsList = Preferences.getString(this, key);

            if (itemsList != "")
            {
                ArrayList lists = new ArrayList();

                lists.AddRange(itemsList.Split('|'));
                List<Item> listItems = new List<Item>();

                foreach (String str in lists)
                {
                    listItems.Add(new Item { NameItem = str });
                }

                allElements = listItems.Count;
                string[] arrayString = listItems.Select(x => x.NameItem).ToArray();
                ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItemChecked, arrayString);

                
            }
            
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            l.ChoiceMode = Android.Widget.ChoiceMode.Multiple;
     
            /******** Count check ticked **********/
            bool flag = positions.Contains(position.ToString());

            if (!flag){
               if(count != -1)
                {
                    positions.Add(position.ToString());
                } 
               
                count++;
            }else
            {
                positions.Remove(position.ToString());
                count--;
            }
            /******** Fin count check ticked **********/
            progress.Text = count + "/" + allElements;

        } 

        private void BtnModifyList_Click(object sender, EventArgs e)
        {
            var bundle = new Bundle();
            Intent ModifyList = new Intent(this, typeof(ModifyList));
            //Send the name of the list that will be the key to visualize.
            bundle.PutString("key", key);
            ModifyList.PutExtras(bundle);
            StartActivity(ModifyList);

        }

    }
}