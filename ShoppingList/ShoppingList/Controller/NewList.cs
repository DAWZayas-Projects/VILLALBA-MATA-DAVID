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
namespace ShoppingList.Controller
{
    [Activity(Label = "NewList")]
    public class NewList : ListActivity
    {
        //vars
        String nameList;
        Button btnSave;

        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.NewList);

            FindViewById<Button>(Resource.Id.save);

          

            //List<Foods> foods = new List<Foods>();

            //ToDo ASÍ AÑADIRIAMOS TODOS LOS ELEMNTOS A LA LISTA
            //foods.Add(lists);


            //this.ListAdapter = new ButtonAdapter(this, foods);


            btnSave.Click += delegate
            {
                EditText edit = FindViewById<EditText>(Resource.Id.editText);


                String elements = Preferences.getString(this, Preferences.getLists());

                ArrayList elementsList = new ArrayList();

                elementsList.AddRange(elements.Split('|'));

                String newElement = edit.Text;

                elementsList.Add(newElement);
                //lists.Add(nameList);

                elements = string.Join("|", elementsList);

                Preferences.setString(this, Preferences.getLists(), elements);
            };
           
        }







        public void addNewList()
        {
            //Accedo a las listas
            String listNames = Preferences.getString(this, Preferences.getLists());
            ArrayList lists = new ArrayList();

            if (listNames != "")
            {
                lists.AddRange(listNames.Split('|'));
            }

            nameList = "list_" + (lists.Count + 1);

            Preferences.setString(this, nameList, "");

            lists.Add(nameList);

            //Concatenar Array en un String 
            //listNames = string.Join("|",lists);

            foreach (var item in lists)
            {
                listNames += (item) + "|";
            }
            listNames = listNames.TrimEnd('|');

            Preferences.setString(this, Preferences.getLists(), listNames);
        }








 /*        protected override void OnResume()
        {
            String elements =  Preferences.getString(this, nameList);

            ArrayList lists = new ArrayList();

            lists.AddRange(elements.Split('|'));

        }
*/

      /*  private class ButtonAdapter : BaseAdapter<Foods>
          {
              private Activity activity;
              private List<Foods> data;

              public ButtonAdapter(Activity activity, List<Foods> data)
              {
                  this.activity = activity;
                  this.data = data;
              }

              public override Foods this[int position]
              {
                  get { return this.data[position]; }
              }

              public override int Count
              {
                  get { return this.data.Count(); }
              }

              public override long GetItemId(int position)
              {
                  return 0;
              }
              
              public override View GetView(int position, View convertView, ViewGroup parent)
              {
                  View view = convertView;

                  if (view == null)
                  {
                      view = this.activity.LayoutInflater.Inflate(Resource.Layout.view_row, null);
                  }

                  Foods food = this.data[position] ;

                  string name = food.Name;
                
                    view.FindViewById<TextView>(Resource.Id.tvViewRow).Text = name;
                    Button btnDelete = view.FindViewById<Button>(Resource.Id.BtnDelete);
                    btnDelete.Tag = name;
                    btnDelete.SetOnClickListener(new ButtonClickListener(this.activity));
                

                  return view;
              }

              private class ButtonClickListener : Java.Lang.Object, View.IOnClickListener
              {
                  private Activity activity;

                  public ButtonClickListener(Activity activity)
                  {
                      this.activity = activity;
                  }

                  public void OnClick(View v)
                  {
                      string name = (string)v.Tag;
                      string text = string.Format("{0} Button Click.", name);
                      Toast.MakeText(this.activity, text, ToastLength.Short).Show();
                  }
              }
       }*/
    }
}