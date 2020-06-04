param([string]$buildEnv = 'Development', [string]$migrationName)
$env:ASPNETCORE_ENVIRONMENT = $buildEnv;
dotnet ef database update $migrationName --project ../ --startup-project ../../Library.WebApi