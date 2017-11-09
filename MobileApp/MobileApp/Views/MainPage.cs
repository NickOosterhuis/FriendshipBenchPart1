using MobileApp.Views;
using System;

using Xamarin.Forms;

namespace MobileApp
{
    public class MainPage : TabbedPage
    {
        public MainPage()
        {
            Page itemsPage, aboutPage, signInPage = null;

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    signInPage = new NavigationPage(new SignInPage())
                    {
                        Title = "Sign In"
                    };
                    itemsPage = new NavigationPage(new AppointmentsPage())
                    {
                        Title = "Appointments"
                    };

                    aboutPage = new NavigationPage(new AboutPage())
                    {
                        Title = "About"
                    };
                    itemsPage.Icon = "tab_feed.png";
                    aboutPage.Icon = "tab_about.png";
                    break;
                default:
                    signInPage = new SignInPage()
                    {
                        Title = "Sign In"
                    };
                    itemsPage = new AppointmentsPage()
                    {
                        Title = "Appointments"
                    };
                    aboutPage = new AboutPage()
                    {
                        Title = "About"
                    };
                    break;
            }

            Children.Add(signInPage);
            Children.Add(itemsPage);
            Children.Add(aboutPage);
            
            Title = Children[0].Title;
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            Title = CurrentPage?.Title ?? string.Empty;
        }
    }
}
