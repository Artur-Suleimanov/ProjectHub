using PHDataManager.Library.DataAccess.Interfaces;
using PHDataManager.Library.Models;

namespace PHDataManager.Library.DataAccess
{
    public class ProjectData : IProjectData
    {
        private readonly ISqlDataAccess _sql;

        public ProjectData(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public List<ProjectModel> GetProjectsByUserId(string id)
        {
            var output = _sql.LoadData<ProjectModel, dynamic>("dbo.spGetProjectsWhereUserIsInitiator", new { Id = id }, "PHData");

            foreach (var project in output)
            {
                project.Tasks = _sql.LoadData<TaskModel, dynamic>("dbo.spGetProjectTasks", new { Id = project.Id }, "PHData");
            }

            return output;
        }
    }
}
