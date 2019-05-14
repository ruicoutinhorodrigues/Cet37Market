using Cet37Market.UIForms.ViewModels;
using Cet37Market.UIForms.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cet37Market.UIForms
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }

        public static MasterPage Master { get; internal set; }



        public App()
        {
            InitializeComponent();

            MainViewModel.GetInstance().Login = new LoginViewModel();

            MainPage = new NavigationPage(new LoginPage());
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
