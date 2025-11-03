# Test the application in standalone mode (without IIS)
param(
    [string]$ProjectPath = "."
)

Write-Host "🧪 Testing application in standalone mode..." -ForegroundColor Green

# Change to project directory
Set-Location $ProjectPath

# Check if project file exists
if (!(Test-Path "ArzTi3Server.csproj")) {
    Write-Error "Project file not found. Make sure you're in the correct directory."
    exit 1
}

Write-Host "📦 Building application..." -ForegroundColor Yellow
try {
    dotnet build -c Release
    if ($LASTEXITCODE -ne 0) {
        throw "Build failed"
    }
    Write-Host "✓ Build successful" -ForegroundColor Green
} catch {
    Write-Error "Build failed: $_"
    exit 1
}

Write-Host "`n🚀 Starting application in development mode..." -ForegroundColor Yellow
Write-Host "Press Ctrl+C to stop the application" -ForegroundColor Cyan

# Set environment variable for development
$env:ASPNETCORE_ENVIRONMENT = "Development"

# Start the application
try {
    dotnet run --no-build --project ArzTi3Server.csproj
} catch {
    Write-Host "Application stopped or failed: $_" -ForegroundColor Red
}