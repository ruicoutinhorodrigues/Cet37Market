using Newtonsoft.Json;

namespace Cet37Market.Common.Models
{
    public class TokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("expiration")]
        public string Expiration { get; set; }
    }
}
