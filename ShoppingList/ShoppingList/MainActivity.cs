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
            Intent newIntent = new Intent(this, typeof(Init));
            StartActivity(newIntent);
        }
    }
}

