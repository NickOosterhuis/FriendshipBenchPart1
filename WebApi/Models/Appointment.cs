using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public bool Accepted { get; set; }

        public Bench Bench { get; set; }

        public int ClientID { get; set; }

        public string HealthworkerName { get; set; }
    }
}
