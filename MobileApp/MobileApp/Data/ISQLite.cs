using SQLite;

namespace MobileApp.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
