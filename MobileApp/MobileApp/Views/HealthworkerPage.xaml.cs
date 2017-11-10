using MobileApp.Helpers;
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
    public partial class HealthworkerPage : ContentPage
    {
        APIRequestHelper apiRequestHelper;
        Healthworker healthworker;
        string healthworkerId = "11f5f26c-137a-4242-8044-20d2e137d171";

        // Initialize the page.
        public HealthworkerPage()
        {
            apiRequestHelper = new APIRequestHelper();
            FetchHealthworker();
            InitializeComponent();
        }

        // Fetch the Healthworker.
        private async Task FetchHealthworker()
        {
            // Send a GET request to the API.
            string apiResponse = await apiRequestHelper.GetRequest(Constants.healthWorkerUrl + "/" + healthworkerId);
            if (apiResponse != null)
            {
                // Convert the API response into a JSON object.
                dynamic convertedJson = JsonConvert.DeserializeObject(apiResponse);

                // Create a new object from the appointments.
                healthworker = new Healthworker
                {
                    Id = healthworkerId,
                    Firstname = convertedJson.firstname,
                    Lastname = convertedJson.lastname,
                    Email = convertedJson.email,
                    Birthday = convertedJson.birthDay,
                    Gender = convertedJson.gender
                };
            }
            else
            {
                DisplayAlert("Error", "Sorry, something went wrong. Please try again later.", "Okay");
            }

            // Update the page items.
            BindingContext = healthworker;
        }
    }
}