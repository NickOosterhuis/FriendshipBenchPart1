using MobileApp.Models;
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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            Init();
        }

        void Init()
        {
            BackgroundColor = Constants.backroundColor;
            Lbl_Username.TextColor = Constants.mainTextColor;
            Lbl_Password.TextColor = Constants.mainTextColor;
            ActivitySpinner.IsVisible = false;
            LoginIcon.HeightRequest = Constants.logoHeight;
            
            Entry_Username.Completed += (sender, e) => Entry_Password.Focus();
            Entry_Password.Completed += (sender, e) => SignInProcedure(sender, e); 
        }

        void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            if (user.CheckInformation())
            {
                DisplayAlert("Login", "Login Success", "Ok");
                App.UserDatabase.SaveUser(user);
            }
            else
            {
                App.UserDatabase.GetUser();
                DisplayAlert("Login", "Login Not Correct", "Empty username or password." , "Ok");
            }
        }
    }
}