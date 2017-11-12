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
using MobileApp.Helpers;
using MobileApp.ViewModels;

namespace MobileApp.Views
{    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public Picker MenuItem;
        APIRequestHelper requestHelper;
        Client client; 
        

        public SignInPage()
        {
            InitializeComponent();
            requestHelper = new APIRequestHelper(); 

            RegisterButton.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new RegisterPage());
            };

            SignInButton.Clicked += async (object sender, EventArgs e) =>
            {
                client = new Client { Email = Email.Text, Password = Password.Text };
                await Login(client);
            };
        }

        protected override void OnDisappearing()
        {
            LoginViewModel vm = new LoginViewModel
            {
                Email = Email.Text,
                Password = Password.Text,
            };

            var content = JsonConvert.SerializeObject(vm);
            var token = requestHelper.GetAccessToken(content);

            App.Current.Properties["id"] = client.Id;
            App.Current.Properties["email"] = client.Email;
            App.Current.Properties["token"] = token;
            App.Current.Properties["password"] = client.Password;

            App.Current.SavePropertiesAsync();
        }

        public async Task Login(Client user)
       {
            LoginViewModel vm = new LoginViewModel
            {
                Email = user.Email,
                Password = user.Password
            };

            var content = JsonConvert.SerializeObject(vm); 
            
            var apiResponse = await requestHelper.PostRequest(Constants.loginUrl, content);

            if (apiResponse != null)
            {
                //get token and bind to httpheader
                requestHelper.SetTokenHeader(content);
                Debug.WriteLine(@" User Successfully logged in");
                await Navigation.PushAsync(new LandingPage());
            }
            else
            {
                await DisplayAlert("Invalid login", "The username or password is incorrect.", "Cancel");
                Debug.WriteLine("Er is iets fout gegaan :("); 
            }  
        }
    }
}