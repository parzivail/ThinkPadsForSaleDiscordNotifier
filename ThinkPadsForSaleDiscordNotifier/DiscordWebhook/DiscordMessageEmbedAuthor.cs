using Newtonsoft.Json;

namespace JNogueira.Discord.Webhook.Client
{
    /// <summary>
    /// Embed author object
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class DiscordMessageEmbedAuthor
    {
        /// <summary>
        /// Name of author
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Url of author. If name was used, it becomes a hyperlink
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Url of author icon
        /// </summary>
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonConstructor]
        public DiscordMessageEmbedAuthor()
        {

        }

        public DiscordMessageEmbedAuthor(string name, string url = null, string iconUrl = null)
        {
            Name = name;
            Url = url;
            IconUrl = iconUrl;
        }
    }
}