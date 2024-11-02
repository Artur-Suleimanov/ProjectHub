using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess.Interfaces
{
    public interface IUserData
    {
        List<int> CheckUserProjectMembership(string userId, int projectId);
        void CreateUser(UserModel user);
        List<UserModel> GetUserByEmail(string email);
        List<UserModel> GetUserById(string id);
    }
}