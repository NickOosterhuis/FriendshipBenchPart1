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
	public partial class LandingPage : MasterDetailPage
	{
		public LandingPage ()
		{
			InitializeComponent ();
            DisplayAlert("Hello", "Swipe right to open the menu", "Got it!");
        }

        private void About_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AboutPage());
        }

        private void Appointments_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AppointmentsPage());
        }

        private void Profile_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new EditUserPage());
        }

        private void Messages_Clicked(object sender, EventArgs e)
        {

        }

        private void Questionnaire_Clicked(object sender, EventArgs e)
        {

        }

        private void Logout_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new Signout());
        }
    }
}