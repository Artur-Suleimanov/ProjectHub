using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess.Interfaces
{
    public interface IProjectData
    {
        void AddUserInProject(int projectId, string userId, int roleId);
        ProjectModel CreateNewProject(string userId, string name, string description);
        void DeleteUserInProject(int projectId, string userId);
        List<ProjectModel> GetProjectsByUserId(string id);
        List<TaskModel> GetProjectTasks(int projectId);
        List<UserModel> GetProjectUsers(int id);
        void TransferTasksToNewExecuter(int projectId, string oldUserId, string newUserId);
    }
}