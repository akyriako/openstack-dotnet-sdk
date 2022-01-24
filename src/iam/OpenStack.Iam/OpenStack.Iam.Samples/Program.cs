using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenStack.Iam.Authentication;
using OpenStack.Core.Extensions;

namespace OpenStack.Iam.Samples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();
            var configuration = BootstrapConfigurationBuilder(configurationBuilder).Build();

            var services = new ServiceCollection();

            services.AddOptions();
            services.AddAuthenticationAndTokenManagementClient(configuration);

            var serviceProvider = services.BuildServiceProvider();

            var authenticationAndTokenManagementClient = serviceProvider.GetService<IAuthenticationAndTokenManagementClient>();

            var token = await authenticationAndTokenManagementClient.
                GetTokenPasswordAuthenticationScopedAuthorizationAsync(
                "5e77900bc83e4ee0a0f1be58dcec635c",
                "9a9bc4e78cad4b8286c9abb07d05404b",
                "bcf5d5acb6c0ff1875344cfd9f3");

            var responseAsJson = await authenticationAndTokenManagementClient.ValidateAndShowInformationForTokenAsync(token.Token);

            Console.WriteLine(responseAsJson.ToJsonString());

            Console.ReadLine();
        }

        static IConfigurationBuilder BootstrapConfigurationBuilder(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            return builder;
        }
    }
}
