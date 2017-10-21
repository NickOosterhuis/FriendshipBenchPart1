using System;
using System.IO;
using MobileApp.Data;
using Xamarin.Forms;
using MobileApp.iOS.Data;

[assembly: Dependency(typeof(SQL_IOS))]

namespace MobileApp.iOS.Data
{
    class SQL_IOS : ISQLite
    {
        public SQL_IOS() { }

        public SQLite.SQLiteConnection GetConnection()
        {
            var fileName = "TestDB.db3";
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }
    }
}