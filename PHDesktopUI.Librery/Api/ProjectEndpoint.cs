using PHDesktopUI.Helpers;
using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public class ProjectEndpoint : IProjectEndpoint
    {
        private readonly IAPIHelper _apiHelper;

        public ProjectEndpoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<ProjectModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Project"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ProjectModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task CreateProject(CreateProjectModel createProjectModel)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Project/CreateProject", createProjectModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    await response.Content.ReadAsAsync<List<ProjectModel>>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<UserModel>> GetProjectUsers(int id)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/Project/GetProjectUsers/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<UserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task AddUserInProject(int projectId, string userId, int roleId)
        {
            var data = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("projectId", projectId.ToString()),
                new KeyValuePair<string, string>("userId", userId),
                new KeyValuePair<string, string>("roleId", roleId.ToString())
            ]);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync($"/api/Project/AddUserInProject", data))
            {
                if(response.IsSuccessStatusCode == false)
                    throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task DeleteUserFromProject(int projectId, string oldUserId, string newUserId)
        {
            using(HttpResponseMessage response = await _apiHelper.ApiClient.DeleteAsync($"/api/Project/DeleteUserFromProject/{projectId}_{oldUserId}_{newUserId}"))
            {
                if(response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<List<TaskModel>> GetProjectTasks(int projectId)
        {
            using(HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/api/Project/GetProjectTasks/{projectId}"))
            {
                if(response.IsSuccessStatusCode == true)
                {
                    var result = await response.Content.ReadAsAsync<List<TaskModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task CreateTask(string name, string description, 
            string initiatorId, string executorId, int projectId, int stateId)
        {
            var data = new FormUrlEncodedContent(
                [
                    new KeyValuePair<string, string>("name", name),
                    new KeyValuePair<string, string>("description", description),
                    new KeyValuePair<string, string>("initiatorId", initiatorId),
                    new KeyValuePair<string, string>("executorId", executorId),
                    new KeyValuePair<string, string>("projectId", projectId.ToString()),
                    new KeyValuePair<string, string>("stateId", stateId.ToString())
                ]);

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsync($"/api/Project/CreateTask", data))
            {
                if(response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
