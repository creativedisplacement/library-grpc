param([Parameter(Mandatory=$true)][string]$migrationName)
dotnet ef --project ../ --startup-project ../../Library.WebApi migrations add $migrationName