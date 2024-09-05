using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public interface ITaskEndpoint
    {
        Task<List<StateModel>> GetAllStates();
        Task<string> GetSolutionText(int? id);
        Task UpdateTask(TaskModel task);
    }
}