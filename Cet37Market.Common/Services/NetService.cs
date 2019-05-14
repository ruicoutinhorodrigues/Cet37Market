using Cet37Market.Common.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cet37Market.Common.Services
{
    public class NetService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Turn on your internet"
                    //Message = Languages.TurnOnInternet,
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "google.com");

            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check your connection"
                   /* Message = Languages.InternetConnection*/,
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok"
            };
        }
    }
}
