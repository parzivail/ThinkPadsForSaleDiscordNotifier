using Newtonsoft.Json;

namespace JNogueira.Discord.Webhook.Client
{
    /// <summary>
    /// Embed thumbnail object
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class DiscordMessageEmbedThumbnail
    {
        /// <summary>
        /// url of thumbnail
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonConstructor]
        private DiscordMessageEmbedThumbnail()
        {

        }

        public DiscordMessageEmbedThumbnail(string url)
        {
            Url = url;
        }
    }
}