using System;
using System.Collections.Generic;
using System.Linq;
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

            var address = new Entry
            {
                Placeholder = "Street name"
            };

            var housenumber = new Entry
            {
                Placeholder = "House number",
                Keyboard = Keyboard.Numeric
            };

            var zipcode = new Entry
            {
                Placeholder = "Zipcode"
            };

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
                    address,
                    housenumber,
                    zipcode,
                    genderPicker,
                    birthdayLabel,
                    birhtDay,
                    registerButton
                }
            };

            Content = new ScrollView { Content = stack }; 
        }
    }
}