using System;
using System.Net;
using System.Net.Http;

namespace OpenStack.Core
{
    public class ServiceClientHttpRequestException: HttpRequestException
    {
        public HttpStatusCode StatusCode { get; }

        public ServiceClientHttpRequestException(string reasonPhrase, HttpStatusCode statusCode) : this(reasonPhrase, statusCode, null)
        {
        }

        public ServiceClientHttpRequestException(string reasonPhrase, HttpStatusCode statusCode, Exception inner) : base(reasonPhrase, inner)
        {
            StatusCode = statusCode;
        }
    }
}
