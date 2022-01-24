using System.Text.Json;
using System.Threading.Tasks;
using OpenStack.Core;

namespace OpenStack.Iam.Authentication
{
    public interface IAuthenticationAndTokenManagementClient
    {
        Task<AccessToken> GetTokenPasswordAuthenticationUnscopedAuthorizationAsync(
            string username,
            string domain,
            string password);

        Task<AccessToken> GetTokenPasswordAuthenticationScopedAuthorizationAsync(
            string userId,
            string tenantId,
            string password);

        Task<AccessToken> GetTokenPasswordAuthenticationExplicitUnscopedAuthorizationAsync(
            string userId,
            string password);

        Task<AccessToken> GetTokenTokenAuthenticationUnscopedAuthorizationAsync(
            string token);

        Task<AccessToken> GetTokenTokenAuthenticationScopedAuthorizationAsync(
            string token,
            string tenantId);

        Task<JsonDocument> ValidateAndShowInformationForTokenAsync(
            string token);
    }
}