//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TaskMgmt.Console.Dtos.Task;

//namespace TaskMgmt.Console
//{
//    public class TaskConsole
//    {
//        private readonly string ApiUrl = "https://localhost:7197/api/";
//        private string userToken;
//        private readonly ApiClient apiClient;

//        public TaskConsole()
//        {
//            apiClient = new ApiClient(ApiUrl);
//        }

//        public async void CreateNewTask(
//            int groupId,
//            int projectId,
//            string Description,
//            DateTime DueDate,
//            string AsigneeMail,
//            int CurrentStatusId)
//        {
//            var newTask= new TaskCreateDto
//            {
//                Description = Description,
//                DueDate = DueDate,
//                AssigneeMail = AsigneeMail,
//                CurrentStatusId = CurrentStatusId
//            };

//            try
//            {
//                var response = await apiClient.PostAsync($"groups/{groupId}/projects/{projectId}/tasks", newTask);
//                if (response.IsSuccessStatusCode)
//                {
//                    System.Console.WriteLine("Welcome.");
//                }
//                else
//                {
//                    System.Console.WriteLine("SignUp Failed.");
//                }
//            }
//            catch (Exception ex)
//            {
//                System.Console.WriteLine(ex.Message);
//            }
        
//        }
//    }
//}
