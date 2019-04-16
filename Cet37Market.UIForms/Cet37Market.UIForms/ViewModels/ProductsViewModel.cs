using Cet37Market.Common.Models;
using Cet37Market.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Cet37Market.UIForms.ViewModels
{
    public class ProductsViewModel:INotifyPropertyChanged
    {
        #region Attributes

        private ApiService apiService;

        private ObservableCollection<Product> products;

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        //TODO: Colocar inotify genérico
        public ObservableCollection<Product> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                if (this.products != value)
                {
                    this.products = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Products"));
                }
            }
        }

        #endregion


        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }

        private async void LoadProducts()
        {
            var response = await this.apiService.GetListAsync<Product>(
                "http://ruirodrigues-001-site1.etempurl.com",
                "/api",
                "/Products"
                );

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");

                return;
            }

            var myProducts = (List<Product>)response.Result;

            this.Products = new ObservableCollection<Product>(myProducts);
        }
    }
}
