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

            var newProject = _projectData.CreateNewProject(userId, createProjectModel.Name, createProjectModel.Description);
        }
    }
}
