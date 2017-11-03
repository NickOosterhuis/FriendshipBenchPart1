using WebApi.Models;

namespace WebApi.ViewModels
{
    public class AppointmentGetViewModel
    {
        public int ID { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public bool Accepted { get; set; }

        public Bench Bench { get; set; }

        public int ClientID { get; set; }

        public string HealthworkerName { get; set; }
    }
}