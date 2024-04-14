using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Program
{
    private static HttpClient client = new HttpClient();
    private static string baseUrl = "http://localhost:5138/api/";

    public static async Task Main(string[] args)
    {
        client.BaseAddress = new Uri(baseUrl);

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1: Create a new list");
            Console.WriteLine("2: Add a task to a list");
            Console.WriteLine("3: Display lists");
            Console.WriteLine("4: Exit");

            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    await CreateList();
                    break;
                case "2":
                    await AddTaskToList();
                    break;
                case "3":
                    await DisplayLists();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }

    private static async Task CreateList()
    {
        Console.WriteLine("Enter the name of the new list:");
        string listName = Console.ReadLine();
        var data = new { listName = listName };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("Lists", content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("List created successfully.");
        }
        else
        {
            Console.WriteLine($"Failed to create the list. Status Code: {response.StatusCode}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }

    private static async Task AddTaskToList()
    {
        Console.WriteLine("Enter the ID of the list to add a task:");
        if (!int.TryParse(Console.ReadLine(), out int listId))
        {
            Console.WriteLine("Invalid input for list ID.");
            return;
        }
        Console.WriteLine("Enter the task description:");
        string taskDescription = Console.ReadLine();
        var data = new { description = taskDescription };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await client.PostAsync($"Lists/{listId}/tasks", content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Task added successfully.");
        }
        else
        {
            Console.WriteLine($"Failed to add task to the list. Status Code: {response.StatusCode}");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }

    private static async Task DisplayLists()
    {
        var response = await client.GetAsync("Lists");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            try
            {
                // Deserialize the response into a list of ToDoList objects
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var lists = JsonSerializer.Deserialize<List<ToDoList>>(content, options);
                Console.WriteLine("Lists and their tasks:");

                foreach (var list in lists)
                {
                    Console.WriteLine($"List ID: {list.Id}, Name: {list.Name}");

                    // Safeguard against null values for Tasks
                    if (list.Tasks != null && list.Tasks.Count > 0)
                    {
                        foreach (var task in list.Tasks)
                        {
                            Console.WriteLine($"\tTask ID: {task.Id}, Description: {task.Description}, Completed: {task.IsCompleted}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\tNo tasks in this list.");
                    }
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Failed to retrieve lists.");
            Console.WriteLine(await response.Content.ReadAsStringAsync());
        }
    }

    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TaskItem> Tasks { get; set; }

        public ToDoList()
        {
            Tasks = new List<TaskItem>(); // Ensures Tasks is never null
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }

}
