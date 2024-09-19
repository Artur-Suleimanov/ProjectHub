using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess.Interfaces
{
    public interface IProjectData
    {
        void AddUserInProject(int projectId, string userId, int roleId);
        ProjectModel CreateNewProject(string userId, string name, string description);
        void CreateTask(string name, string description, string initiatorId, string executorId, int projectId, int stateId);
        void DeleteTask(int taskId);
        void DeleteUserInProject(int projectId, string userId);
        ProjectModel GetProjectById(int id);
        List<ProjectModel> GetProjectsByUserId(string id);
        List<ProjectModel> GetProjectsWhereUserIsInitiator(string id);
        List<TaskModel> GetProjectTasks(int projectId);
        List<UserModel> GetProjectUsers(int id);
        void TransferTasksToNewExecuter(int projectId, string oldUserId, string newUserId);
    }
}