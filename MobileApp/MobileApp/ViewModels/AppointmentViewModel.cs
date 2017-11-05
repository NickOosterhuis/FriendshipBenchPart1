using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Models
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public int StatusId { get; set; }

        public int BenchId { get; set; }

        public int ClientId { get; set; }

        public string HealthworkerName { get; set; }
    }
}
