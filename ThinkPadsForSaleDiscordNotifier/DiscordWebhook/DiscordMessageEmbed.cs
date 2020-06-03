using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace JNogueira.Discord.Webhook.Client
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DiscordMessageEmbed
    {
        /// <summary>
        /// Color code of the embed.
        /// </summary>
        [JsonProperty("color")]
        public int? Color { get; set; }

        /// <summary>
        ///  Embed author object
        /// </summary>
        [JsonProperty("author")]
        public DiscordMessageEmbedAuthor Author { get; set; }

        /// <summary>
        /// Title of embed
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Url of embed. If title was used, it becomes hyperlink
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Description text
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Array of embed field objects
        /// </summary>
        [JsonProperty("fields")]
        public DiscordMessageEmbedField[] Fields { get; set; }

        /// <summary>
        /// Embed thumbnail object
        /// </summary>
        [JsonProperty("thumbnail")]
        public DiscordMessageEmbedThumbnail Thumbnail { get; set; }

        /// <summary>
        /// Embed image object
        /// </summary>
        [JsonProperty("image")]
        public DiscordMessageEmbedImage Image { get; set; }

        /// <summary>
        /// Embed footer object
        /// </summary>
        [JsonProperty("footer")]
        public DiscordMessageEmbedFooter Footer { get; set; }

        [JsonConstructor]
        public DiscordMessageEmbed()
        {

        }
    }
}
