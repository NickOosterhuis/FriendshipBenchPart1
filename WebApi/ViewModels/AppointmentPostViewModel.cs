namespace WebApi.ViewModels
{
    public class AppointmentPostViewModel
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public int BenchId { get; set; }

        public int ClientId { get; set; }

        public string HealthworkerName { get; set; }
    }
}