using System.Net.Http.Json;

namespace TaskMgmt.Console
{
    public class ConsoleProject
    {
        /*
        api/groups/{groupId}/projects
        // POST: /groups/{groupId}/projects
        // GET: api/groups/{groupId}/projects
        // GET: api/groups/{groupId}/projects/{id}
        */
        public readonly string ApiUrl;
        private readonly HttpClient _httpClient;

        public ConsoleProject()
        {
            ApiUrl = "https://localhost:7197";
            _httpClient = new HttpClient();
        }

        public async void CreateNewProject(
            string projName, 
            string projDescription,
            int groupId)
        {
            Dtos.ProjectDto proj = new Dtos.ProjectDto
            {
                ProjectName = projName,
                ProjectDescription = projDescription,
            };

            string postRequest =$"{ApiUrl}/api/groups/{groupId}/projects";
            System.Console.WriteLine($"POST: {postRequest} ...");

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(postRequest, proj);
            if(response == null)
            {
                System.Console.WriteLine("Invalid response!");
                return;
            }

            System.Console.WriteLine($"RESPONSE: {response}");

            if (!response.IsSuccessStatusCode)
            {
                System.Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return;
            }
            System.Console.WriteLine($"SUCCESS!");

        }

    }
}
