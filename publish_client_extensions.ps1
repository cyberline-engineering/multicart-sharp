Remove-Item ./packages/Cle.MulticartApi.Client.Extensions -Recurse
dotnet build ./Cle.MulticartApi.Client.Extensions/Cle.MulticartApi.Client.Extensions.csproj --configuration Release -o ./packages/Cle.MulticartApi.Client.Extensions
.\nuget push .\packages\Cle.MulticartApi.Client.Extensions\*.nupkg -Source https://api.nuget.org/v3/index.json -SkipDuplicate