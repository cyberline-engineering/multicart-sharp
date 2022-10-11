# Cle.MulticartApi.Client: A .net library for Multicartshop&copy; API
[![NuGet](https://img.shields.io/nuget/v/Cle.MulticartApi.Client.svg?maxAge=3600)](https://www.nuget.org/packages/Cle.MulticartApi.Client/)

# 1. Installation

Cle.MulticartApi.Client is [available on NuGet](https://www.nuget.org/packages/Cle.MulticartApi.Client/). Use the package manager
console in Visual Studio to install it:

```pwsh
Install-Package Cle.MulticartApi.Client
```

If you're using .NET Core, you can use the `dotnet` command from your favorite shell:

```sh
dotnet add package Cle.MulticartApi.Client
```

# 2. Using Cle.MulticartApi.Client

The `AddMulticartApi` extension method is used to register services in the DI container.

Example for console app or tests:

```
var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((hostContext, collection) =>
                {
                    collection.AddMulticartApi(hostContext.Configuration,
                        options: o =>
                        {
                            o.BaseUrl = new Uri("https://stage.redoc.cledeploy.com");
                        });
                })
                .Build();

  var adminCartItemClient = host.Services.GetRequiredService<IAdminCartItemClient>();
```

As an alternative, you can use IHost configuration extension method `ConfigureMulticartApi`

```
 var host = Host
                .CreateDefaultBuilder()
                .UseEnvironment("Development")
                .ConfigureMulticartApi(
                    (_, _, options) => { options.BaseUrl = new Uri("https://stage.redoc.cledeploy.com"); },
                    //You can set the default http client parameters
                    (client, _) =>
                    {
                        // As default base address configuration
                        //client.BaseAddress = configuration.BaseUrl;

                        client.DefaultRequestHeaders.Date = DateTimeOffset.Now;

                        //Use custom authentication token
                        //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }, 
                    //You can use http client builder
                    (builder, _) =>
                    {
                        //builder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler());
                    }
                )
                .Build();
```

# 3. For more information on using the API, see the test projects
[Cle.MulticartApi.Client.Tests](https://github.com/cyberline-engineering/multicart-sharp/tree/main/Cle.MulticartApi.Client.Tests)