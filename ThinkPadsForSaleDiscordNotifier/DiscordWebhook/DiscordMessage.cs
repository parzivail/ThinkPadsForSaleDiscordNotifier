using Newtonsoft.Json;

namespace JNogueira.Discord.Webhook.Client
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DiscordMessage
    {
        /// <summary>
        /// Overrides the current username of the webhook
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Overrides the default avatar of the webhook
        /// </summary>
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Simple message, the message contains (up to 2000 characters)
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// If true, the message will be pronounced in chat like tts message
        /// </summary>
        [JsonProperty("tts")]
        public bool Tts { get; set; }

        /// <summary>
        /// An array of embed objects. That means you put use more than one in the same body
        /// </summary>
        [JsonProperty("embeds")]
        public DiscordMessageEmbed[] Embeds { get; set; }

        [JsonConstructor]
        public DiscordMessage()
        {

        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
        }
    }
}