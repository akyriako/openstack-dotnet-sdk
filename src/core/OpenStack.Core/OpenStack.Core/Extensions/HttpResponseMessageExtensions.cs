using System;
using System.Net.Http;
using System.Text.Json;

namespace OpenStack.Core.Extensions
{
    public static class HttpResponseMessageExtensions
    {
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
