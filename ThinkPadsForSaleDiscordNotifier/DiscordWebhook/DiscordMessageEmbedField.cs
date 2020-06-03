using Newtonsoft.Json;

namespace JNogueira.Discord.Webhook.Client
{
    /// <summary>
    /// Embed field objects
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class DiscordMessageEmbedField
    {
        /// <summary>
        /// The name of the field
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// The value of the field
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// If true, fields will be displayed in the same line, but there can only be 3 max in the same line or 2 max if you used thumbnail
        /// </summary>
        [JsonProperty("inline")]
        public bool Inline { get; set; }

        [JsonConstructor]
        public DiscordMessageEmbedField()
        {

        }
    }
}