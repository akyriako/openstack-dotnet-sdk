using System;

namespace OpenStack.Core
{
    public class AccessToken
    {
        public string Token { get; set; }
        public long ExpiresAt { get; set; }
    }
}
