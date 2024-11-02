using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public interface IUserEndpoint
    {
        Task<bool> CheckUserProjectMembership(string userId, int projectId);
        Task CreateUser(CreateUserModel model);
        Task<UserModel> GetUserByEmail(string email);
    }
}