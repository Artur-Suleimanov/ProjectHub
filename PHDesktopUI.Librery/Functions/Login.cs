using PHDesktopUI.Helpers;

namespace PHDesktopUI.Librery.Functions
{
    public class Login : ILogin
    {
        private readonly IAPIHelper _apiHelper;

        public Login(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task Run(string _userName, string _password)
        {
            var result = await _apiHelper.Authenticate(_userName!, _password!);

            // Capture more information about the user:
            await _apiHelper.GetLoggedInUserInfo(result.Access_Token);
        }
    }
}
