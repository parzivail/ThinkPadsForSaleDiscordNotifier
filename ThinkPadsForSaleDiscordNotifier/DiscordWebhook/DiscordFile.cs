namespace JNogueira.Discord.Webhook.Client
{
    public class DiscordFile
    {
        /// <summary>
        /// File name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// File content
        /// </summary>
        public byte[] Content { get; }

        public DiscordFile(string name, byte[] content)
        {
            Name = name;
            Content = content;
        }
    }
}
