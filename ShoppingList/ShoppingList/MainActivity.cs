using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ShoppingList.Controller;

namespace ShoppingList
{
    [Activity(Label = "ShoppingList", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.NewList);

 
            button.Click += delegate 
            {
                Intent newIntent = new Intent(this, typeof(NewList));
                StartActivity(newIntent);
            };
        }
    }
}

