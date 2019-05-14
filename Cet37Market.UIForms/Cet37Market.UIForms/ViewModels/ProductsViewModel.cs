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
    public class ProductsViewModel:BaseViewModel
        //INotifyPropertyChanged
    {
        #region Attributes

        private ApiService apiService;

        private ObservableCollection<Product> products;

        private NetService netService;

        private bool isRefreshing;

        #endregion

        #region Events

        //public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        //TODO: Colocar inotify genérico
        public ObservableCollection<Product> Products
        {
            get => this.products;
            set => this.SetValue(ref this.products, value);
            //get
            //{
            //    return this.products;
            //}
            //set
            //{
            //    //if (this.products != value)
            //    //{

            //    //    //this.products = value;
            //    //    //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Products"));
            //    //}
            //}
        }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
            //get
            //{
            //    return this.isRefreshing;
            //}
            //set
            //{
            //    if (this.isRefreshing != value)
            //    {
            //        this.isRefreshing = value;
            //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRefreshing"));
            //    }
            //}
        }

        #endregion


        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
            this.netService = new NetService();
        }

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            //var connection = await this.netService.CheckConnection();

            //if (!connection.IsSuccess)
            //{
            //    await Application.Current.MainPage.DisplayAlert(
            //        "Error",
            //        connection.Message,
            //        "Accept");
            //    return;
            //}


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Product>(
                url,
                "/api",
                "/Products",
                "bearer",
                MainViewModel.GetInstance().Token.Token);


            //var response = await this.apiService.GetListAsync<Product>(
            //    "http://ruirodrigues-001-site1.etempurl.com",
            //    "/api",
            //    "/Products"
            //    );

            this.IsRefreshing = false;

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
