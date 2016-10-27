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
using Java.Util;
using System.Collections;
using ShoppingList.Controller;

namespace ShoppingList.Controller
{
    [Activity(Label = "NewList")]
    public class NewList : ListActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.NewList);
            Button btnAdd = FindViewById<Button>(Resource.Id.add);

            // List<Foods> foods = Foods.GetFoods();
            List<Foods> foods = new List<Foods>();

            foods.Add(new Foods { Name = "Patatas" });

       
            this.ListAdapter = new ButtonAdapter(this, foods);


            btnAdd.Click += delegate
            {
                EditText edit = FindViewById<EditText>(Resource.Id.editText);
                foods.Add(new Foods { Name = edit.Text });
            };
           
        }

      

     
          private class ButtonAdapter : BaseAdapter<Foods>
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
       }
    }
}