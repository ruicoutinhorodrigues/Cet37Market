using Cet37Market.Common.Models;
using Cet37Market.Common.Services;
using Cet37Market.UIForms.Views;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cet37Market.UIForms.ViewModels
{
    public class LoginViewModel:BaseViewModel
        //INotifyPropertyChanged
    {
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private NetService netService;
        public string Email { get; set; }

        public string Password { get; set; }

        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.Email = "rui.coutinho.rodrigues@gmail.com";
            this.Password = "lagarto75";
            this.isEnabled = true;
            this.netService = new NetService();
        }

        //public event PropertyChangedEventHandler PropertyChanged;

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }

        public bool IsEnable
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }

        public ICommand LoginCommand => new RelayCommand(Login);

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email",
                    "Accept");

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an password",
                    "Accept");

                return;
            }

            this.isRunning = true;
            this.isEnabled = false;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var connection = await this.netService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Accept");
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetTokenAsync(
                url,
                "/Account",
                "/CreateToken",
                request);

            this.isRunning = false;
            this.isEnabled = true;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Email or password incorrect",
                    "Accept");

                return;
            }

            var token = (TokenResponse)response.Result;

            var mainViewModel = MainViewModel.GetInstance();

            mainViewModel.Token = token;

            mainViewModel.Products = new ProductsViewModel();

            //await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());

            Application.Current.MainPage = new MasterPage();

            //if (!this.Email.Equals("rui.coutinho.rodrigues@gmail.com") || !this.Password.Equals("lagarto75"))
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //       "Ok",
            //       "User or passsword wrong.",
            //       "Accept");
            //}


            //await Application.Current.MainPage.DisplayAlert(
            //        "Ok",
            //        "Fuck entrámos!!!",
            //        "Accept");

            //MainViewModel.GetInstance().Products = new ProductsViewModel();

            //await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }
    }
}
