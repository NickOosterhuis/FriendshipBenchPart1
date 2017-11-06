using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppointmentDetailPage : ContentPage
    {
        Appointment appointment;
        int appointmentId;

        // Initialize the page.
        public AppointmentDetailPage(int appointmentId)
        {
            this.appointmentId = appointmentId;
            FetchAppointment();
            InitializeComponent();
        }

        // Fetch the appointment.
        private async Task FetchAppointment()
        {
            // Send a GET request to the API.
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(Constants.appointmentsUrl + "/" + appointmentId);

            // If the request was succesfull, create a new appointment object
            if (response.IsSuccessStatusCode)
            {
                // Convert the API response into a JSON object.
                String json = await response.Content.ReadAsStringAsync();
                dynamic convertedJson = JsonConvert.DeserializeObject(json);

                // Create a new object from the appointments.
                appointment = new Appointment
                {
                    Id = (int)convertedJson.id,
                    Date = (string)convertedJson.date,
                    Time = (string)convertedJson.time,
                    Status = new AppointmentStatus { Id = (int)convertedJson.status.id, Name = (string)convertedJson.status.name },
                    Bench = new Bench { Id = (int)convertedJson.bench.id, Streetname = (string)convertedJson.bench.streetname, Housenumber = (string)convertedJson.bench.housenumber, Province = (string)convertedJson.bench.province, District = (string)convertedJson.bench.district },
                    ClientId = (int)convertedJson.clientId,
                    HealthworkerName = (string)convertedJson.healthworkerName,
                };
            }
            else
            {
                DisplayAlert("Error", "Sorry, something went wrong. Please try again later.", "Okay");
            }

            BindingContext = appointment;
            UpdateButtons();
        }

        // Show the right buttons for this appointment.
        private void UpdateButtons()
        {
            // Remove existing buttons.
            ButtonSpace.Children.Clear();

            // Create a accept and a cancel button.
            Button acceptButton = new Button { Text = "Accept the appointment" };
            Button cancelButton = new Button { Text = "Cancel the appointment" };

            // Add listeners to these buttons.
            acceptButton.Clicked += (object sender, EventArgs e) => {
                UpdateAppointmentStatusAsync(appointment, true);
            };
            acceptButton.BackgroundColor = (Color)Application.Current.Resources["Primary"];
            acceptButton.TextColor = Color.White;
            //acceptButton.Image = ImageSource.FromFile("check.png");
            cancelButton.Clicked += (object sender, EventArgs e) => {
                UpdateAppointmentStatusAsync(appointment, false);
            };

            // If the appointment is waiting for response, show an accept and a cancel button.
            if (appointment.Status.Id == 1)
            {
                ButtonSpace.Children.Add(acceptButton);
                ButtonSpace.Children.Add(cancelButton);
            }
            // If the appointment has been accepted, show a cancel button.
            else if (appointment.Status.Id == 2)
            {
                ButtonSpace.Children.Add(cancelButton);
            }
        }

        // Update the status of an appointment.
        private async Task UpdateAppointmentStatusAsync(Appointment appointment, bool accepted)
        {
            // Create a model that will be sent to update through the API
            int statusId = accepted ? 2 : 3;
            AppointmentViewModel viewModel = new AppointmentViewModel
            {
                Id = appointment.Id,
                Date = appointment.Date,
                Time = appointment.Time,
                StatusId = statusId,
                BenchId = appointment.Bench.Id,
                HealthworkerName = appointment.HealthworkerName,
            };

            // Do a PUT request.
            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(viewModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage();
            response = await client.PutAsync(Constants.appointmentsUrl + "/" + appointment.Id, content);

            if (response.IsSuccessStatusCode)
            {
                // Display a succesfull alert.
                String alertTitle = accepted ? "Appointment accepted" : "Appointment canceled";
                String alertMessage = accepted ? "The appointment has been accepted." : "The appointment has been canceled.";
                DisplayAlert(alertTitle, alertMessage, "Okay");
                FetchAppointment();
            } 
            else
            {
                // Display an error.
                DisplayAlert("Error", "Sorry, something went wrong. Please try again later.", "Okay");
            }

        }
    }
}