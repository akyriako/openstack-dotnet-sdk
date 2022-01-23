using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenStack.Core;

namespace OpenStack.Iam.Authentication
{
    public class AuthenticationAndTokenManagementClient :
        ServiceClientBase<AuthenticationAndTokenManagementClientOptions>,
        IAuthenticationAndTokenManagementClient
    {
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

        protected override string SuffixSegments => "auth/tokens";

        public async Task<string> GetTokenPasswordAuthenticationUnscopedAuthorizationAsync()
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "/");
            var httpResponseMessage = await this.HttpClient.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();


            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return await httpResponseMessage.Content.ReadAsStringAsync();
            }
            else
            {
                throw new ServiceClientHttpRequestException(
                    httpResponseMessage.ReasonPhrase, httpResponseMessage.StatusCode);
            }
        }
    }

}
