using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class User
    {
        public User() { }
        public User(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;
        }

        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        public bool CheckInformation()
        {
            if (this.UserName != null && this.Password != null)
                return true;
            else
                return false;
        }
    }
}
