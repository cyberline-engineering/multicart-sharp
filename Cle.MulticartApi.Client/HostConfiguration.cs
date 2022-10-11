using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using Cle.MulticartApi.Client.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Cle.MulticartApi.Client
{
    /// <summary>
    /// HostConfiguration options
    /// </summary>
    public class HostConfiguration
    {
        private readonly IServiceCollection services;

        internal bool HttpClientsAdded { get; private set; }
        /// <summary>
        /// Client base uri
        /// </summary>
        public Uri? BaseUrl { get; set; }

        /// <summary>
        /// HostConfiguration
        /// </summary>
        /// <param name="services"></param>
        public HostConfiguration(IServiceCollection services)
        {
            this.services = services;

            this.services.AddSingleton<ICartItemClient, CartItemClient>();
            this.services.AddSingleton<IAdminCartItemClient, AdminCartItemClient>();
            this.services.AddSingleton<IOfferClient, OfferClient>();
        }

        /// <summary>
        /// Configures the HttpClients.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public HostConfiguration AddApiHttpClients<TCartItemClient, TOfferClient, TAdminCartItemClient>
        (
            Action<HttpClient, HostConfiguration>? client = default, Action<IHttpClientBuilder, HostConfiguration>? builder = default)
            where TCartItemClient : class, ICartItemClient
            where TOfferClient : class, IOfferClient
            where TAdminCartItemClient : class, IAdminCartItemClient
        {
            void ConfigureClientAction<T>(HttpClient configureClient)
            {
                configureClient.DefaultRequestHeaders.UserAgent.Clear();
                configureClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(nameof(T),
                    Assembly.GetExecutingAssembly().GetName().Version?.ToString(3)));

                configureClient.BaseAddress ??= BaseUrl;

                client?.Invoke(configureClient, this);
            }

            var builders = new[]
            {
                services.AddHttpClient<ICartItemClient, TCartItemClient>((Action<HttpClient>)ConfigureClientAction<TCartItemClient>),
                services.AddHttpClient<TOfferClient, TOfferClient>((Action<HttpClient>)ConfigureClientAction<TOfferClient>),
                services.AddHttpClient<IAdminCartItemClient, TAdminCartItemClient>(
                    (Action<HttpClient>)ConfigureClientAction<TAdminCartItemClient>),
            };

            if (builder != null)
                foreach (var instance in builders)
                    builder(instance, this);

            HttpClientsAdded = true;

            return this;
        }

        /// <summary>
        /// Configures the HttpClients.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public HostConfiguration AddApiHttpClients(Action<HttpClient, HostConfiguration>? client = default,
            Action<IHttpClientBuilder, HostConfiguration>? builder = default)
        {
            AddApiHttpClients<CartItemClient, OfferClient, AdminCartItemClient>(client, builder);

            return this;
        }
    }
}