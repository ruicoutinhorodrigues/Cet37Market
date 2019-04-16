using Cet37Market.UIForms.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cet37Market.UIForms.ViewModels
{
    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public LoginViewModel()
        {
            this.Email = "rui.coutinho.rodrigues@gmail.com";
            this.Password = "lagarto75";
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

            if (!this.Email.Equals("rui.coutinho.rodrigues@gmail.com") || !this.Password.Equals("lagarto75"))
            {
                await Application.Current.MainPage.DisplayAlert(
                   "Ok",
                   "User or passsword wrong.",
                   "Accept");
            }


            //await Application.Current.MainPage.DisplayAlert(
            //        "Ok",
            //        "Fuck entrámos!!!",
            //        "Accept");

            MainViewModel.GetInstance().Products = new ProductsViewModel();

            await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
        }
    }
}
