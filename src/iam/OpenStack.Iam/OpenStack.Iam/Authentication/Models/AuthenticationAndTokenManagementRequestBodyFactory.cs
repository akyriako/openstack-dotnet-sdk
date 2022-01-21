using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Microsoft.Extensions.FileProviders;
using OpenStack.Core.Extensions;

namespace OpenStack.Iam.Authentication.Models
{
    public static class AuthenticationAndTokenManagementRequestBodyFactory
    {
        public static string BuildPasswordAuthenticationUnscopedAuthorizationRequestBody(string name, string domain, string password)
        {
            string responseBody;

            var assembly = typeof(OpenStack.Iam.Authentication.Models.AuthenticationAndTokenManagementRequestBodyFactory).GetTypeInfo().Assembly;
            var embeddedFileProvider = new EmbeddedFileProvider(assembly, "OpenStack.Iam.Authentication.Models");

            using (var stream = embeddedFileProvider.GetFileInfo("PasswordAuthenticationUnscopedAuthorization.json").CreateReadStream())
            {
                if (stream == null)
                {
                    throw new InvalidOperationException("Could not load manifest resource stream.");
                }
                using (var reader = new StreamReader(stream))
                {
                    responseBody = reader.ReadToEnd();
                }
            }

            responseBody = responseBody.ToFormattableJsonString();

            return String.Format(responseBody, name, domain, password);
        }
    }
}
    