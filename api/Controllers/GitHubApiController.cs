using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GitHubApiController : ControllerBase
    {
        [HttpGet]
         public async Task<IEnumerable<GitHubApiModel>> GetRepositoriesGithub(string profile, int pageNumber, string language, string repositoryName, DateTime createdAt)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/orgs/takenet/repos");
            request.Headers.Add("accept", "application/vnd.github+json");
            request.Headers.Add("User-Agent", "jessikasousa");

            var client = new HttpClient();
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                var list = await JsonSerializer.DeserializeAsync
                    <IEnumerable<GitHubApiModel>>(responseStream);

                if (!String.IsNullOrEmpty(language))
                    list = list.Where(x => x.Language == language);

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