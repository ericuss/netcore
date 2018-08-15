[CmdletBinding(SupportsShouldProcess = $true)]
Param([String]$migration)

dotnet ef migrations add $migration -c Lanre.Data.Context.Contexts.SchedulerContext -p ../../data/Lanre.Data.Context/Lanre.Data.Context.csproj -o Migrations/Schedule