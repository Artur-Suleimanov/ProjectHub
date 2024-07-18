using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PHDataManager.Library.DataAccess.Interfaces;
using PHDataManager.Library.Models;
using System.Security.Claims;

namespace PHApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectData _projectData;

        public ProjectController(IProjectData projectData)
        {
            _projectData = projectData;
        }

        [HttpGet]
        public List<ProjectModel> GetProjectsWhereUserIsInitiator()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new Exception("Cannot get Authorized User");
            }

            return _projectData.GetProjectsByUserId(userId);
        }

        [HttpPost]
        [Route("CreateProject")]
        public void CreateProject(CreateProjectModel createProjectModel)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new Exception("Cannot get Authorized User");
            }

            var newProject = _projectData.CreateNewProject(userId, createProjectModel.Name, createProjectModel.Description);
        }

        [HttpGet]
        [Route("GetProjectUsers/{id}")]
        public List<UserModel> GetProjectUsers(int id)
        {
            return _projectData.GetProjectUsers(id);
        }

        [HttpPost]
        [Route("AddUserInProject")]
        public void Run()
        {
            var form = Request.Form;

            // Получаем отдельные данные:
            int projectId = int.Parse(form["projectId"]!);
            string? userId = form["userId"];
            int roleId = int.Parse(form["roleId"]!);

            _projectData.AddUserInProject(projectId, userId!, roleId);
        }

        [HttpDelete]
        [Route("DeleteUserFromProject/{projectId}_{olduserId}_{newUserId}")]
        public void DeleteUserFromProject(int projectId, string olduserId, string newUserId)
        {
            // Обновление исполнителя в задачах, где удаляемый пользователь является исполнителем:
            _projectData.TransferTasksToNewExecuter(projectId, olduserId, newUserId);

            // Удаление пользователя из проекта:
            _projectData.DeleteUserInProject(projectId, olduserId);
        }

        [HttpGet]
        [Route("GetProjectTasks/{projectId}")]
        public List<TaskModel> GetProjectTasks(int projectId)
        {
            return _projectData.GetProjectTasks(projectId);
        }

        [HttpPost]
        [Route("CreateTask")]
        public void CreateTask()
        {
            var form = Request.Form;

            // Получение отдельных данных:
            var name = form["name"].ToString();
            var description = form["description"].ToString();
            var initiatorId = form["initiatorId"].ToString();
            var executorId = form["executorId"].ToString();
            var projectId = int.Parse(form["projectId"]);
            var stateId = int.Parse(form["stateId"]);

            _projectData.CreateTask(name, description, initiatorId, executorId, projectId, stateId);

        }
    }
}
