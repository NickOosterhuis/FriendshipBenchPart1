namespace WebApi.ViewModels
{
    public class AppointmentPostViewModel
    {
        public string Date { get; set; }

        public string Time { get; set; }

        public int BenchId { get; set; }

        public string ClientId { get; set; }

        public string HealthworkerId { get; set; }
    }
}