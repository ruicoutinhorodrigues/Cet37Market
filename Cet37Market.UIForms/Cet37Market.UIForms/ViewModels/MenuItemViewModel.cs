using Cet37Market.Common.Models;
using Cet37Market.UIForms.Views;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cet37Market.UIForms.ViewModels
{
    public class MenuItemViewModel : Common.Models.MenuView
    {
        public ICommand SelectMenuCommand => new RelayCommand(this.selectMenu);

        private async void selectMenu()
        {
            App.Master.IsPresented = false;
            //var mainViewModel = MainViewModel.GetInstance();

            switch (this.PageName)
            {
                case "AboutPage":
                    await App.Navigator.PushAsync(new AboutPage());
                    break;

                case "SetupPage":
                    await App.Navigator.PushAsync(new SetupPage());
                    break;

                default:
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    break;
            }
        }
    }
}
