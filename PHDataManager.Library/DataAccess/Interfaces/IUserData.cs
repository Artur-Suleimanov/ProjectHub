using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess.Interfaces
{
    public interface IUserData
    {
        List<UserModel> GetUserById(string id);
    }
}