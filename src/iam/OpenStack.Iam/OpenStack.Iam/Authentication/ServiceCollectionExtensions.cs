using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using OpenStack.Iam.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationAndTokenManagementClient
            (this IServiceCollection services, IConfiguration configuration, string namedClient = OpenStackNamedClients.OSDK_NC_IAM)
        {
            var optionsSection = configuration.GetSection(OpenStackNamedClients.OSDK_NC_IAM);
            var options = optionsSection.Get<AuthenticationAndTokenManagementClientOptions>();

            services.Configure<AuthenticationAndTokenManagementClientOptions>(optionsSection);

            services.AddHttpClient(namedClient, httpClient =>
            {
                httpClient.BaseAddress = new Uri(options.BaseUri);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                httpClient.DefaultRequestHeaders.Add("User-Agent", OpenStackNamedClients.OSDK_NC);
            });

            services.AddScoped<IAuthenticationAndTokenManagementClient>(ctx =>
            {
                var httpClientFactory = ctx.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(namedClient);

                return new AuthenticationAndTokenManagementClient(httpClient, options);
            });

            return services;
        }
    }
}
