using System;

namespace OpenStack.Core
{
    public abstract class ServiceClientOptionsBase
    {
        public string BaseUri { get; set; }
        public string Version { get; set; }
    }
}
