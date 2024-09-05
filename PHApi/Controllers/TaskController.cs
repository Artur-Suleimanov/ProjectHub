using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PHDataManager.Library.Models;
using PHDataManager.Library.DataAccess;

namespace PHApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskData _taskData;

        public TaskController(ITaskData taskData)
        {
            _taskData = taskData;
        }

        [HttpGet]
        [Route("GetAllStates")]
        public List<StateModel> GetAllStates()
        {
            return _taskData.GetAllStates();
        }

        [HttpPut]
        [Route("UpdateTask")]
        public void UpdateTask(TaskModel task)
        {
            _taskData.UpdateTask(task);
        }

        [HttpGet]
        [Route("GetSolutionText/{id}")]
        public string GetSolutionText(int id)
        {
            return _taskData.GetSolutionText(id);
        }
    }
}
