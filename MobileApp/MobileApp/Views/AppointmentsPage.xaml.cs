using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentsPage : ContentPage
    {
        ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();

        public AppointmentsPage()
        {
            FillAppointments();
            InitializeComponent();
            AppointmentsList.ItemsSource = appointments;
        }

        private void FillAppointments()
        {
            appointments.Add(new Appointment { Date = "02-11-2017", Time = "14:00", Accepted = false, BenchID = 1, ClientID = 1, HealthworkerName = "Dr. Blaauw" });
            appointments.Add(new Appointment { Date = "06-12-2017", Time = "15:45", Accepted = false, BenchID = 1, ClientID = 1, HealthworkerName = "Dr. Boonstra" });
            appointments.Add(new Appointment { Date = "12-10-2018", Time = "10:00", Accepted = false, BenchID = 1, ClientID = 1, HealthworkerName = "Dr. Boonstra" });
            appointments.Add(new Appointment { Date = "25-02-2018", Time = "11:30", Accepted = false, BenchID = 1, ClientID = 1, HealthworkerName = "Dr. Blaauw" });
        }
    }
}