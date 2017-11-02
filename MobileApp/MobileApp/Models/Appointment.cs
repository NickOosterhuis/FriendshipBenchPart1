using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class Appointment
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public bool Accepted { get; set; }

        public int BenchID { get; set; }

        public int ClientID { get; set; }

        public string HealthworkerName { get; set; }

        public string DateTime
        {
            get { return string.Format("{0}, {1}", Date, Time); }
        }

        public string AcceptStatus
        {
            get { return Accepted ? "Accepted" : "Awaiting response"; }
        }
    }
}
