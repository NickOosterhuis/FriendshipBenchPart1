using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{

    public partial class Home : ContentPage
    {
        Label label;
        public Home()
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
                Text = "At The Friendship Bench, " +
                "we try to reach out to people in Zimbabwe who are in need of help. " +
                "Think about people who suffer from mental disorders, " +
                "unwanted pregnancies or anything else that comes with (mental) problems.",
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
                    button,
                }
            };
        }

            void OnButtonClicked(object sender, EventArgs e)
            {
                Navigation.PushAsync(new Home2());
            }
    }
}

