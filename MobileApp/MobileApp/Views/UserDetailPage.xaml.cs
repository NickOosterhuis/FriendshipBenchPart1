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
    public partial class UserDetailPage : ContentPage
    {
        public UserDetailPage()
        {
            InitializeComponent();
            GetCurrentUser();

        }

        public async Task GetCurrentUser()
        {
            var httpClient = new HttpClient();
            var url = "http://10.0.2.2:54618/api/account/user";

            HttpResponseMessage response = await httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode)
            {
                String json = await response.Content.ReadAsStringAsync();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var convertedJson = JsonConvert.DeserializeObject(json);

                Debug.WriteLine(json);
                DisplayAlert("SUCCESS", response.RequestMessage.Content.ToString(), "cancel");
            }
            else
            {
                DisplayAlert("HTTP ERROR", response.Content.ReadAsStringAsync().ToString(), "Cancel");
                //Debug.WriteLine("HTTP ERROR: " + response.Headers.);
            }
        }
    }
}