using System;
using System.Threading;
using Git.Data.Service.Entity;
using System.Collections.Generic;
using Git.Data.Service.Interface;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Git.Data.Service
{
    public class GitSyncWorker
    {
        private readonly ILogger<GitSyncWorker> Logger;
        private IRepoService RepoService;
        private readonly ConcurrentDictionary<int, GitRepo> Cache;
        private static readonly Object locker = new object();
        private Timer Timer;

        public GitSyncWorker(ILogger<GitSyncWorker> logger, IRepoService repoService, ConcurrentDictionary<int, GitRepo> cache)
        {
            Logger = logger;
            RepoService = repoService;
            Cache = cache;
            int start = 1000, interval = 5000;
            Timer = new Timer(Tick, null, start, interval);
        }
        void Tick(object data)
        {
            try
            {
                GitRepos repos;
                repos = RepoService.GetPopularAssemblyRepos();

                if (repos.Repositories != null)
                    lock (locker)
                        UpdateCache(repos);
            }
            catch (Exception e)
            {
                Logger.LogInformation($"{e.Message} {e.InnerException} {e.StackTrace}");
            }
            finally
            {
                Logger.LogInformation($"Cache updated at: {DateTime.Now}{Environment.NewLine}");
            }
        }

        private void UpdateCache(GitRepos repos)
        {
            CleanCache(repos);
            foreach (var repo in repos.Repositories)
            {
                Cache.AddOrUpdate(repo.Id, repo, (key, oldRepo) => {
                    if (repo.UpdatedAt > oldRepo.UpdatedAt)
                    {
                        return repo;
                    }
                    return oldRepo;
                });
            }
            
        }

        private void CleanCache(GitRepos repos)
        {
            var set = new Dictionary<int, GitRepo>();
            var removeSet = new List<int>();

            foreach (var repo in repos.Repositories)
            {
                set.Add(repo.Id, repo);
            }

            foreach (var repo in Cache)
            {
                if (!set.ContainsKey(repo.Key))
                    removeSet.Add(repo.Key);
            }

            foreach (var id in removeSet)
            {
                Cache.Remove(id, out GitRepo value);
            }
        }
    }
}
