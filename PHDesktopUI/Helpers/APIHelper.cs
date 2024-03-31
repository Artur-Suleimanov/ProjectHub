using Microsoft.Extensions.Configuration;
using PHDesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PHDesktopUI.Helpers
{
    public class APIHelper : IAPIHelper
    {
        private readonly IConfiguration _config;

        private HttpClient _apiClient;

        public APIHelper(IConfiguration config)
        {
            _config = config;
            InitializeClient();
        }

        private void InitializeClient()
        {
            string api = _config.GetValue<string>("api");

            _apiClient = new HttpClient
            {
                BaseAddress = new Uri(api)
            };
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            var data = new FormUrlEncodedContent(
            [
               //new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", password)
            ]);

            using (HttpResponseMessage response = await _apiClient.PostAsync("/Token", data))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        //public async Task<AuthenticatedUser> Authenticate(string username, string password)
        //{
        //    var data = new FormUrlEncodedContent(new[]
        //    {
        //        //new KeyValuePair<string, string>("grant_type", "password"),
        //        new KeyValuePair<string, string>("username", username),
        //        new KeyValuePair<string, string>("password", password)
        //    });

        //    using (HttpResponseMessage response = await _apiClient.PostAsync("https://localhost:7059/token", data))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}
    }
}
