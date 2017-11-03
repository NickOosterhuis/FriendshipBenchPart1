using MobileApp.Models;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentsPage : ContentPage
    {
        ObservableCollection<Appointment> allAppointments;
        ObservableCollection<ObservableGroupCollection<string, Appointment>> groupedAppointments;
        private bool isRefreshing = false;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
        }

        // Initialize the page.
        public AppointmentsPage()
        {
            BindingContext = this;
            FetchAppointments();
            InitializeComponent();
        }

        // Fetch all the appointments and make a list of it.
        private async Task FetchAppointments()
        {
            Bench bench1 = new Bench { Streetname = "Teststraat", Housenumber = "2", District = "Districttest", Province = "Testprovince" };

            // Send a GET request to the API.
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Constants.getAppointmentsUrl);

            // If the request was succesfull, add appointments to the list. If not, let the user know.
            if (response.IsSuccessStatusCode)
            {
                // Convert the API response into a JSON object.
                List<Appointment> appointmentModels = new List<Appointment>();
                String json = await response.Content.ReadAsStringAsync();
                dynamic convertedJson = JsonConvert.DeserializeObject(json);

                // Loop through all the appointments in the JSON object and create appointment objects.
                foreach (var appointment in convertedJson)
                {
                    appointmentModels.Add(new Appointment
                    {
                        ID = (int)appointment.id,
                        Date = (string)appointment.date,
                        Time = (string)appointment.time,
                        Accepted = (bool)appointment.accepted,
                        ClientID = (int)appointment.clientID,
                        HealthworkerName = (string)appointment.healthworkerName,
                        Bench = bench1
                    });  
                }
                allAppointments = new ObservableCollection<Appointment>(appointmentModels);
            }
            else
            {
                DisplayAlert("Error", "Sorry, something went wrong. Please try again later.", "Okay");
            }

            // Group and update the list.
            GroupAppointments();
            AppointmentsList.ItemsSource = groupedAppointments;
        }

        // Group appointments by their status.
        private void GroupAppointments()
        {
            // Group the appointments by their status.
            var orderedAppointments =
                allAppointments.OrderBy(a => a.Date)
                .ThenBy(a => a.Time)
                .GroupBy(a => a.AcceptStatus)
                .Select(a => new ObservableGroupCollection<string, Appointment>(a))
                .ToList();

            // Convert the grouped appointments to a ObservableGroupCollection.
            groupedAppointments = new ObservableCollection<ObservableGroupCollection<string, Appointment>>(orderedAppointments);
        }

        // Show the appointment details when an item has been selected.
        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new AppointmentDetailPage((Appointment) e.SelectedItem));           
        }

        // Refresh the list with appointments when a 'pull-to-refresh' has been performed.
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;
                    await FetchAppointments();
                    IsRefreshing = false;
                });
            }
        }
    }


    // The class for a ObserverableGroupCollection.
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