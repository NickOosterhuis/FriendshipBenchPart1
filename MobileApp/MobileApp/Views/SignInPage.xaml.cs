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
                await Login(new ClientUser { Email = Email.Text, Password = Password.Text });
            };

        }

       public async Task Login(ClientUser user)
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
                Debug.WriteLine(readableContent);
                await Navigation.PushAsync(new LandingPage());
            }
            else
            {
                await DisplayAlert("Invalid login", "The username or password is incorrect.", "Cancel");
                Debug.WriteLine("Er is iets fout gegaan :(");
                Debug.WriteLine(response.Headers);
            }  
        }
    }
}