using Git.Data.Service.Entity;

namespace Git.Data.Service.Interface
{
    public interface IGitHttpService
    {
        Response Get<Response>(string url) where Response : GitObject;
    }

}
