using PHDesktopUI.Helpers;
using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public class UserEndpoint : IUserEndpoint
    {
        private readonly IAPIHelper _apIHelper;

        public UserEndpoint(IAPIHelper apIHelper)
        {
            _apIHelper = apIHelper;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            using (HttpResponseMessage response = await _apIHelper.ApiClient.GetAsync($"/api/User/GetUserByEmail/{email}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<UserModel>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<bool> CheckUserProjectMembership(string userId, int projectId)
        {
            using (HttpResponseMessage response = await _apIHelper.ApiClient.GetAsync($"/api/User/CheckUserProjectMembership/{userId}_{projectId}"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<bool>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task CreateUser(CreateUserModel model)
        {
            var data = new { model.FirstName, model.LastName, model.EmailAddress, model.Password };

            using (HttpResponseMessage response = await _apIHelper.ApiClient.PostAsJsonAsync("/api/User/Register", data))
            {
                if (response.IsSuccessStatusCode == false) 
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
