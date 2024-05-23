using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public interface IProjectEndpoint
    {
        Task<List<ProjectModel>> GetAll();
    }
}