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
        private static readonly string s_BaseNamespace = "OpenStack.Iam.Authentication.Models";

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

        public static string BuildPasswordAuthenticationScopedAuthorizationRequestBody(string userId, string tenantId, string password)
        {
            string responseBody;

            var assembly = typeof(OpenStack.Iam.Authentication.Models.AuthenticationAndTokenManagementRequestBodyFactory).GetTypeInfo().Assembly;
            var embeddedFileProvider = new EmbeddedFileProvider(assembly, "OpenStack.Iam.Authentication.Models");

            using (var stream = embeddedFileProvider.GetFileInfo("PasswordAuthenticationScopedAuthorization.json").CreateReadStream())
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

            return String.Format(responseBody, userId, password, tenantId);
        }

        public static string BuildPasswordAuthenticationExplicitUnscopedAuthorizationRequestBody(string userId, string password)
        {
            string responseBody;

            var assembly = typeof(OpenStack.Iam.Authentication.Models.AuthenticationAndTokenManagementRequestBodyFactory).GetTypeInfo().Assembly;
            var embeddedFileProvider = new EmbeddedFileProvider(assembly, s_BaseNamespace);

            using (var stream = embeddedFileProvider.GetFileInfo("PasswordAuthenticationExplicitUnscopedAuthorization.json").CreateReadStream())
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

            return String.Format(responseBody, userId, password);
        }

        public static string BuildTokenAuthenticationUnscopedAuthorizationRequestBody(string token)
        {
            string responseBody;

            var assembly = typeof(OpenStack.Iam.Authentication.Models.AuthenticationAndTokenManagementRequestBodyFactory).GetTypeInfo().Assembly;
            var embeddedFileProvider = new EmbeddedFileProvider(assembly, s_BaseNamespace);

            using (var stream = embeddedFileProvider.GetFileInfo("TokenAuthenticationUnscopedAuthorization.json").CreateReadStream())
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

            return String.Format(responseBody, token);
        }

        public static string BuildTokenAuthenticationScopedAuthorizationRequestBody(string token, string tenantId)
        {
            string responseBody;

            var assembly = typeof(OpenStack.Iam.Authentication.Models.AuthenticationAndTokenManagementRequestBodyFactory).GetTypeInfo().Assembly;
            var embeddedFileProvider = new EmbeddedFileProvider(assembly, s_BaseNamespace);

            using (var stream = embeddedFileProvider.GetFileInfo("TokenAuthenticationScopedAuthorization.json").CreateReadStream())
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

            return String.Format(responseBody, token, tenantId);
        }

    }
}
    