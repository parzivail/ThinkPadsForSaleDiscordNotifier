using Newtonsoft.Json;

namespace JNogueira.Discord.Webhook.Client
{
    /// <summary>
    /// Embed footer object
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class DiscordMessageEmbedFooter
    {
        /// <summary>
        /// Footer text, doesn't support Markdown
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Url of footer icon
        /// </summary>
        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonConstructor]
        public DiscordMessageEmbedFooter()
        {

        }
    }
}