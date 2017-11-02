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
        ObservableCollection<Appointment> allAppointments;
        ObservableCollection<ObservableGroupCollection<string, Appointment>> groupedAppointments;

        public AppointmentsPage()
        {
            // Fill a list with all appointments and bind the list to the listview.
            FillAppointments();
            InitializeComponent();
            AppointmentsList.ItemsSource = groupedAppointments;
        }

        private void FillAppointments()
        {
            // Fill a list with all the appointments.
            allAppointments = new ObservableCollection<Appointment>();
            allAppointments.Add(new Appointment { Date = "02-11-2017", Time = "14:00", Accepted = true, BenchID = 1, ClientID = 1, HealthworkerName = "Dr. Blaauw" });
            allAppointments.Add(new Appointment { Date = "06-12-2017", Time = "15:45", Accepted = true, BenchID = 1, ClientID = 1, HealthworkerName = "Dr. Boonstra" });
            allAppointments.Add(new Appointment { Date = "12-10-2018", Time = "10:00", Accepted = true, BenchID = 1, ClientID = 1, HealthworkerName = "Dr. Boonstra" });
            allAppointments.Add(new Appointment { Date = "25-02-2018", Time = "11:30", Accepted = false, BenchID = 1, ClientID = 1, HealthworkerName = "Dr. Blaauw" });

            // Group the appointments by status.
            var orderedAppointments =
                allAppointments.OrderBy(a => a.Date)
                .GroupBy(a => a.AcceptStatus)
                .Select(a => new ObservableGroupCollection<string, Appointment>(a))
                .ToList();

            // Convert the grouped appointments to a ObservableGroupCollection
            groupedAppointments = new ObservableCollection<ObservableGroupCollection<string, Appointment>>(orderedAppointments);
        }
    }

    // The class for a ObserverableGroupCollection
    public class ObservableGroupCollection<S, T> : ObservableCollection<T>
    {
        private readonly S _key;

        public ObservableGroupCollection(IGrouping<S, T> group) : base(group)
        {
            _key = group.Key;
        }

        public S Key
        {
            get { return _key; }
        }
    }
}