using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace _4chanDL
{
    public static class Config
    {
        public static string Url { get; set; } = "https://boards.4chan.org/biz/catalog";
        public static string UserAgent { get; set; } = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/98.0.4758.80 Safari/537.36";
    }

    internal class Program
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<HtmlDocument> FetchThreads(string url)
        {
            var html = await GetHTTP(url);
            return ParseHTML(html);
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Fetching threads...");
            var threads = await FetchThreads(Config.Url);
            var thread = threads.DocumentNode.SelectSingleNode("//div[@id='threads']");

        }

        
        public static HtmlDocument ParseHTML(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc;
        }

        public static async Task<string> GetHTTP(string url)
        {
            try
            {
                if (!client.DefaultRequestHeaders.Contains("User-Agent"))
                {
                    client.DefaultRequestHeaders.Add("User-Agent", Config.UserAgent);
                }

                using HttpResponseMessage response = await client.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return "";
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred: {e.Message}");
                return "";
            }
        }
    }
}
