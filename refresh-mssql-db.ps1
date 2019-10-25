function Set-ConsoleForegroundColor ($foregroundColor) {
    $currentForegroundColor = $Host.UI.RawUI.ForegroundColor
    $Host.UI.RawUI.ForegroundColor = $foregroundColor
    return $currentForegroundColor
}

$currentForegroundColor = Set-ConsoleForegroundColor Green
Write-Host "`r`nScript started! Creating new Identity database using MsSql."

$projectPath = Get-Location
$mssqlProjectPath = "$($projectPath)\Identity.Database.MsSql\Migrations"

# Run MsSql in Docker container
Set-ConsoleForegroundColor DarkCyan | Out-Null
Write-Host "`r`nSetup Identity MsSql database in Docker container started"
docker-compose -f docker-compose-mssql.yml down
docker-compose -f docker-compose-mssql.yml up -d
Write-Host "Completed setup of Identity MsSql database in Docker container"

# Clean, Restore and Build solution
Set-ConsoleForegroundColor White | Out-Null

Write-Host "`r`nBuilding solution"
dotnet clean
dotnet restore
dotnet build --no-restore --configuration Release
Write-Host "Completed building solution"

# Remove old migrations
Set-ConsoleForegroundColor Yellow | Out-Null
Write-Host "`r`n"

if (Test-Path -Path $mssqlProjectPath) {
    Remove-Item -Recurse -Path $mssqlProjectPath
    Write-Host "Removed MsSql database migrations folder '$($mssqlProjectPath)'"
}

# Create MsSql migrations and update database
Set-ConsoleForegroundColor Magenta | Out-Null

Write-Host "`r`nCreating initial MsSql Identity database migration"
dotnet-ef migrations add --startup-project .\Identity.Database.Mssql CreateInitialIdentitySchema
Write-Host "Created initial MsSql Identity database migration"

Set-ConsoleForegroundColor DarkMagenta | Out-Null

Write-Host "`r`nStarting MsSql Identity database migrations"
dotnet-ef database update --startup-project .\Identity.Database.Mssql
Write-Host "Completed MsSql Identity database migrations"

Set-ConsoleForegroundColor Green | Out-Null

Write-Host "`r`nScript completed!"
Write-Host "Connect to MsSql Identity database using the following details (as per 'docker-compose-mssql.yml'):"
Write-Host "  - server: localhost (native client)"
Write-Host "  - username: sa"
Write-Host "  - password: P@ssword123!"
Write-Host "`r`n"

Set-ConsoleForegroundColor $currentForegroundColor | Out-Null