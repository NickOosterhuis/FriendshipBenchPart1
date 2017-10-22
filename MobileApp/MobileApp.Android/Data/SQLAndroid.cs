using MobileApp.Data;
using MobileApp.Droid.Data;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLAndroid))]
namespace MobileApp.Droid.Data
{
    public class SQLAndroid : ISQLite
    {
        public SQLAndroid() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqlFileName = "TestDB.db3";
            string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            System.Console.WriteLine(documentPath);
            var path = Path.Combine(documentPath, sqlFileName);
            var conn = new SQLite.SQLiteConnection(path);

            return conn;
        }
    }
}