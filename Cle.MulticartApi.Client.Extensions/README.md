# Cle.MulticartApi.Client.Extensions: A .net library extensions for Multicartshop&copy; API Client
[![NuGet](https://img.shields.io/nuget/v/Multicartshop.Client.Extensions.svg?maxAge=3600)](https://www.nuget.org/packages/Multicartshop.Client.Extensions/)

# 1. Installation

Cle.MulticartApi.Client.Extensions is [available on NuGet](https://www.nuget.org/packages/Multicartshop.Client.Extensions/). Use the package manager
console in Visual Studio to install it:

```pwsh
Install-Package Cle.MulticartApi.Client.Extensions
```

If you're using .NET Core, you can use the `dotnet` command from your favorite shell:

```sh
dotnet add package Cle.MulticartApi.Client.Extensions
```

# 2. Using Cle.MulticartApi.Client.Extensions

The `AddMulticartClientForService` extension method is used to register services in the DI container.

Example for console app or tests:

```
var host = Host
                .CreateDefaultBuilder()
                .ConfigureServices((hostContext, collection) =>
                {
                    collection.AddMulticartClientForService(hostContext.Configuration,
                        options: o =>
                        {
                            o.BaseUrl = new Uri("https://stage.redoc.cledeploy.com");
                        });
                })
                .Build();
```

OAuth client need any options in appsettings.json configuration file:

``` json
{
  "IdentityOAuthConfig": {
    "Endpoint": "https://stage.identity.multicartshop.com"
  },
  "IdentityResourceConfig": {
    "ClientId": "",
    "ClientSecret": "",
    "Scopes": [ "multicart.api" ]
  }
}
```

# 3. For more information on using the API, see the test projects
[Cle.MulticartApi.Client.Tests](https://github.com/cyberline-engineering/multicart-sharp/tree/main/Cle.MulticartApi.Client.Tests)