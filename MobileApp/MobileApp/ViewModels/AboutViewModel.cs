﻿using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace MobileApp
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("http://www.friendshipbenchzimbabwe.org/")));
        }

        public ICommand OpenWebCommand { get; }
    }
}