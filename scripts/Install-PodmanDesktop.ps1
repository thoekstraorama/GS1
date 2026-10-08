<#
.SYNOPSIS
Installs Podman Desktop

.DESCRIPTION
Installs Podman CLI v5.8.3, because Podman v6 is not compatible with Aspire in this setup. 
After installation, run Install-PodmanMachine.ps1.
#>

$ErrorActionPreference = "Stop"

if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
    throw "winget is unavailable. Install Podman Desktop manually, then run Install-PodmanMachine.ps1."
}

Write-Host "Installing Podman Desktop..."
& winget install --id RedHat.Podman-Desktop `
    --source winget `
    --accept-source-agreements `
    --accept-package-agreements
if ($LASTEXITCODE -ne 0) {
    throw "Podman Desktop installation failed with exit code $LASTEXITCODE."
}

Write-Host "Installing Podman CLI v5.8.3..."
& winget install --id RedHat.Podman `
    --version 5.8.3 `
    --source winget `
    --accept-source-agreements `
    --accept-package-agreements
if ($LASTEXITCODE -ne 0) {
    throw "Podman CLI installation failed with exit code $LASTEXITCODE."
}

Write-Host "Open a new PowerShell window for Podman machine setup..."