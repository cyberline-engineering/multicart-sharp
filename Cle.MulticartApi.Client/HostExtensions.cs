using System;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cle.MulticartApi.Client
{
    public static class HostExtensions
    {
        /// <summary>
        /// Add the multicart api to your host builder.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <param name="client"></param>
        /// <param name="clientBuilder"></param>
        public static IHostBuilder ConfigureMulticartApi(this IHostBuilder builder,
            Action<HostBuilderContext, IServiceCollection, HostConfiguration>? options = default,
            Action<HttpClient, HostConfiguration>? client = default,
            Action<IHttpClientBuilder, HostConfiguration>? clientBuilder = default)
        {
            builder.ConfigureServices((context, services) =>
            {
                var config = new HostConfiguration(services);
                options?.Invoke(context, services, config);
                AddMulticartApi(config, client, clientBuilder);
            });

            return builder;
        }

        /// <summary>
        /// Add the api to your host builder.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <param name="client"></param>
        /// <param name="builder"></param>
        public static IServiceCollection AddMulticartApi(this IServiceCollection services,
            Action<HostConfiguration>? options = default, Action<HttpClient, HostConfiguration>? client = default,
            Action<IHttpClientBuilder, HostConfiguration>? builder = default)
        {
            var config = new HostConfiguration(services);
            options?.Invoke(config);
            AddMulticartApi(config, client, builder);

            return services;
        }

        private static void AddMulticartApi(HostConfiguration host,
            Action<HttpClient, HostConfiguration>? client = default,
            Action<IHttpClientBuilder, HostConfiguration>? builder = default)
        {
            if (!host.HttpClientsAdded)
                host.AddApiHttpClients(client, builder);
        }
    }
}
