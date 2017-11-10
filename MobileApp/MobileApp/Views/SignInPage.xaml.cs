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

namespace MobileApp.Views
{    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public Picker MenuItem;
        APIRequestHelper requestHelper; 
        
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
                await Login(new Client { Email = Email.Text, Password = Password.Text });
            };

        }

       public async Task Login(Client user)
       {
            var content = JsonConvert.SerializeObject(user);           
            var apiResponse = await requestHelper.PostRequest(Constants.loginUrl, content);

            if (apiResponse != null)
            {
                //get token and bind to httpheader
                requestHelper.SetTokenHeader(user);
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