﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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
        private readonly ApiClient _client;

        public ConsoleProject(ApiClient client)
        {
            _client = client;
        }

        public async void GetAllProjects(int groupId)
        {
            string endPoint = $"{_client.BaseAddress}/api/groups/{groupId}/projects"; 

            System.Console.WriteLine($"POST: {endPoint} ...");

            var response = await _client.GetAsync(endPoint);

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

            string endPoint = $"{_client.BaseAddress}/api/groups/{groupId}/projects"; 

            System.Console.WriteLine($"POST: {endPoint} ...");

            var request = new HttpRequestMessage(HttpMethod.Post, endPoint);
            string jsonContent = JsonSerializer.Serialize(proj);

            StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = stringContent;
            var response = await _client.PostAsync(endPoint, request);

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
