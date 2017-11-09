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
	public partial class MainQuestionnaire : ContentPage
	{
		public MainQuestionnaire ()
		{
			InitializeComponent ();
		}

        private void start_test(object sender, EventArgs e)
        {
            Navigation.PushAsync(new QuestionnairePage());
        }
    }
}