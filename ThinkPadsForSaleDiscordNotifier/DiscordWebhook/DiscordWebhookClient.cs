using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DiscordNotifier;

namespace JNogueira.Discord.Webhook.Client
{
    public class DiscordWebhookClient
    {
        private static readonly HttpClient WebClient = new HttpClient
        {
            Timeout = new TimeSpan(0, 0, 30)
        };

        private readonly string _urlWebhook;

        /// <summary>
        /// A client class to send messages using a Discord webhook.
        /// </summary>
        /// <param name="urlWebhook">The Discord webhook url</param>
        public DiscordWebhookClient(string urlWebhook)
        {
            if (string.IsNullOrEmpty(urlWebhook))
                throw new ArgumentNullException(nameof(urlWebhook), "The Discord webhook url cannot be null or empty.");

            _urlWebhook = urlWebhook;
        }

        private void Publish(HttpContent content)
        {
            HttpResponseMessage response;
            do
            {
                response = AsyncHelper.RunSync(() => WebClient.PostAsync(_urlWebhook, content));

                var jsonBody =
                    JsonConvert.DeserializeObject<DiscordTooManyRequestsResponse>(response.Content
                        .ReadAsStringAsync().Result);

                if (jsonBody != null)
                    System.Threading.Thread.Sleep(jsonBody.RetryAfter + 1);
            } while ((int) response.StatusCode == 429);

            if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.NoContent) return;

            throw new DiscordWebhookClientException(
                $"An error occurred in sending the message: {AsyncHelper.RunSync(() => response.Content.ReadAsStringAsync())} - HTTP status code {(int) response.StatusCode} - {response.StatusCode}");
        }

        /// <summary>
        /// Send a message with files to Discord using a webhook
        /// </summary>
        /// <param name="message">Message to be sent</param>
        /// <param name="files">Files to be sent</param>
        public void SendToDiscord(DiscordMessage message, DiscordFile[] files = null)
        {
            if (files != null && files.Length > 10)
                throw new DiscordWebhookClientException($"Files collection size limit is 10 objects. (actual size is {files.Length})");

            if (message == null && files == null)
                throw new DiscordWebhookClientException("The message parameter cannot be null.");

            using (var formContent = new MultipartFormDataContent())
            {
                if (message != null)
                    formContent.Add(new StringContent(message.ToJson(), Encoding.UTF8), "payload_json");

                if (files != null)
                    foreach (var file in files)
                        formContent.Add(new ByteArrayContent(file.Content), Path.GetFileName(file.Name), file.Name);

                Publish(formContent);
            }
        }
    }
}
