function Set-ConsoleForegroundColor ($foregroundColor) {
    $currentForegroundColor = $Host.UI.RawUI.ForegroundColor
    $Host.UI.RawUI.ForegroundColor = $foregroundColor
    return $currentForegroundColor
}

$currentForegroundColor = Set-ConsoleForegroundColor Green
Write-Host "`r`nScript started! Tearing down Identity database using Postgres."

$projectPath = Get-Location
$pgsqlProjectPath = "$($projectPath)\Identity.Database.Postgres\Migrations"

# Tear down Docker containers
Set-ConsoleForegroundColor DarkCyan | Out-Null
Write-Host "`r`nTear down Identity Postgres database in Docker container started"
docker-compose -f docker-compose-pg.yml down --volumes
Write-Host "Completed tearing down of Identity Postgres database in Docker container"

# Remove old migrations
Set-ConsoleForegroundColor Yellow | Out-Null
Write-Host "`r`n"

if (Test-Path -Path $pgsqlProjectPath) {
    Remove-Item -Recurse -Path $pgsqlProjectPath
    Write-Host "Removed Postgres database migrations folder '$($pgsqlProjectPath)'"
}

# Complete script
Set-ConsoleForegroundColor Green | Out-Null
Write-Host "`r`nScript completed!"
Write-Host "Tearing down of Postgres database and migrations completed"
Write-Host "`r`n"

Set-ConsoleForegroundColor $currentForegroundColor | Out-Null