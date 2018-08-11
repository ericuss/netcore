1.  Se añaden el nuget
    *   coverlet.msbuild
2.  Se pone en las dependencies.props
3.  Se ejecuta el commando y funciona
    *   dotnet watch test Lanre.Clients.Api.Tests.csproj -p:CollectCoverage=true
    *   dotnet test Lanre.Clients.Api.Tests.csproj -p:CollectCoverage=true
4.  Se acualiza el .gitignore