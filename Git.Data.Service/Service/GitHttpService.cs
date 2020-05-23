using System;
using Newtonsoft.Json;
using System.Net.Http;
using Git.Data.Service.Entity;
using Git.Data.Service.Interface;
using Microsoft.Extensions.Configuration;

namespace Git.Data.Service.Service
{
    public class GitHttpService : IGitHttpService
    {
        public IConfiguration Configuration { get; set; }
        public GitHttpService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public Response Get<Response>(string url) where Response : GitObject
        {
            Response response = default;
            HttpClientHandler clientHandler = new HttpClientHandler();
            
            using (HttpClient client = new HttpClient(handler: clientHandler, disposeHandler: true))
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, new Uri($"{url}"));
                request.Headers.Add("User-Agent", "agent");
                request.Headers.Add("authorization", $"{Configuration.GetSection("Token").Value}");
                HttpResponseMessage responseMessage = client.SendAsync(request).Result;
                string jsonResponse = responseMessage.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<Response>(jsonResponse);
            }
            return response;
        }

    }
}
