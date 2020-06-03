using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using JNogueira.Discord.Webhook.Client;
using LiteDB;
using Newtonsoft.Json;
using SharpConfig;

namespace ThinkPadsForSaleDiscordNotifier
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create default config file if it doesn't exist
            if (!File.Exists("config.cfg"))
            {
                var c = new Configuration
                {
                    new Section("General")
                    {
                        {"subreddit", "market_subreddit_name"},
                        {"webhook", "https://discordapp.com/api/webhooks/..."}
                    }
                };

                c.SaveToFile("config.cfg");
            }

            // Load configuration
            var config = Configuration.LoadFromFile("config.cfg");
            var configGeneral = config["General"];

            var webhook = configGeneral["webhook"].StringValue;
            var subreddit = configGeneral["subreddit"].StringValue;

            // Load webhook handler
            var discord = new DiscordWebhookClient(webhook);

            // Load market entry cache database
            var cacheDb = new LiteDatabase("cachedposts.db");
            var cachedEntries = cacheDb.GetCollection<MarketEntry>();
            cachedEntries.EnsureIndex(entry => entry.Id, true);

            // Load the most recent subreddit posts
            SubredditNewPosts sub;
            using (var wc = new WebClient())
                sub = JsonConvert.DeserializeObject<SubredditNewPosts>(wc.DownloadString($"https://www.reddit.com/r/{subreddit}/new.json"));

            // Create title-matching regex
            var regex = new Regex("\\[(?<location>.*?)\\]\\s*\\[H\\](?<have>.*)\\[W\\](?<want>.+)", RegexOptions.Compiled);

            // Select "buying" posts which have a correct title format
            var marketEntries =
                sub.Subreddit.SubredditPosts
                    .Where(post => post.Post.LinkFlairCssClass == "selling" && regex.IsMatch(post.Post.Title))
                    .Select(post => (post, matches: regex.Match(post.Post.Title)))
                    .Select(tuple => new MarketEntry(
                        tuple.post.Post.Author,
                        tuple.matches.Groups["have"].Value.Trim(),
                        tuple.matches.Groups["want"].Value.Trim(),
                        tuple.matches.Groups["location"].Value.Trim(),
                        UnixTimeStampToDateTime(tuple.post.Post.Created),
                        tuple.post.Post.Id,
                        tuple.post.Post.Permalink
                    ))
                    .Where(entry => !cachedEntries.Exists(marketEntry => marketEntry.Id == entry.Id));

            foreach (var post in marketEntries)
            {
                var message = new DiscordMessage
                {
                    Embeds = new[]
                    {
                        new DiscordMessageEmbed
                        {
                            Color = 0x5b92fa,
                            Url = $"https://reddit.com{post.Permalink}",
                            Title = $"New listing by {post.Author} in {post.Location}",
                            Fields = new[]
                            {
                                new DiscordMessageEmbedField
                                {
                                    Name = "Have",
                                    Value = post.Have
                                },
                                new DiscordMessageEmbedField
                                {
                                    Name = "Want",
                                    Value = post.Want
                                }
                            }
                        }
                    }
                };

                discord.SendToDiscord(message);

                cachedEntries.Insert(post);
            }
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
