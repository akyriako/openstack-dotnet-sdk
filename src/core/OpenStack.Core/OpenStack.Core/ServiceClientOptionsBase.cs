using System;

namespace OpenStack.Core
{
    public abstract class ServiceClientOptionsBase<E> where E: Enum
    {
        public string ServiceVersion { get; }

        public ServiceClientOptionsBase(E serviceVersion)
        {
            if (serviceVersion == null)
            {
                throw new ArgumentNullException(nameof(serviceVersion));
            }

            ServiceVersion = serviceVersion.ToString();
        }
    }
}
