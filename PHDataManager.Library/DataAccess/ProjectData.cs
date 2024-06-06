using PHDataManager.Library.DataAccess.Interfaces;
using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess
{
    public class ProjectData : IProjectData
    {
        private readonly ISqlDataAccess _sql;
        private readonly IUserData _userData;

        public ProjectData(ISqlDataAccess sql,
                           IUserData userData)
        {
            _sql = sql;
            _userData = userData;
        }

        public List<ProjectModel> GetProjectsByUserId(string id)
        {
            var output = _sql.LoadData<ProjectModel, dynamic>("dbo.spGetProjectsWhereUserIsInitiator", new { Id = id }, "PHData");

            foreach (var project in output)
            {
                // Получение задач в проекте:
                project.Tasks = _sql.LoadData<TaskModel, dynamic>("dbo.spGetProjectTasks", new { Id = project.Id }, "PHData");

                if (project.Tasks == null)
                    continue;

                // Получение исполнителей задач:
                foreach (var task in project.Tasks)
                {
                    task.Executor = _userData.GetUserById(task.ExecutorId)[0];
                }
            }

            return output;
        }

        public ProjectModel CreateNewProject(string userId, string name, string description)
        {
            var newProjectId = _sql.LoadData<ProjectModel, dynamic>("dbo.spCreateProject", new { UserId = userId, ProjectName = name, ProjectDescription = description }, "PHData");

            return newProjectId[0];
        }

        public List<UserModel> GetProjectUsers(int id)
        {
            var output = _sql.LoadData<UserModel, dynamic>("dbo.spProjectParticipants", new { Id = id }, "PHData");

            return output;
        }
    }
}
