using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TCC_APP.Views;
using Xamarin.Forms.Internals;
using System.Diagnostics;
using System.Threading.Tasks;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TCC_APP
{
    public partial class App : Application
    {
        public static bool IsUserLoggedIn { get; set; }
        public App()
        {
            InitializeComponent();

            Xamarin.Forms.Internals.Log.Listeners.Add(new DelegateLogListener((arg1, arg2) => Debug.WriteLine(arg2)));
#if !DEBUG
            IsUserLoggedIn = true;
#endif

            MainPage = new NavigationPage(new MainPage());

            //if (!IsUserLoggedIn)
            //{
            //    //MainPage = new NavigationPage(new Login());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new MainPage());
            //}
        }

        public static async void NavigatiPage(Page name)
        {
            Application.Current.MainPage = new NavigationPage(new ListasDeCompraPage());
            // new NavigationPage(new UsersListPage());
            //Application.Current.MainPage = navPage;
            await name.Navigation.PushAsync(new ListasDeCompraPage());
        }

        internal static async Task NavigatiPageAsync(Page name)
        {
            Application.Current.MainPage = new NavigationPage(new ListasDeCompraPage());
            // new NavigationPage(new UsersListPage());
            //Application.Current.MainPage = navPage;
            await name.Navigation.PushAsync(new ListasDeCompraPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
