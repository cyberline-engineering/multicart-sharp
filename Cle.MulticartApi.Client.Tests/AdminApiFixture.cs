using System.Reflection;
using Cle.MulticartApi.Client.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cle.MulticartApi.Client.Tests
{
    public class AdminApiFixture
    {
        public AdminApiFixture()
        {
            var host = Host
                .CreateDefaultBuilder()
                .UseEnvironment("Development")
                //.ConfigureMulticartApi(
                //    (_, _, options) => { options.BaseUrl = new Uri("https://stage.redoc.cledeploy.com"); },
                //    //You can set the default http client parameters
                //    (client, _) =>
                //    {
                //        // As default base address configuration
                //        //client.BaseAddress = configuration.BaseUrl;

                //        client.DefaultRequestHeaders.Date = DateTimeOffset.Now;

                //        //Use custom authentication token
                //        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                //    }, 
                //    //You can use http client builder
                //    (builder, _) =>
                //    {
                //        //builder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler());
                //    }
                //)
                .ConfigureAppConfiguration(builder => builder.AddUserSecrets(Assembly.GetExecutingAssembly(), true))
                .ConfigureServices((hostContext, collection) =>
                {
                    collection.AddMulticartClientForService(hostContext.Configuration,
                        options: o =>
                        {
                            //o.BaseUrl = new Uri("https://stage.redoc.cledeploy.com");
                            o.BaseUrl = new Uri("http://localhost:5001");
                        });
                })
                .Build();

            Services = host.Services;
        }

        public IServiceProvider Services { get; }
    }
}
