$ErrorActionPreference = "Stop"

$apiUrl    = "http://localhost:5176/api/aeroports"
$swaggerUrl = "http://localhost:5176/swagger"
$frontUrl  = "http://localhost:4200"

Write-Host ""
Write-Host "=== AeroManag Web — Demarrage ===" -ForegroundColor Cyan
Write-Host ""
Write-Host "Demarrage du backend (API)..." -ForegroundColor Cyan

$backendArgs = "run --project src/AeroManag.Api"
$backend = Start-Process -FilePath "dotnet" `
    -ArgumentList $backendArgs `
    -PassThru `
    -NoNewWindow

try {
    # Attendre que l'API reponde (max 60 secondes)
    $maxAttempts = 60
    $attempt     = 0
    Write-Host "Attente que l'API soit prete sur $apiUrl ..." -ForegroundColor Yellow

    while ($attempt -lt $maxAttempts) {
        try {
            Invoke-WebRequest -Uri $apiUrl -UseBasicParsing -TimeoutSec 2 | Out-Null
            break
        } catch {
            if ($backend.HasExited) {
                Write-Host "ERREUR : le backend s'est arrete de facon inattendue." -ForegroundColor Red
                exit 1
            }
            Start-Sleep -Seconds 1
            $attempt++
        }
    }

    if ($attempt -ge $maxAttempts) {
        Write-Host "ERREUR : l'API n'a pas repondu apres $maxAttempts secondes." -ForegroundColor Red
        Stop-Process -Id $backend.Id -Force
        exit 1
    }

    Write-Host ""
    Write-Host "  API Swagger  : $swaggerUrl" -ForegroundColor Green
    Write-Host "  Frontend     : $frontUrl" -ForegroundColor Green
    Write-Host ""
    Write-Host "Appuyez sur Ctrl+C pour tout arreter." -ForegroundColor Yellow
    Write-Host ""

    Set-Location aeromanag-frontend
    ng serve
}
finally {
    Set-Location $PSScriptRoot
    if ($null -ne $backend -and -not $backend.HasExited) {
        Write-Host ""
        Write-Host "Arret du backend..." -ForegroundColor Cyan
        Stop-Process -Id $backend.Id -Force
        Write-Host "Backend arrete." -ForegroundColor Cyan
    }
}
