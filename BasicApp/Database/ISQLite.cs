using System;
using SQLite;

namespace BasicApp.Database
{
    public interface ISQLite
    {
        SQLiteAsyncConnection GetAsyncConnection();

        SQLiteConnection GetConnection();
    }
}
