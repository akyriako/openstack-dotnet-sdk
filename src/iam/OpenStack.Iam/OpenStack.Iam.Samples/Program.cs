using System;
using OpenStack.Iam.Authentication;

namespace OpenStack.Iam.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            string keyStoneUrl = "https://192.168.57.12:5000/";

            AuthenticationAndTokenManagementClientOptions options =
             new AuthenticationAndTokenManagementClientOptions(AuthenticationAndTokenManagementVersion.v2);

            AuthenticationAndTokenManagementClient authenticationAndTokenManagementClient =
                new AuthenticationAndTokenManagementClient(keyStoneUrl, options);

            Console.WriteLine(authenticationAndTokenManagementClient.GetEndpointUri());

            Console.ReadLine();
        }
    }
}
