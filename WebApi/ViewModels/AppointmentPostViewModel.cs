namespace WebApi.ViewModels
{
    public class AppointmentPostViewModel
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public bool Accepted { get; set; }

        public int BenchId { get; set; }

        public int ClientID { get; set; }

        public string HealthworkerName { get; set; }
    }
}