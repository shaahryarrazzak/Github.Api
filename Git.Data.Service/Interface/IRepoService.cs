using Git.Data.Service.Entity;

namespace Git.Data.Service.Interface
{
    public interface IRepoService
    {
        GitRepos GetPopularAssemblyRepos();
    }
}
