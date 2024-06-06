﻿using PHDesktopUI.Helpers;
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
    }
}
