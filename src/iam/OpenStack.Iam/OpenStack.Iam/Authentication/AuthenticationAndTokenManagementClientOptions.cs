using System;
using OpenStack.Core;

namespace OpenStack.Iam.Authentication
{
    public class AuthenticationAndTokenManagementClientOptions : ServiceClientOptionsBase
    {
        public string UserId { get; set; }
        public string DomainId { get; set; }
        public string Password { get; set; }
        public string TenantId { get; set; }
    }
}
