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
using BasicApp.Database;
using SQLite;
using System.IO;

namespace BasicApp.Droid
{
    public class SQLite_Android : ISQLite
    {
        const string DATABASE_NAME = "vs.db3";

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, DATABASE_NAME);
            // Create the connection
            var conn = new SQLite.SQLiteAsyncConnection(path);
            // Return the database connection
            return conn;
        }

        public SQLiteConnection GetConnection()
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, DATABASE_NAME);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}