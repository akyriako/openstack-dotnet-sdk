using System;
using System.Linq;

namespace OpenStack.Core
{
    public abstract class ServiceClientBase<T,O> where T: ServiceClientOptionsBase<O> where O:Enum
    {
        public Uri BaseUri { get; }
        public string ServiceVersion { get; }
        public T Options { get; }

        protected virtual string ExtraPathSegments { get; set; }

        public ServiceClientBase(string serviceBaseUrl) : this(serviceBaseUrl, default)
        {
            
        }

        public ServiceClientBase(string serviceBaseUrl, T options = default)
        {
            if (String.IsNullOrEmpty(serviceBaseUrl))
            {
                throw new ArgumentNullException(nameof(serviceBaseUrl));
            }

            Uri serviceBaseUri;

            if (!Uri.TryCreate(serviceBaseUrl, UriKind.Absolute, out serviceBaseUri))
            {
                throw new UriFormatException(nameof(serviceBaseUrl));
            }

            BaseUri = serviceBaseUri;
            Options = options;

            if (options == default)
            {
                ServiceVersion = Enum.GetValues(typeof(O)).GetValue(0).ToString();
            }
        }

        protected ServiceClientBase()
        {
        }

        public string GetEndpointUri()
        {
            return new Uri(BaseUri, $"{(Options == default ? this.ServiceVersion : Options.ServiceVersion)}/{ExtraPathSegments}").ToString();
        }
    }
}
