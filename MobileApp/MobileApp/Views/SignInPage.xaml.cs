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

        public Entry email;
        public Entry password;
        
        public SignInPage()
        {
            email = new Entry
            {
                Placeholder = "Email"
            };

            password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true
            };

            var signInButton = new Button
            {
                Text = "Sign In"
            };

            var registerButton = new Button
            {
                Text = "Register"
            };

            

            Content = new StackLayout
            {
                Padding = 30,
                Spacing = 10,
                Children =
                {
                    new Label {Text = "Sign in", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), HorizontalOptions = LayoutOptions.Center},
                    email,
                    password,
                    signInButton,
                    registerButton
                }
            };

            registerButton.Clicked += (object sender, EventArgs e) =>
            {
                Navigation.PushAsync(new RegisterPage());
            };

            signInButton.Clicked += async (object sender, EventArgs e) =>
            {
                Login(new User { Email = email.Text, Password = password.Text });
            };

        }

       public async void Login(User user)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(user);
      
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string myContent = await content.ReadAsStringAsync();

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
                Debug.WriteLine(myContent);
            }
            else
            {
                Debug.WriteLine("Er is iets fout gegaan :(");
                Debug.WriteLine(response.Headers);
            }  
        }
    }
}