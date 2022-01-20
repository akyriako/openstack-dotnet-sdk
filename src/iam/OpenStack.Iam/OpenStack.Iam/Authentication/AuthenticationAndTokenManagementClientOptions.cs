using System;
using OpenStack.Core;

namespace OpenStack.Iam.Authentication
{
    public class AuthenticationAndTokenManagementClientOptions : ServiceClientOptionsBase<AuthenticationAndTokenManagementVersion>
    {
        public AuthenticationAndTokenManagementClientOptions(AuthenticationAndTokenManagementVersion serviceVersion) : base(serviceVersion)
        {
        }
    }
}
