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
                project.Tasks = GetProjectTasks((int)project.Id!);

                if (project.Tasks == null)
                    continue;

                GetTasksExecuters(project.Tasks);
            }

            return output;
        }

        private void GetTasksExecuters(List<TaskModel> tasks)
        {
            // Получение исполнителей задач:
            foreach (var task in tasks)
            {
                task.Executor = _userData.GetUserById(task.ExecutorId)[0];
            }
        }

        public List<TaskModel> GetProjectTasks(int projectId)
        {
            // Получение задач в проекте:
            List<TaskModel> tasks = _sql.LoadData<TaskModel, dynamic>("dbo.spGetProjectTasks", new { Id = projectId }, "PHData");

            GetTasksExecuters(tasks);

            return tasks;
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

        public void AddUserInProject(int projectId, string userId, int roleId)
        {
            _sql.SaveData("spAddUserInProject", new { projectId, userId, roleId }, "PHData");
        }

        public void DeleteUserInProject(int projectId, string userId) 
        {
            _sql.SaveData("spDeleteUserFromProject", new { projectId, userId }, "PHData");
        }

        public void TransferTasksToNewExecuter(int projectId, string oldUserId, string newUserId)
        {
            _sql.SaveData("spTransferTasksToNewExecuter", new { ProjectId = projectId, OldExecuterId = oldUserId, NewExecuterId = newUserId }, "PHData");
        }

        public void CreateTask(string name, string description, string initiatorId, string executorId, int projectId, int stateId)
        {
            _sql.SaveData("spCreateTask", new { Name = name, Description = description, InitiatorId = initiatorId, ExecutorId = executorId, ProjectId = projectId, StateId = stateId }, "PHData");
        }
    }
}
