using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

            // Set the values for the status button.
            ToggleStatusButton.Clicked += (object sender, EventArgs e) => {
                UpdateAppointmentStatus(appointment);
            };
            if(appointment.Status.Id == 1)
            {
                ToggleStatusButton.Text = "Accept appointment";
            }
            else if(appointment.Status.Id == 2)
            {
                ToggleStatusButton.Text = "Cancel appointment";
            }
            else if (appointment.Status.Id == 2)
            {
                ToggleStatusButton.Text = "Accept appointment";
            }
        }

        // Update the status of an appointment.
        private void UpdateAppointmentStatus(Appointment appointment)
        {
            // TODO: Send an API POST request and update the view.

            // Display an alert.
            //bool accepted = !appointment.Accepted;
            //String alertTitle = accepted ? "Appointment accepted" : "Appointment canceled";
            //String alertMessage = accepted ? "The appointment has been accepted." : "The appointment has been canceled.";

            //string jsonAppointment = JsonConvert.SerializeObject(appointment);

            //DisplayAlert(alertTitle, alertMessage, "Okay");
        }
    }
}