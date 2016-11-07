using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ListManager.Controller;

namespace ListManager
{
    [Activity(Label = "ListManager", MainLauncher = true, Icon = "@drawable/icon")]
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