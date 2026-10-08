<#
.SYNOPSIS
Creates and validates a rootful Podman machine for the GS1 application.

.DESCRIPTION
The script requires Podman CLI v5.8.31, because Podman 6 is not compatible with Aspire in this setup. 
Run Install-PodmanDesktop.ps1 first to install Podman Desktop and CLI.
#>

$Name = "podman-machine-default"

$ErrorActionPreference = "Stop"

if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
    throw "Podman is not installed or unavailable in PATH. Run Install-PodmanDesktop.ps1 first, then run this script again in the new PowerShell window."
}

# Check whether the default machine already exists before creating it.
$previousErrorActionPreference = $ErrorActionPreference
try {
    $ErrorActionPreference = "Continue"
    & podman machine inspect $Name *> $null
    $inspectExitCode = $LASTEXITCODE
}
finally {
    $ErrorActionPreference = $previousErrorActionPreference
}
$machineExists = $inspectExitCode -eq 0

if ($machineExists) {
    throw "Podman machine '$Name' already exists. Remove it before running this script again."
}

Write-Host "Creating Podman machine '$Name'..."
# --now starts the machine immediately after initialization.
& podman machine init `
    --rootful `
    --now `
    $Name
if ($LASTEXITCODE -ne 0) {
    throw "podman machine init failed with exit code $LASTEXITCODE."
}

# Run a disposable container as an end-to-end check of the machine configuration.
Write-Host "Validating attached container execution..."
& podman run --rm quay.io/podman/hello
if ($LASTEXITCODE -ne 0) {
    throw "podman run failed with exit code $LASTEXITCODE."
}

Write-Host "Podman machine '$Name' is ready."