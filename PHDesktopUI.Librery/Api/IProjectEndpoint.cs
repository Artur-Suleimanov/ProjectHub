using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public interface IProjectEndpoint
    {
        Task AddUserInProject(int projectId, string userId, int roleId);
        Task CreateProject(CreateProjectModel createProjectModel);
        Task<List<ProjectModel>> GetAll();
        Task<List<UserModel>> GetProjectUsers(int id);
    }
}