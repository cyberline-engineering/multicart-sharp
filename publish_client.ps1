Remove-Item ./packages/Cle.MulticartApi.Client -Recurse
dotnet build ./Cle.MulticartApi.Client/Cle.MulticartApi.Client.csproj --configuration Release -o ./packages/Cle.MulticartApi.Client
.\nuget push .\packages\Cle.MulticartApi.Client\*.nupkg -Source https://api.nuget.org/v3/index.json