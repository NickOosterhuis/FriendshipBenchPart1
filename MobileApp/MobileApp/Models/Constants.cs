using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class Constants
    {

        public static bool isDev = true;

        public static string loginUrl = "http://10.0.2.2:54618/api/account/signin";
        public static string appointmentsUrl = "http://10.0.2.2:54618/api/appointments";
        public static string registerUrl = "http://10.0.2.2:54618/api/account/register/client";
    }
}
