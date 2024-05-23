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
    }
}
