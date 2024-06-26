using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public interface IUserEndpoint
    {
        Task<bool> CheckUserProjectMembership(string userId, int projectId);
        Task<UserModel> GetUserByEmail(string email);
    }
}