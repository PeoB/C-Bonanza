using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskStuff
{
    class Program
    {
        static void Main()
        {
            DoIt().Wait();
        }

        static async Task DoIt()
        {
            var result = await await Task.WhenAny(GetGoogle(), GetBing());
            Console.WriteLine(result);
            Console.WriteLine(result.Contains("google") ? "google" : "bing");
        }

        static async Task<string> GetGoogle()
        {
            return await GetContents("http://www.google.se/");
        }

        static async Task<string> GetBing()
        {
            return await GetContents("http://www.bing.com/");
        }

        static async Task<string> GetContents(string url)
        {
            var client = new HttpClient();

            var result = await client.GetAsync(url);
            var str = await result.Content.ReadAsStringAsync();
            return str;

        }
    }
}
