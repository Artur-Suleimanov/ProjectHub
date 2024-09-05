using PHDesktopUI.Helpers;
using PHDesktopUI.Librery.Models;

namespace PHDesktopUI.Librery.Api
{
    public class TaskEndpoint : ITaskEndpoint
    {
        private readonly IAPIHelper _aPIHelper;

        public TaskEndpoint(IAPIHelper aPIHelper)
        {
            _aPIHelper = aPIHelper;
        }

        public async Task<List<StateModel>> GetAllStates()
        {
            using (HttpResponseMessage response = await _aPIHelper.ApiClient.GetAsync("/api/Task/GetAllStates"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<StateModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task UpdateTask(TaskModel task)
        {
            using (HttpResponseMessage response = await _aPIHelper.ApiClient.PutAsJsonAsync("/api/Task/UpdateTask", task))
            {
                if (response.IsSuccessStatusCode == false)
                    throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<string> GetSolutionText(int? id)
        {
            using (HttpResponseMessage response = await _aPIHelper.ApiClient.GetAsync($"/api/Task/GetSolutionText/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
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
