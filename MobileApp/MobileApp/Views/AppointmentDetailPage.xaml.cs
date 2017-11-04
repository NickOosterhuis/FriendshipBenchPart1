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
        // Initialize the page.
        public AppointmentDetailPage(Appointment appointment)
        {
            BindingContext = appointment;
            InitializeComponent();

            // Create a accept and a cancel button.
            Button acceptButton = new Button { Text = "Accept the appointment" };
            Button cancelButton = new Button { Text = "Cancel the appointment" };

            // Add listeners to these buttons.
            acceptButton.Clicked += (object sender, EventArgs e) => {
                UpdateAppointmentStatusAsync(appointment, true);
            };
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
            } 
            else
            {
                // Display an error.
                DisplayAlert("Error", "Sorry, something went wrong. Please try again later.", "Okay");
            }

        }
    }
}