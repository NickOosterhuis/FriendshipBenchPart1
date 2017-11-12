using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class LoginToken
    {
        public string id_token { get; set; }
        public string access_token { get; set; }
        public ClientUser user { get; set; }
    }
}
