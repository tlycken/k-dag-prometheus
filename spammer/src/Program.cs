using System;

using System.Threading.Tasks;
using System.Net.Http;

namespace Igniter
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            var random = new Random();

            var http = new HttpClient();

            while (true) {
                await Task.Delay(random.Next(50, 3000));
                var response = await http.GetAsync("http://fire/");
                Console.WriteLine($"Got response: {(int)response.StatusCode} {response.ReasonPhrase}");
            }
        }
    }
}
