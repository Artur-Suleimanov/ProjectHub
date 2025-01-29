using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using PHDesktopUI.Librery.Models;
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
        private readonly ILoggedInUserModel _loggedInUser;
        private HttpClient _apiClient;

        public HttpClient ApiClient { get => _apiClient; }

        public APIHelper(IConfiguration config,
                         ILoggedInUserModel loggedInUserModel)
        {
            _config = config;
            _loggedInUser = loggedInUserModel;
            InitializeClient();
        }

        private void InitializeClient()
        {
            string api = _config.GetValue<string>("api")!;

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
                    var errorContent = await response.Content.ReadAsStringAsync();
                    if(string.IsNullOrWhiteSpace(errorContent) == false)
                    {
                        throw new Exception(errorContent);
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                    
                }
            }
        }

        public async Task GetLoggedInUserInfo(string token)
        {
            _apiClient.DefaultRequestHeaders.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            using (HttpResponseMessage response = await _apiClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                    _loggedInUser.CreateDate = result.CreateDate;
                    _loggedInUser.EmailAddress = result.EmailAddress;
                    _loggedInUser.Token = result.Token;
                    _loggedInUser.FirstName = result.FirstName;
                    _loggedInUser.LastName = result.LastName;
                    _loggedInUser.Id = result.Id;
                    _loggedInUser.Token = token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
