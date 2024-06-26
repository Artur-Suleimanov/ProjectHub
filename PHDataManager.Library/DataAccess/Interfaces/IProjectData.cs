using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess.Interfaces
{
    public interface IProjectData
    {
        void AddUserInProject(int projectId, string userId, int roleId);
        ProjectModel CreateNewProject(string userId, string name, string description);
        List<ProjectModel> GetProjectsByUserId(string id);
        List<UserModel> GetProjectUsers(int id);
    }
}