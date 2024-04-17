using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Add methods to communicate with the API
    // For example: CreateListAsync, AddTaskToListAsync, etc.
}
