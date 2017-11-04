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

        public int StatusId { get; set; }

        public int BenchId { get; set; }

        public int ClientId { get; set; }

        public string HealthworkerName { get; set; }
    }
}
