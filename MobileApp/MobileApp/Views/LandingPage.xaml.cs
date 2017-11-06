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
            Detail = new NavigationPage(new AboutPage());
        }

        private void About_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AboutPage());
            IsPresented = false;
        }

        private void Appointments_Clicked(object sender, EventArgs e)
        {
            Detail = new NavigationPage(new AppointmentsPage());
            IsPresented = false;
        }

        private void Profile_Clicked(object sender, EventArgs e)
        {

        }

        private void Messages_Clicked(object sender, EventArgs e)
        {

        }

        private void Questionnaire_Clicked(object sender, EventArgs e)
        {

        }
    }
}