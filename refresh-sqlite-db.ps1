function Set-ConsoleForegroundColor ($foregroundColor) {
    $currentForegroundColor = $Host.UI.RawUI.ForegroundColor
    $Host.UI.RawUI.ForegroundColor = $foregroundColor
    return $currentForegroundColor
}

$projectPath = Get-Location
$sqliteProjectPath = "$($projectPath)\Identity.Database.Sqlite\Migrations"
$sqliteDbPath = "$($projectPath)\data"

$currentForegroundColor = Set-ConsoleForegroundColor "Green"
Write-Host "`r`nScript started! Creating new Identity database using Sqlite."

# Clean, Restore and Build solution
Set-ConsoleForegroundColor White | Out-Null

Write-Host "`r`nBuilding solution"
dotnet clean
dotnet restore
dotnet build --no-restore --configuration Release
Write-Host "Completed building solution"

# Remove old data and migrations
Set-ConsoleForegroundColor Yellow | Out-Null
Write-Host "`r`n"

if (Test-Path -Path $sqliteDbPath) {
    Remove-Item -Recurse -Path $sqliteDbPath
    Write-Host "Removed Sqlite Db folder '$($sqliteDbPath)'"
}

if (Test-Path -Path $sqliteProjectPath) {
    Remove-Item -Recurse -Path $sqliteProjectPath
    Write-Host "Removed Sqlite database migrations folder '$($sqliteProjectPath)'"
}

Write-Host "`r`nCreating Sqlite Db folder '$($sqliteDbPath)'"
New-Item -Path $sqliteDbPath -ItemType Directory | Out-Null
Write-Host "Created Sqlite Db folder '$($sqliteDbPath)'."

# Create Sqlite migrations and update database
Set-ConsoleForegroundColor Magenta | Out-Null

Write-Host "`r`nCreating initial Sqlite Identity database migration"
dotnet-ef migrations add --startup-project .\Identity.Database.Sqlite CreateInitialIdentitySchema
Write-Host "Created initial Sqlite Identity database migration"

Set-ConsoleForegroundColor DarkMagenta | Out-Null

Write-Host "`r`nStarting Sqlite Identity database migrations"
dotnet-ef database update --startup-project .\Identity.Database.Sqlite
Write-Host "Completed Sqlite Identity database migrations"

Set-ConsoleForegroundColor Green | Out-Null

Write-Host "`r`nScript completed! You can find the Sqlite database file located at '$($sqliteDbPath)'"
Write-Host "`r`n"

Set-ConsoleForegroundColor $currentForegroundColor | Out-Null