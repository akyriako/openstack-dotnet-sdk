using System;

namespace OpenStack.Iam.Authentication
{
    public class AccessToken
    {
        public string Token { get; set; }
        public long ExpiresAt { get; set; }
    }
}
