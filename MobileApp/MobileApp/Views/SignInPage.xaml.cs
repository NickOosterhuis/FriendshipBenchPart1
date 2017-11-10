using MobileApp.Models;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace MobileApp.Views
{    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public Picker MenuItem;
        
        public SignInPage()
        {
            InitializeComponent();
            RegisterButton.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new RegisterPage());
            };

            SignInButton.Clicked += async (object sender, EventArgs e) =>
            {
                await Login(new Client { Email = Email.Text, Password = Password.Text });
            };

        }

       public async Task Login(Client user)
       {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(user);
      
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string readableContent = await content.ReadAsStringAsync();

            var response = new HttpResponseMessage(); 
            
            try
            {
                response = await client.PostAsync(Constants.loginUrl, content);
            }
            catch(Exception e)
            {
                await DisplayAlert("ERROR", e.Message , "Cancel");
                Debug.WriteLine("HTTP ERROR: " + e.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@" User Successfully logged in");
                String responseJson = await response.Content.ReadAsStringAsync();
                LoginToken token = JsonConvert.DeserializeObject<LoginToken>(responseJson);
                //await LogoutTest(token);
                await Navigation.PushAsync(new LandingPage());
            }
            else
            {
                await DisplayAlert("Invalid login", "The username or password is incorrect.", "Cancel");
                Debug.WriteLine("Er is iets fout gegaan :(");
                Debug.WriteLine(response.Headers);
            }  
        }

        public async Task LogoutTest(LoginToken token)
        {
            string url = "http://10.0.2.2:54618/api/Account/user";
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.id_token);
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                String json = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(json);
            } else
            {
                Debug.WriteLine(response.StatusCode);
            }
        }
    }
}