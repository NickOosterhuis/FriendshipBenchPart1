using MobileApp.Helpers;
using MobileApp.Models;
using MobileApp.ViewModels;
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
    public partial class UserEditPage : ContentPage
    {
        APIRequestHelper apiRequestHelper; 
        DateTime now = DateTime.Now.AddYears(-10);
        Client user; 

        public UserEditPage(Client user)
        {
            apiRequestHelper = new APIRequestHelper();
            InitializeComponent();

            var email = new Label
            {
                Text = user.Email,
            };

            var password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
            };

            var confirmPassword = new Entry
            {
                Placeholder = "Confirm Password",
                IsPassword = true,
            };

            var streetName = new Entry
            {
                Text = user.StreetName
            };

            var houseNumber = new Entry
            {
                Text = user.HouseNumber
            };

            var district = new Entry
            {
                Text = user.District
            };

            var province = new Entry
            {
                Text = user.Province
            };

            var editButton = new Button
            {
                Text = "Edit"
            };

            var stack = new StackLayout
            {
                Padding = 30,
                Spacing = 10,
                Children =
                {
                    new Label {Text = "Edit", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), HorizontalOptions = LayoutOptions.Center},
                    email,
                    password,
                    confirmPassword,
                    streetName,
                    houseNumber,
                    province,
                    district,
                    editButton
                }
            };

            Content = new ScrollView { Content = stack };

            editButton.Clicked += async (object sender, EventArgs e) =>
            {
                if (!password.Text.Equals(confirmPassword.Text))
                {
                    await DisplayAlert("Error", "Confirm password doesn't match password", "Cencel");
                }

                await UpdateUserAsync(user = new Client
                {
                    Password = password.Text,
                    ConfirmPassword = confirmPassword.Text,
                    StreetName = streetName.Text,
                    HouseNumber = houseNumber.Text,
                    Province = province.Text,
                    District = district.Text,
                });
            };
        }

        protected async void OnDisappearing()
        {
            if (user.Email != null && user.Password != null)
            {
                LoginViewModel vm = new LoginViewModel
                {
                    Email = user.Email,
                    Password = user.Password,
                };

                var content = JsonConvert.SerializeObject(vm);
                var tokenJson = await apiRequestHelper.GetAccessToken(content);

                dynamic token = JsonConvert.DeserializeObject(tokenJson);

                Debug.WriteLine((string)token.token);

                App.Current.Properties["email"] = user.Email;
                App.Current.Properties["token"] = (string)token.token;
                App.Current.Properties["password"] = user.Password;

                App.Current.SavePropertiesAsync();
            }
        }

        private async Task UpdateUserAsync(Client user)
        {
            EditUserViewModel vm = new EditUserViewModel
            {
                District = user.District,
                StreetName = user.StreetName,
                Password = user.Password,
                Province = user.Province,
                HouseNumber = user.HouseNumber
            };

            string content = JsonConvert.SerializeObject(vm);

            apiRequestHelper.SetTokenHeader();
            string response = await apiRequestHelper.PutRequest(Constants.editClientUrl + "/" + user.Email, content);

            if (response != null)
            {
                DisplayAlert("Update message", "User updated successfully", "Okay");
                
            }
            else
            {
                DisplayAlert("Error", "Sorry, something went wrong. Please try again later.", "Okay");
            }
        }

    }
}