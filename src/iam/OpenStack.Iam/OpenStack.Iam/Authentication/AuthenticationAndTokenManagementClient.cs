using System;
using System.Threading.Tasks;
using OpenStack.Core;

namespace OpenStack.Iam.Authentication
{
    public class AuthenticationAndTokenManagementClient : ServiceClientBase<AuthenticationAndTokenManagementClientOptions, AuthenticationAndTokenManagementVersion>
    {
        protected override string ExtraPathSegments => "auth/tokens";
        //protected override string ServiceVersion => "v3";

        public AuthenticationAndTokenManagementClient()
        {
        }

        public AuthenticationAndTokenManagementClient(string serviceBaseUrl) : base(serviceBaseUrl)
        {
        }

        public AuthenticationAndTokenManagementClient(string serviceBaseUrl, AuthenticationAndTokenManagementClientOptions options = null) : base(serviceBaseUrl, options)
        {
        }
    }

}
