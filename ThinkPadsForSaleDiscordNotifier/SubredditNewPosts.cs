using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ThinkPadsForSaleDiscordNotifier
{
    public class SubredditNewPosts
    {
        [JsonProperty("data")]
        public SubredditData Subreddit { get; set; }
    }

    public class SubredditData
    {
        [JsonProperty("children")]
        public SubredditPost[] SubredditPosts { get; set; }
    }

    public class SubredditPost
    {
        [JsonProperty("data")]
        public PostData Post { get; set; }
    }

    public class PostData
    {
        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link_flair_css_class")]
        public string LinkFlairCssClass { get; set; }

        [JsonProperty("created")]
        public float Created { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }
    }
}