using System;
using System.Net.Http;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        using (var client = new HttpClient())
        {
            string baseUrl = "http://localhost:5138/api/";
            try
            {
                HttpResponseMessage response = await client.GetAsync(baseUrl + "tasks"); // Make sure "tasks" is the correct endpoint
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
}
