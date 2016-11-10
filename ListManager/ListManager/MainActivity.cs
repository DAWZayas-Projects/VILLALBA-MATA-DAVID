using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ListManager.Controller;

using System.Threading;


namespace ListManager
{

    [Activity(Label = "SplashScreen", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.SplashActivity")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Wait for 2 seconds
            Thread.Sleep(2000);

            //Moving to next activity
            StartActivity(typeof(MainActivity));
        }
    }


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