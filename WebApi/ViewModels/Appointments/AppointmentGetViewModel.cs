using System;
using WebApi.Models;
using WebApi.ViewModels.HealthWorkers;

namespace WebApi.ViewModels.Appointments
{
    public class AppointmentGetViewModel
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        public AppointmentStatus Status { get; set; }

        public Bench Bench { get; set; }

        public string ClientId { get; set; }

        public HealthWorkerViewModel Healthworker { get; set; }
    }
}