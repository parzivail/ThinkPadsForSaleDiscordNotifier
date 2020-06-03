using Newtonsoft.Json;

namespace JNogueira.Discord.Webhook.Client
{
    public class DiscordTooManyRequestsResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("retry_after")]
        public int RetryAfter { get; set; }

        [JsonProperty("global")]
        public bool Global { get; set; }
    }
}