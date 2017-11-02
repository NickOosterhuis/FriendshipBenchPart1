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
    public partial class Home2 : ContentPage
    {
        Label label;
        public Home2()
        {
            InitializeComponent();

            Label header = new Label
            {
                Text = "Welkom !",
                Font = Font.BoldSystemFontOfSize(35),
                HorizontalOptions = LayoutOptions.Center
            };

            Label text = new Label
            {
                Text = "We work with a SSQ-14, " +
                "a question list which can determine if you are in need of help. Later on, " +
                "you will receive more information about this question list.",
                Font = Font.BoldSystemFontOfSize(20),
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            Label loginText = new Label
            {
                Text = "But first," +
                    "let's login or create an account!",
                Font = Font.BoldSystemFontOfSize(20),
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center
                   
            };

            Button button = new Button
            {
                Text = "Continue",
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            button.Clicked += OnButtonClicked;

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    text,
                    loginText,
                    button,
                }
            };
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SignInPage());
        }
    }
}