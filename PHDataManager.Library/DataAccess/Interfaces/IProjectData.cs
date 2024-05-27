using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess.Interfaces
{
    public interface IProjectData
    {
        ProjectModel CreateNewProject(string userId, string name, string description);
        List<ProjectModel> GetProjectsByUserId(string id);
    }
}