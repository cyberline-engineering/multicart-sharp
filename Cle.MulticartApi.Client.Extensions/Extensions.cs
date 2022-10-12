using Cle.Identity.Extensions;
using Cle.Identity.Extensions.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cle.MulticartApi.Client.Extensions
{
    /// <summary>
    /// MulticartApi Client Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Init MulticartApiClient for service-to-service communication using OAuth Client Credential Grant
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="identityResourceConfigSection"></param>
        /// <param name="options"></param>
        /// <param name="client"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddMulticartClientForService(this IServiceCollection services,
            IConfiguration configuration, IConfiguration? identityResourceConfigSection = default,
            Action<HostConfiguration>? options = default, Action<HttpClient, HostConfiguration>? client = default,
            Action<IHttpClientBuilder, HostConfiguration>? builder = default)
        {
            return services
                    .AddIntrospectionSecurityTokenAccessor(identityResourceConfigSection ?? configuration)
                    .AddMulticartClient<IntrospectionSecurityTokenAccessor>(configuration, options, client, builder)
                ;
        }

        /// <summary>
        /// Init MulticartApiClient for customer-to-service communication using OAuth Access Token from HttpContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        /// <param name="client"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IServiceCollection AddMulticartClientForUser(this IServiceCollection services,
            IConfiguration configuration, Action<HostConfiguration>? options = default,
            Action<HttpClient, HostConfiguration>? client = default,
            Action<IHttpClientBuilder, HostConfiguration>? builder = default)
        {
            return services
                    .AddHttpContextSecurityTokenAccessor()
                    .AddMulticartClient<HttpContextSecurityTokenAccessor>(configuration, options, client, builder)
                ;
        }

        private static IServiceCollection AddMulticartClient<T>(this IServiceCollection services,
            IConfiguration configuration, Action<HostConfiguration>? options,
            Action<HttpClient, HostConfiguration>? client,
            Action<IHttpClientBuilder, HostConfiguration>? builder) where T : ISecurityTokenAccessor
        {
            return services
                .AddCleIdentityClient(configuration)
                .AddMulticartApi(options, client, (b, c) =>
                {
                    b.UseCleIdentityClient<T>();
                    builder?.Invoke(b, c);
                });
        }
    }
}