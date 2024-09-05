using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess
{
    public interface ITaskData
    {
        List<StateModel> GetAllStates();
        string GetSolutionText(int id);
        void UpdateTask(TaskModel task);
    }
}