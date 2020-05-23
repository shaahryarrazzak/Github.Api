using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Git.Data.Service.Entity
{
    public class GitRepos : GitObject
    {
        public DateTime LastUpdateTime { get; set; }

        [JsonProperty("items")]
        public List<GitRepo> Repositories { get; set; }
    }
}
