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
    public partial class RegisterPage : ContentPage
    {

        DateTime now = DateTime.Now.AddYears(-10); 

        List<string> genderList = new List<string>
        {
            "Male",
            "Female"
        };


        public RegisterPage()
        {
            var email = new Entry
            {
                Placeholder = "Email",
                Keyboard = Keyboard.Email
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

            var firstname = new Entry
            {
                Placeholder = "First Name",
            };

            var lastname = new Entry
            {
                Placeholder = "Last Name",
            };

            var address = new Entry
            {
                Placeholder = "Address"
            };

            var district = new Entry
            {
                Placeholder = "District"
            };

            var province = new Entry
            {
                Placeholder = "Province"
            };

            //var housenumber = new Entry
            //{
            //    Placeholder = "House number",
            //    Keyboard = Keyboard.Numeric
            //};

            //var zipcode = new Entry
            //{
            //    Placeholder = "Zipcode"
            //};

            Picker genderPicker = new Picker
            {
                Title = "Gender",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            foreach (string gender in genderList)
            {
                genderPicker.Items.Add(gender); 
            }

            Label birthdayLabel = new Label
            {
                Text = "Birthday:"
            };

            DatePicker birhtDay = new DatePicker
            {       
                Format = "D",
                MinimumDate = new DateTime(1950, 1, 1),
                MaximumDate = now, 
                VerticalOptions =LayoutOptions.CenterAndExpand,
                
            };

            var registerButton = new Button
            {
                Text = "Register"
            };

            var stack = new StackLayout
            {
                Padding = 30,
                Spacing = 10,
                Children =
                {
                    new Label {Text = "Register", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), HorizontalOptions = LayoutOptions.Center},
                    email,
                    password,
                    confirmPassword,
                    firstname,
                    lastname,
                    genderPicker,
                    birthdayLabel,
                    birhtDay,
                    address,
                    province, 
                    district,
                    registerButton
                }
            };

            Content = new ScrollView { Content = stack }; 

            registerButton.Clicked += async (object sender, EventArgs e) =>
            {
                await Register(new User
                {
                    Email = email.Text,
                    Password = password.Text,
                    ConfirmPassword = confirmPassword.Text,
                    Adress = address.Text,
                    BirthDay = birhtDay.Date,
                    Province = province.Text,
                    District = district.Text,
                    Gender = genderPicker.SelectedItem.ToString(), 
                    FirstName = firstname.Text,
                    LastName = lastname.Text, 
                });
            };
        }
        public async Task Register(User user)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(user);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            string readableContent = await content.ReadAsStringAsync();

            var response = new HttpResponseMessage();

            try
            {
                response = await client.PostAsync(Constants.registerUrl, content);
            }
            catch (Exception e)
            {
                await DisplayAlert("ERROR", e.Message, "Cancel");
                Debug.WriteLine("HTTP ERROR: " + e.Message);
            }

            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@" User Successfully registered in");
                Debug.WriteLine(readableContent);
                await Navigation.PushAsync(new AboutPage());
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