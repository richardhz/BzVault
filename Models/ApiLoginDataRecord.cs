using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BzVault.Models
{
    public record ApiLoginDataRecord
    {
        [JsonPropertyName("id")]
        public Guid Id { get; init; }
        [JsonPropertyName("name")]
        public string Name { get; init; }
        [JsonPropertyName("login")]
        public string Login { get; init; }
        [JsonPropertyName("password")]
        public string Password { get; init; }
        [JsonPropertyName("url")]
        public string Url { get; init; }
        [JsonPropertyName("description")]
        public string Description { get; init; }
    }
}
