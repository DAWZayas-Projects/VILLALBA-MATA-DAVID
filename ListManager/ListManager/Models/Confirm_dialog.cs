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
using ListManager;



namespace ListManager.Model
{
    class CustomDialog
    {
        public static string title = "Confirmación de acción";
        public static Resource.Layout view;
        public static bool cancellable = true;
        public bool result { get; set; }
        public Button yesBtn;
        public Button noBtn;

        public CustomDialog(Context context)
        {
            var builder = new AlertDialog.Builder(context);
            builder.SetTitle(title);

            //Seteo los botones
            builder.SetPositiveButton("Si", (EventHandler<DialogClickEventArgs>)null);
            builder.SetNegativeButton("No", (EventHandler<DialogClickEventArgs>)null);
            var dialog = builder.Create();

            // Muestro el modal box
            dialog.Show();

            // Cojo los botones
            yesBtn = dialog.GetButton((int)DialogButtonType.Positive);
            noBtn = dialog.GetButton((int)DialogButtonType.Negative);

            // Asigno los listeners
            noBtn.Click += (sender, args) =>
            {
                result = false;
                dialog.Dismiss();
            };
            yesBtn.Click += (sender, args) =>
            {
                result = true;
                dialog.Dismiss();
            };
        }

    }
}