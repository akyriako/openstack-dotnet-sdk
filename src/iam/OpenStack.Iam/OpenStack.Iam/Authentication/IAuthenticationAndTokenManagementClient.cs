using System.Threading.Tasks;

namespace OpenStack.Iam.Authentication
{
    public interface IAuthenticationAndTokenManagementClient
    {
        Task<string> GetTokenPasswordAuthenticationUnscopedAuthorizationAsync();
    }
}