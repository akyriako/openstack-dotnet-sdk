using System.Threading.Tasks;

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
    }
}