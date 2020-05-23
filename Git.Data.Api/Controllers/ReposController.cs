using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Git.Data.Service.Entity;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Git.Data.Api.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]")]
    public class ReposController : ControllerBase
    {
        private readonly ILogger<ReposController> Logger;
        public ConcurrentDictionary<int, GitRepo> Cache { get; set; }

        public ReposController(ILogger<ReposController> logger, 
                                    ConcurrentDictionary<int, GitRepo> cache)
        {
            Logger = logger;
            Cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult<List<GitRepo>>> GetRepos(string description)
        {
            List<GitRepo> repos = await Task.FromResult(
            Cache.Values.ToList().Where((R) => R.Description.Contains(description ?? string.Empty))
                        .OrderByDescending((R) => R.StarCount).ToList());

            if (repos.Count == 0)
                return NotFound();

            return repos;
        }
    }
}
