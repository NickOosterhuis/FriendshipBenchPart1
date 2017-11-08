using MobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditUserPage : ContentPage
    {
        ClientUser ClientUser;
        public EditUserPage()
        {
            InitializeComponent();
            fetchUser();
        }

        private async Task fetchUser()
        {
            var baseAddress = new Uri(Constants.getUserUrl);
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var content = new FormUrlEncodedContent(new[]
                {
        new KeyValuePair<string, string>("foo", "bar"),
        new KeyValuePair<string, string>("baz", "bazinga"),
    });
                cookieContainer.Add(baseAddress, new Cookie("CookieName", "cookie_value"));
                var result = client.PostAsync("/test", content).Result;
                result.EnsureSuccessStatusCode();
            }




        }
    }
}