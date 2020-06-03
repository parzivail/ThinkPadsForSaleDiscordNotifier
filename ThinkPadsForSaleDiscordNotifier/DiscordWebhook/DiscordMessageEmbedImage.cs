using Newtonsoft.Json;

namespace JNogueira.Discord.Webhook.Client
{
    /// <summary>
    /// Embed image object
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class DiscordMessageEmbedImage
    {
        /// <summary>
        /// url of image
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonConstructor]
        private DiscordMessageEmbedImage()
        {

        }

        public DiscordMessageEmbedImage(string url)
        {
            Url = url;
        }
    }
}