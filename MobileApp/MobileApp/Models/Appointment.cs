using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public AppointmentStatus Status { get; set; }

        public Bench Bench { get; set; }

        public int ClientId { get; set; }

        public string HealthworkerName { get; set; }

        public string DateTime
        {
            get { return string.Format("{0}, {1}", Date, Time); }
        }
    }
}
