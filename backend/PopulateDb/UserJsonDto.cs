#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

using Newtonsoft.Json;

namespace PopulateDb
{
    internal class UserJsonDto
    {
        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("email")]
        public string Email { get; internal set; }

        [JsonProperty("phone")]
        public string Phone { get; internal set; }

        [JsonProperty("password")]
        public string Password { get; internal set; }
    }
}