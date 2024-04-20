using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using To_Dooey_Interface.ViewModels;

namespace To_Dooey_Interface.Services
{ 
    public class ApiService
    {
        
        private static HttpClient client = new HttpClient();
        private static string baseUrl = "http://localhost:5138/api/";
        

       
        public ApiService()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
        }
        public async Task CreateList(string listName)
        {
            var data = new { listName };
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Lists", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create the list. Status Code: {response.StatusCode}");
            }
        }

        public async Task AddTaskToList(int listId, string taskDescription, CompletionStatus taskStatus, string taskResponsibility)
        {
            var data = new
            {
                description = taskDescription,
                status = taskStatus,
                responsibility = taskResponsibility
            };
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"Lists/{listId}/tasks", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to add task to the list. Status Code: {response.StatusCode}");
            }
        }


        public async Task DeleteList(int listId)
        {
            var response = await client.DeleteAsync($"Lists/{listId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete the list. Status Code: {response.StatusCode}");
            }
        }

        public async Task DeleteTask(int listId, int taskId)
        {
            var response = await client.DeleteAsync($"Lists/{listId}/tasks/{taskId}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to delete the task. Status Code: {response.StatusCode}");
            }
        }
        public async Task<List<ToDoListViewModel>> GetListsAsync()
        {
            var response = await client.GetAsync("Lists");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<List<ToDoListViewModel>>(content, options);
            }
            else
            {
                throw new Exception($"Failed to fetch lists. Status Code: {response.StatusCode}");
            }
        }
        public async Task UpdateTask(int listId, int taskId, string description, CompletionStatus status, string responsibility)
        {
            var updateModel = new { Description = description, Status = status, Responsibility = responsibility };
            var content = new StringContent(JsonSerializer.Serialize(updateModel), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"Lists/{listId}/tasks/{taskId}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update the task. Status Code: {response.StatusCode}");
            }
        }


    }
}