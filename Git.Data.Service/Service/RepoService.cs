using System;
using System.Text;
using Git.Data.Service.Entity;
using Git.Data.Service.Interface;
using Microsoft.Extensions.Configuration;

namespace Git.Data.Service
{
    public class RepoService : IRepoService
    {
        public IConfiguration Configuration { get; set; }
        public IGitHttpService GitHttpService { get; set; }
        
        public RepoService(IConfiguration configuration, 
                                 IGitHttpService httpService)
        {
            Configuration = configuration;
            GitHttpService = httpService;
        }

        public GitRepos GetPopularAssemblyRepos()
        {
            return  GetGitRepos("assembly", "stars", "desc");
        }

        private GitRepos GetGitRepos(String language, string orderBy, string orderDirection)
        {
            StringBuilder query = new StringBuilder();
            query.Append($"q=language:{language}&sort={orderBy}&order={orderDirection}");
            string uri = $"{Configuration.GetSection("GitUrl").Value}search/repositories?{query.ToString()}";
            GitRepos repositories = GitHttpService.Get<GitRepos>(uri);
            return repositories;
        }

    }
}
