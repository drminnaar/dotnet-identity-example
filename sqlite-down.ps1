function Set-ConsoleForegroundColor ($foregroundColor) {
  $currentForegroundColor = $Host.UI.RawUI.ForegroundColor
  $Host.UI.RawUI.ForegroundColor = $foregroundColor
  return $currentForegroundColor
}

$projectPath = Get-Location
$sqliteMigrationsPath = "$($projectPath)/Identity.Database.Sqlite/Migrations"
$sqliteDbPath = "$($projectPath)/Identity.Database.Sqlite/identity.db"

$currentForegroundColor = Set-ConsoleForegroundColor "Green"
Write-Host "`r`nScript started! Tearing down Identity database using Sqlite."

# Remove old data and migrations
Set-ConsoleForegroundColor Yellow | Out-Null
Write-Host "`r`n"

if (Test-Path -Path $sqliteDbPath) {
  Remove-Item -Path $sqliteDbPath
  Write-Host "Removed Sqlite Db '$($sqliteDbPath)'"
}

if (Test-Path -Path $sqliteMigrationsPath) {
  Remove-Item -Recurse -Path $sqliteMigrationsPath
  Write-Host "Removed Sqlite database migrations folder '$($sqliteMigrationsPath)'"
}

# Complete script
Set-ConsoleForegroundColor Green | Out-Null
Write-Host "`r`nScript completed! SQLite database and migrations was removed successfully"
Write-Host "`r`n"

Set-ConsoleForegroundColor $currentForegroundColor | Out-Null