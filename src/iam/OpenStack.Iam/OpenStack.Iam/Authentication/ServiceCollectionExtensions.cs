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
                string baseAddress = $"{options.BaseUri.TrimEnd('/')}/{options.Version}/";
                httpClient.BaseAddress = new Uri(baseAddress);
                //httpClient.BaseAddress = new Uri(new Uri(options.BaseUri), options.Version);
                httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                httpClient.DefaultRequestHeaders.Add("User-Agent", OpenStackNamedClients.OSDK_NC);
                httpClient.Timeout = new TimeSpan(0, 0, 10);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler
                {
                    ClientCertificateOptions = ClientCertificateOption.Manual,
                    ServerCertificateCustomValidationCallback =
                        (httpRequestMessage, cert, certChain, policyErrors) => true
                };
            }); ;

            services.AddScoped<IAuthenticationAndTokenManagementClient>(ctx =>
            {
                var httpClientFactory = ctx.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(namedClient);

                return new AuthenticationAndTokenManagementClient(httpClient, options);
            });

            return services;
        }

        //public static IServiceCollection AddOpenStackServiceClient
        //    (this IServiceCollection services, IConfiguration configuration, string client)
        //{
        //    switch (client)
        //    {
        //        case OpenStackNamedClients.OSDK_NC_IAM:
        //            return AddAuthenticationAndTokenManagementClient(services, configuration, client);
        //        default:
        //            break;
        //    }

        //    return services;
        //}
    }
}
