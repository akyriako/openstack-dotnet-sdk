using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenStack.Core;
using OpenStack.Iam.Extensions;
using OpenStack.Iam.Authentication.Models;
using System.Text.Json;

namespace OpenStack.Iam.Authentication
{
    public class AuthenticationAndTokenManagementClient :
        ServiceClientBase<AuthenticationAndTokenManagementClientOptions>,
        IAuthenticationAndTokenManagementClient
    {
        protected override string SuffixSegments => "auth/tokens";

        public AuthenticationAndTokenManagementClient()
        {
        }

        public AuthenticationAndTokenManagementClient(HttpClient httpClient, AuthenticationAndTokenManagementClientOptions options) : base(httpClient, options)
        {
        }

        public AuthenticationAndTokenManagementClient(HttpClient httpClient, IOptions<AuthenticationAndTokenManagementClientOptions> options) : base(httpClient, options)
        {
        }

        public AuthenticationAndTokenManagementClient(HttpClient httpClient, string baseUri, string version = "") : base(httpClient, baseUri, version)
        {
        }

        public async Task<AccessToken> GetTokenPasswordAuthenticationUnscopedAuthorizationAsync(
            string username,
            string domain,
            string password)
        {
            AccessToken accessToken = null;

            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (String.IsNullOrEmpty(domain))
            {
                throw new ArgumentNullException(nameof(domain));
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var bodyAsString =
                AuthenticationAndTokenManagementRequestBodyFactory.
                BuildPasswordAuthenticationUnscopedAuthorizationRequestBody(
                    username,
                    domain,
                    password);

            using (HttpRequestMessage httpRequestMessage =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    SuffixSegments))
            {
                using (var bodyAsJson = new StringContent(bodyAsString, Encoding.UTF8, "application/json"))
                {
                    httpRequestMessage.Content = bodyAsJson;
                    var httpResponseMessage = await this.HttpClient.SendAsync(httpRequestMessage);

                    httpResponseMessage.EnsureSuccessStatusCode();

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        if (httpResponseMessage.Headers.Contains("x-subject-token"))
                        {
                            accessToken = httpResponseMessage.ExtractOpenStackAccessToken();
                        }
                    }
                }  
            }

            return await Task.FromResult<AccessToken>(accessToken);
        }

        public async Task<AccessToken> GetTokenPasswordAuthenticationScopedAuthorizationAsync(
            string userId,
            string tenantId,
            string password)
        {
            AccessToken accessToken = null;

            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (String.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentNullException(nameof(tenantId));
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var bodyAsString =
                AuthenticationAndTokenManagementRequestBodyFactory.
                BuildPasswordAuthenticationScopedAuthorizationRequestBody(
                    userId,
                    tenantId,
                    password);

            using (HttpRequestMessage httpRequestMessage =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    SuffixSegments))
            {
                using (var bodyAsJson = new StringContent(bodyAsString, Encoding.UTF8, "application/json"))
                {
                    httpRequestMessage.Content = bodyAsJson;
                    var httpResponseMessage = await this.HttpClient.SendAsync(httpRequestMessage);

                    httpResponseMessage.EnsureSuccessStatusCode();

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        accessToken = httpResponseMessage.ExtractOpenStackAccessToken();
                    }
                }
            }

            return await Task.FromResult<AccessToken>(accessToken);
        }

        public async Task<AccessToken> GetTokenPasswordAuthenticationExplicitUnscopedAuthorizationAsync(
            string userId,
            string password)
        {
            AccessToken accessToken = null;

            if (String.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var bodyAsString =
                AuthenticationAndTokenManagementRequestBodyFactory.
                BuildPasswordAuthenticationExplicitUnscopedAuthorizationRequestBody(
                    userId,
                    password);

            using (HttpRequestMessage httpRequestMessage =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    SuffixSegments))
            {
                using (var bodyAsJson = new StringContent(bodyAsString, Encoding.UTF8, "application/json"))
                {
                    httpRequestMessage.Content = bodyAsJson;
                    var httpResponseMessage = await this.HttpClient.SendAsync(httpRequestMessage);

                    httpResponseMessage.EnsureSuccessStatusCode();

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        accessToken = httpResponseMessage.ExtractOpenStackAccessToken();
                    }
                }
            }

            return await Task.FromResult<AccessToken>(accessToken);
        }

        public async Task<AccessToken> GetTokenTokenAuthenticationUnscopedAuthorizationAsync(
            string token)
        {
            AccessToken accessToken = null;

            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            var bodyAsString =
                AuthenticationAndTokenManagementRequestBodyFactory.
                BuildTokenAuthenticationUnscopedAuthorizationRequestBody(
                    token);

            using (HttpRequestMessage httpRequestMessage =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    SuffixSegments))
            {
                using (var bodyAsJson = new StringContent(bodyAsString, Encoding.UTF8, "application/json"))
                {
                    httpRequestMessage.Content = bodyAsJson;
                    var httpResponseMessage = await this.HttpClient.SendAsync(httpRequestMessage);

                    httpResponseMessage.EnsureSuccessStatusCode();

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        accessToken = httpResponseMessage.ExtractOpenStackAccessToken();
                    }
                }
            }

            return await Task.FromResult<AccessToken>(accessToken);
        }

        public async Task<AccessToken> GetTokenTokenAuthenticationScopedAuthorizationAsync(
            string token,
            string tenantId)
        {
            AccessToken accessToken = null;

            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            if (String.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentNullException(nameof(tenantId));
            }

            var bodyAsString =
                AuthenticationAndTokenManagementRequestBodyFactory.
                BuildTokenAuthenticationScopedAuthorizationRequestBody(
                    token,
                    tenantId);

            using (HttpRequestMessage httpRequestMessage =
                new HttpRequestMessage(
                    HttpMethod.Post,
                    SuffixSegments))
            {
                using (var bodyAsJson = new StringContent(bodyAsString, Encoding.UTF8, "application/json"))
                {
                    httpRequestMessage.Content = bodyAsJson;
                    var httpResponseMessage = await this.HttpClient.SendAsync(httpRequestMessage);

                    httpResponseMessage.EnsureSuccessStatusCode();

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        accessToken = httpResponseMessage.ExtractOpenStackAccessToken();
                    }
                }
            }

            return await Task.FromResult<AccessToken>(accessToken);
        }

        public async Task<JsonDocument> ValidateAndShowInformationForTokenAsync(
            string token)
        {
            JsonDocument jsonDocument = null;

            if (String.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            using (HttpRequestMessage httpRequestMessage =
                new HttpRequestMessage(
                    HttpMethod.Get,
                    SuffixSegments))
            {
                httpRequestMessage.Headers.Add("X-Auth-Token", token);
                httpRequestMessage.Headers.Add("X-Subject-Token", token);

                var httpResponseMessage = await this.HttpClient.SendAsync(httpRequestMessage);

                httpResponseMessage.EnsureSuccessStatusCode();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    jsonDocument = httpResponseMessage.GetContectAsJsonDocument();
                }
            }

            return await Task.FromResult<JsonDocument>(jsonDocument);
        }
    }

}
