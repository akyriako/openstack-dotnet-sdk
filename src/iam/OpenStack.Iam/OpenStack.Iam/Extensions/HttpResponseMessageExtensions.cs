using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using OpenStack.Core;

namespace OpenStack.Iam.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static AccessToken ExtractOpenStackAccessToken(this HttpResponseMessage httpResponseMessage)
        {
            string token = String.Empty;
            DateTime expiresAt;
            var expiresAtPropertyFound = false;

            var contentAsString = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            using (var contentAsJsonDocument = System.Text.Json.JsonSerializer.Deserialize<JsonDocument>(contentAsString))
            {
                expiresAtPropertyFound = DateTime.TryParse(contentAsJsonDocument.RootElement
                                                .GetProperty("token")
                                                .GetProperty("expires_at").ToString(), out expiresAt);

                if (httpResponseMessage.Headers.Contains("x-subject-token"))
                {
                    token = httpResponseMessage.Headers.GetValues("x-subject-token").FirstOrDefault();
                }
            }

            AccessToken accessToken = new AccessToken()
            {
                Token = token,
                ExpiresAt = expiresAtPropertyFound ? expiresAt.Ticks : DateTime.Now.AddMinutes(-1).Ticks
            };

            return accessToken;
        }

        public static bool TryGetContectAsJsonDocument(this HttpResponseMessage httpResponseMessage, out JsonDocument jsonDocument)
        {
            try
            {
                var contentAsString = httpResponseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                jsonDocument = System.Text.Json.JsonSerializer.Deserialize<JsonDocument>(contentAsString);

                return true;
            }
            catch (Exception ex)
            {
            }

            jsonDocument = null;
            return false;
        }
    }
}
