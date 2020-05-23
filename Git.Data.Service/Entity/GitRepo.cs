using Newtonsoft.Json;
using System;

namespace Git.Data.Service.Entity
{
    public class GitRepo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("node_id")]
        public string NodeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("owner")]
        public User User { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }
        [JsonProperty("stargazers_count")]
        public int StarCount { get; set; }

    }

}
