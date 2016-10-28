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
using Android.Preferences;

using Java.IO;
using Android.Provider;
using System.IO;

namespace ShoppingList
{
    class Preferences
    {

        private static string id_list              = "ID_LIST";
        private static string first_time_execution = "FIRST_TIME_EXECUTION";
        private static string lists = "LISTS";

        public static string getIdList()
        {
            return id_list;
        }

        public static string getLists()
        {
            return lists;
        }

        public static string getFirsTimeExcution()
        {
            return first_time_execution;
        }

        public static string getString(Context context, string clave)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            var str = preferences.GetString(clave, "");
            return str;
        }

        public static int getInt(Context context, string clave)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            return preferences.GetInt(clave, 0);
        }

        public static void setString(Context context, string clave, string valor)
        {
            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editor = preferences.Edit();
            editor.PutString(clave, valor);
            editor.Commit();
        }

        public static void setInt(Context context, string clave, int valor)
        {

            ISharedPreferences preferences = PreferenceManager.GetDefaultSharedPreferences(context);
            ISharedPreferencesEditor editor = preferences.Edit();
            editor.PutInt(clave, valor);
            editor.Commit();
        }

        // Preferences.getInt(c, Preferences.getIdList()); --> LLamada
        
        public static void initPreferences(Context c)
        {
            var i = getInt(c, first_time_execution);
            if (i == 0)
            {
                string folder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures) + "/ShoppingList";
                if (Directory.Exists(folder))
                {
                    Directory.Delete(folder, true);
                }
                setInt(c, first_time_execution, 1);

            }
            setInt(c, id_list, -1);
        
        }
    }
}