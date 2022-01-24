using System;
using System.Linq;
using System.Net.Http;
using OpenStack.Iam.Authentication;

namespace OpenStack.Iam.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static AccessToken ExtractOpenStackAccessToken(this HttpResponseMessage httpResponseMessage)
        {
            string token = String.Empty;

            if (httpResponseMessage.Headers.Contains("x-subject-token"))
            {
                token = httpResponseMessage.Headers.GetValues("x-subject-token").FirstOrDefault();
            }

            AccessToken accessToken = new AccessToken()
            {
                Token = token,
                ExpiresAt = DateTime.Now.Ticks
            };

            return accessToken;
        }
    }
}
