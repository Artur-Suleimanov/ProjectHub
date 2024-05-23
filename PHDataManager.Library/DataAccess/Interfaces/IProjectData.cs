using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess.Interfaces
{
    public interface IProjectData
    {
        List<ProjectModel> GetProjectsByUserId(string id);
    }
}