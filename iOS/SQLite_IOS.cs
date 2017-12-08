using System;
using System.IO;
using BasicApp.Database;
using SQLite;

namespace BasicApp.iOS
{
    public class SQLite_iOS : ISQLite
    {
        const string DATABASE_NAME = "vs.db3";

        public SQLiteAsyncConnection GetAsyncConnection()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, DATABASE_NAME);
            // Create the connection
            var conn = new SQLite.SQLiteAsyncConnection(path);
            // Return the database connection
            return conn;
        }

        public SQLiteConnection GetConnection()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, DATABASE_NAME);
            // Create the connection
            var conn = new SQLite.SQLiteConnection(path);
            // Return the database connection
            return conn;
        }
    }
}
