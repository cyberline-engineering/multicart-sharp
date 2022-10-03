using Cle.Identity.Extensions;
using Cle.Identity.Extensions.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cle.MulticartApi.Client.Extensions
{
    public static class Extensions
    {
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