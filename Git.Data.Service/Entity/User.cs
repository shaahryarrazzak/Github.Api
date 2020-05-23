using Newtonsoft.Json;

namespace Git.Data.Service.Entity
{
    public class User
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }
    }
}
