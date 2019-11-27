using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace TCC_APP.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://google.com")));
        }

        public ICommand OpenWebCommand { get; }
    }
}