function Set-ConsoleForegroundColor ($foregroundColor) {
    $currentForegroundColor = $Host.UI.RawUI.ForegroundColor
    $Host.UI.RawUI.ForegroundColor = $foregroundColor
    return $currentForegroundColor
}

$currentForegroundColor = Set-ConsoleForegroundColor Green
Write-Host "`r`nScript started! Tearing down Identity database using MsSql."

$projectPath = Get-Location
$mssqlProjectPath = "$($projectPath)\Identity.Database.MsSql\Migrations"

# Tear down Docker containers
Set-ConsoleForegroundColor DarkCyan | Out-Null
Write-Host "`r`nTear down Identity MsSql database in Docker container started"
docker-compose -f docker-compose-mssql.yml down --volumes
Write-Host "Completed tearing down of Identity MsSql database in Docker container"

# Remove old migrations
Set-ConsoleForegroundColor Yellow | Out-Null
Write-Host "`r`n"

if (Test-Path -Path $mssqlProjectPath) {
    Remove-Item -Recurse -Path $mssqlProjectPath
    Write-Host "Removed MsSql database migrations folder '$($mssqlProjectPath)'"
}

# Complete script
Set-ConsoleForegroundColor Green | Out-Null
Write-Host "`r`nScript completed!"
Write-Host "Tearing down of MSSQL database and migrations completed"
Write-Host "`r`n"

Set-ConsoleForegroundColor $currentForegroundColor | Out-Null