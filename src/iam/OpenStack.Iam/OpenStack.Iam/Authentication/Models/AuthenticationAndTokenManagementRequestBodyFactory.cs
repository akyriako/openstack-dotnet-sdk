using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace OpenStack.Iam.Authentication.Models
{
    public static class AuthenticationAndTokenManagementRequestBodyFactory
    {
        public static string BuildPasswordAuthenticationUnscopedAuthorizationRequestBody(string name, string domain, string password)
        {
            string responseBody;

            var assembly = typeof(OpenStack.Iam.Authentication.Models.AuthenticationAndTokenManagementRequestBodyFactory).GetTypeInfo().Assembly;
            var embeddedFileProvider = new EmbeddedFileProvider(assembly);

            //using (var stream = embeddedFileProvider.GetFileInfo("OpenStack.Iam.Authentication.Models.PasswordAuthenticationUnscopedAuthorization.json").CreateReadStream())
            //{
            //    if (stream == null)
            //    {
            //        throw new InvalidOperationException("Could not load manifest resource stream.");
            //    }
            //    using (var reader = new StreamReader(stream))
            //    {
            //        responseBody = reader.ReadToEnd();
            //    }
            //}

            using (var stream = assembly.GetManifestResourceStream("OpenStack.Iam.Authentication.Models.PasswordAuthenticationUnscopedAuthorization.json"))
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

            return String.Format(responseBody, name, domain, password);
        }
    }
}
    