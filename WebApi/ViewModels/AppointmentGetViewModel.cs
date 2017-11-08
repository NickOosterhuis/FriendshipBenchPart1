using WebApi.Models;

namespace WebApi.ViewModels
{
    public class AppointmentGetViewModel
    {
        public int Id { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public AppointmentStatus Status { get; set; }

        public Bench Bench { get; set; }

        public string ClientId { get; set; }

        public HealthworkerViewModel Healthworker { get; set; }
    }
}