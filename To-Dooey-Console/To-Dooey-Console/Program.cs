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
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1: Create a new list");
            Console.WriteLine("2: Add a task to a list");
            Console.WriteLine("3: Display lists");
            Console.WriteLine("4: Exit");

            switch (Console.ReadLine())
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
        // Make sure the JSON keys match what the API expects
        var data = new { listName = listName };
        var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("Lists", content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("List created successfully.");
        }
        else
        {
            Console.WriteLine("Failed to create the list. Status Code: " + response.StatusCode);
            // Read and display the response body to help diagnose the issue:
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent);
        }
    }



    private static async Task AddTaskToList()
    {
        Console.WriteLine("Enter the ID of the list to add a task:");
        int listId = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the task description:");
        string taskDescription = Console.ReadLine();
        var response = await client.PostAsync($"Lists/{listId}/tasks", new StringContent(JsonSerializer.Serialize(new { description = taskDescription }), Encoding.UTF8, "application/json"));
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Task added successfully.");
        }
        else
        {
            Console.WriteLine("Failed to add task to the list.");
        }
    }

    private static async Task DisplayLists()
    {
        var response = await client.GetAsync("Lists");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var lists = JsonSerializer.Deserialize<List<string>>(content);
            Console.WriteLine("Lists:");
            foreach (var list in lists)
            {
                Console.WriteLine(list);
            }
        }
        else
        {
            Console.WriteLine("Failed to retrieve lists.");
        }
    }
}
