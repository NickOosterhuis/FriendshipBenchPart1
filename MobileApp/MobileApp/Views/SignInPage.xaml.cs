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
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            var email = new Entry
            {
                Placeholder = "Email"
            };

            var password = new Entry
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

        }
    }
}