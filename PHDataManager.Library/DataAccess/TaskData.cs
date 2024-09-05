using PHDataManager.Library.DataAccess.Interfaces;
using PHDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHDataManager.Library.DataAccess
{
    public class TaskData : ITaskData
    {
        private readonly ISqlDataAccess _sql;

        public TaskData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<StateModel> GetAllStates()
        {
            var output = _sql.LoadData<StateModel, dynamic>("dbo.spGetAllStates", new { }, "PHData");

            return output;
        }

        public string GetSolutionText(int id)
        {
            var output = _sql.LoadData<string, dynamic>("spGetTaskSolutionText", new { id }, "PHData");

            return output[0];
        }

        public void UpdateTask(TaskModel task)
        {
            _sql.SaveData("spUpdateTask", new { Id = task.Id, Name = task.Name, Description = task.Description, 
                ExecutorId = task.ExecutorId, StateId = task.StateId, SolutionText = task.SolutionText}, "PHData");
        }
    }
}
