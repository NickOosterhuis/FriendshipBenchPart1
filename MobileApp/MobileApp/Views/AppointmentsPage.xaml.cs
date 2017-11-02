﻿using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
            AppointmentsList.ItemsSource = groupedAppointments;
        }

        // Fetch all the appointments and make a list of it.
        private async Task FetchAppointments()
        {
            // TODO: Send an API GET request.

            // Fill a list with all the appointments.
            allAppointments = new ObservableCollection<Appointment>();
            allAppointments.Add(new Appointment { Date = "02-11-2017", Time = "14:00", Accepted = true, BenchLocation = "Zimbabwe", ClientID = 1, HealthworkerName = "Dr. Blaauw" });
            allAppointments.Add(new Appointment { Date = "06-12-2017", Time = "15:45", Accepted = true, BenchLocation = "Zimbabwe", ClientID = 1, HealthworkerName = "Dr. Boonstra" });
            allAppointments.Add(new Appointment { Date = "12-10-2018", Time = "10:00", Accepted = true, BenchLocation = "Groningen, Netherlands", ClientID = 1, HealthworkerName = "Dr. Boonstra" });
            allAppointments.Add(new Appointment { Date = "25-02-2018", Time = "11:30", Accepted = false, BenchLocation = "Zimbabwe", ClientID = 1, HealthworkerName = "Dr. Blaauw" });

            // Group the appointments by status.
            var orderedAppointments =
                allAppointments.OrderBy(a => a.Date)
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