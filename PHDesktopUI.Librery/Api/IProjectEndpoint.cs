using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public interface IProjectEndpoint
    {
        Task AddUserInProject(int projectId, string userId, int roleId);
        Task CreateProject(CreateProjectModel createProjectModel);
        Task DeleteUserFromProject(int projectId, string oldUserId, string newUserId);
        Task<List<ProjectModel>> GetAll();
        Task<List<TaskModel>> GetProjectTasks(int projectId);
        Task<List<UserModel>> GetProjectUsers(int id);
    }
}