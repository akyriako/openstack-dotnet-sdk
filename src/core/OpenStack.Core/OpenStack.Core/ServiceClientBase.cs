using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OpenStack.Core.Extensions

namespace OpenStack.Core
{
    public abstract class ServiceClientBase<T> : IDisposable
        where T: ServiceClientOptionsBase
    {
        private bool m_Disposed;

        protected virtual string SuffixSegments { get; set; }

        protected readonly HttpClient HttpClient;
        protected readonly T Options;
        protected readonly OpenStackCommonOptions CommonOptions;

        public ServiceClientBase(HttpClient httpClient, string baseUri, string version = "")
        {
            if (String.IsNullOrEmpty(baseUri))
            {
                throw new ArgumentNullException(nameof(baseUri));
            }

            Options = default(T);
            Options.BaseUri = baseUri;
            Options.Version = version;

            HttpClient = httpClient;
        }

        public ServiceClientBase(HttpClient httpClient, T options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (String.IsNullOrEmpty(options.BaseUri))
            {
                throw new ArgumentNullException(nameof(options.BaseUri));
            }

            HttpClient = httpClient;
            Options = options;
        }

        public ServiceClientBase(HttpClient httpClient, IOptions<T> options) : this(httpClient, options?.Value)
        {
            
        }

        protected ServiceClientBase()
        {
        }

        public string GetUri()
        {
            return new Uri(new Uri(Options.BaseUri), $"{Options.Version}/{SuffixSegments}").ToString();
        }

        public virtual async Task<JsonDocument> SendAsync(HttpRequestMessage httpRequestMessage)
        {
            JsonDocument jsonDocument = null;

            var httpResponseMessage = await this.HttpClient.SendAsync(httpRequestMessage);

            if (CommonOptions.ClientThrowsExceptionInsteadOfJsonResponse)
            {
                httpResponseMessage.EnsureSuccessStatusCode();

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    httpResponseMessage.TryGetContectAsJsonDocument(out jsonDocument);
                }
            }
            else
            {
                httpResponseMessage.TryGetContectAsJsonDocument(out jsonDocument);
            }

            return await Task.FromResult<JsonDocument>(jsonDocument);
        }

        public void Dispose()
        {
            if (m_Disposed)
            {
                return;
            }

            m_Disposed = true;
        }
    }
}
