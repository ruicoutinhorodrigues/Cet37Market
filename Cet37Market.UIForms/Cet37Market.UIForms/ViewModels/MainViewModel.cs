using Cet37Market.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Cet37Market.UIForms.ViewModels
{
    public class MainViewModel
    {
        private static MainViewModel instance;

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public TokenResponse Token { get; set; }
        public LoginViewModel Login { get; set; }
        public ProductsViewModel Products { get; set; }

        public MainViewModel()
        {
            instance = this;
            this.LoadMenus();
        }

        private void LoadMenus()
        {
            var menus = new List<MenuView>
            {
                new MenuView
                {
                    Icon = "ic_info",
                    PageName = "AboutPage",
                    Title = "About"
                },

                 new MenuView
                {
                    Icon = "ic_phonelink_setup",
                    PageName = "SetupPage",
                    Title = "Setup"
                },

                  new MenuView
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Close session"
                },
            };

            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel()
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title
            }).ToList());
        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
    }
}
