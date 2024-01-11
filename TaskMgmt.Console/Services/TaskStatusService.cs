using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskMgmt.Console.Services
{

    public class TaskStatusService
    {
        private readonly ApiClient apiClient = new ApiClient(ApiConstants.ApiUrl);

        public void GetTasks(string userToken, int groupId, int projectId)
        {
            var response = apiClient.GetToken($"groups/{groupId}/projects/{projectId}/statuses", userToken);
            var content = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("\t\t\tTask Details");
                var statuses = JsonSerializer.Deserialize<List<StatusDto>>(content);
                foreach (var status in statuses)
                {
                    System.Console.WriteLine($"Status ID: {status.projectTaskStatusId} Text: {status.statusText} Color: {status.statusColor}");
                }
                System.Console.WriteLine("\n\n");
            }
        }
    }

    public class StatusDto
    {
        public int projectTaskStatusId { get; set; }
        public string statusText { get; set; } = null!;
        public string statusColor { get; set; } = null!;
    }
}
