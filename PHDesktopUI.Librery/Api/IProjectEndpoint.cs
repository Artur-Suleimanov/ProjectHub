using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public interface IProjectEndpoint
    {
        Task CreateProject(CreateProjectModel createProjectModel);
        Task<List<ProjectModel>> GetAll();
    }
}