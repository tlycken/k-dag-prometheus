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

            while (true)
            {
                await Task.Delay(random.Next(50, 500));
                await http.GetAsync("http://fire/");

                var decider = random.NextDouble();
                if (decider > .9)
                {
                    Console.WriteLine($"Decider is {decider}, quitting.");
                    break;
                }
                else if (decider > .5)
                {
                    Console.WriteLine($"Decider is {decider}, POST-ing.");
                    await http.PostAsync("http://fire/foo", null);
                }
                else
                {
                    Console.WriteLine($"Decider is {decider}, GET-ing.");
                    await http.GetAsync("http://fire/foo");
                }
            }
        }
    }
}
