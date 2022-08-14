using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GithubController : ControllerBase
    {

        [HttpGet]
        public async Task<IEnumerable<GitHubApiModel>> GetRepositoriesGithub(string profile, int pageNumber, string repositoryName, DateTime createdAt)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/users/" + profile + "/repos?" + "page=" + pageNumber);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "jessikasousa");

            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var list = await JsonSerializer.DeserializeAsync
                    <IEnumerable<GitHubApiModel>>(responseStream);

                if (createdAt > DateTime.MinValue)
                    list = list.Where(x => x.CreatedAt.Substring(0, 10) == createdAt.ToString("yyy-MM-dd").Substring(0, 10));

                if (!String.IsNullOrEmpty(repositoryName))
                    list = list.Where(x => x.FullName == repositoryName);

                return list;
            }
            return null;
        }
    }
}
