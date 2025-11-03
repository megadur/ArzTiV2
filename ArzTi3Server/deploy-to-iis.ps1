# Enhanced PowerShell script for IIS deployment with detailed diagnostics
param(
    [string]$SiteName = "ArzTi3",
    [string]$AppPoolName = "ArzTi3AppPool",
    [string]$PhysicalPath = "C:\inetpub\wwwroot\ArzTi3"
)

# Function to check if running as administrator
function Test-Administrator {
    $currentUser = [Security.Principal.WindowsIdentity]::GetCurrent()
    $principal = New-Object Security.Principal.WindowsPrincipal($currentUser)
    return $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
}

# Check if running as administrator
if (-not (Test-Administrator)) {
    Write-Error "This script must be run as Administrator. Please restart PowerShell as Administrator."
    exit 1
}

Write-Host "🚀 Starting deployment process..." -ForegroundColor Green

# Get script directory and navigate to project directory
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
Write-Host "Script location: $scriptPath" -ForegroundColor Cyan

# Change to the project directory (where the script is located)
Set-Location $scriptPath
Write-Host "Working directory: $(Get-Location)" -ForegroundColor Cyan

# Import WebAdministration module
try {
    Import-Module WebAdministration -ErrorAction Stop
    Write-Host "✓ WebAdministration module loaded successfully" -ForegroundColor Green
} catch {
    Write-Error "Failed to load WebAdministration module. Please ensure IIS Management Tools are installed."
    exit 1
}

# Check .NET installation
Write-Host "`n🔍 Checking .NET installation..." -ForegroundColor Yellow
try {
    $dotnetVersion = dotnet --version
    Write-Host "✓ .NET Version: $dotnetVersion" -ForegroundColor Green
    
    # Check if this is .NET 6 or compatible
    if ($dotnetVersion -like "6.*" -or $dotnetVersion -like "7.*" -or $dotnetVersion -like "8.*" -or $dotnetVersion -like "9.*" -or $dotnetVersion -like "10.*") {
        Write-Host "✓ Compatible .NET version detected" -ForegroundColor Green
    } else {
        Write-Warning "Unexpected .NET version. This project targets .NET 6."
    }
} catch {
    Write-Error ".NET CLI not found. Please install .NET 6 SDK or later."
    exit 1
}

# Check project file exists
$projectFile = "ArzTi3Server.csproj"
if (!(Test-Path $projectFile)) {
    Write-Host "Current directory contents:" -ForegroundColor Yellow
    Get-ChildItem | Format-Table Name, Length
    Write-Error "Project file not found: $projectFile in directory $(Get-Location)"
    exit 1
}
Write-Host "✓ Project file found: $projectFile" -ForegroundColor Green

# Check for solution file and referenced projects
$solutionFiles = Get-ChildItem "*.sln" -ErrorAction SilentlyContinue
if ($solutionFiles) {
    Write-Host "✓ Solution file found: $($solutionFiles[0].Name)" -ForegroundColor Green
    $useSolution = $true
    $buildTarget = $solutionFiles[0].Name
} else {
    Write-Host "ℹ️ No solution file found, building project directly" -ForegroundColor Yellow
    $useSolution = $false
    $buildTarget = $projectFile
}

# Stop application pool if it exists
if (Get-IISAppPool -Name $AppPoolName -ErrorAction SilentlyContinue) {
    Write-Host "`n🛑 Stopping application pool: $AppPoolName" -ForegroundColor Yellow
    try {
        Stop-WebAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
        Write-Host "✓ Stopped application pool: $AppPoolName" -ForegroundColor Green
    } catch {
        Write-Host "ℹ️ Application pool was already stopped" -ForegroundColor Yellow
    }
    Start-Sleep -Seconds 3
}

# Stop existing web application if it exists
if (Get-WebApplication -Name $SiteName -ErrorAction SilentlyContinue) {
    Write-Host "🗑️ Removing existing web application: $SiteName" -ForegroundColor Yellow
    Remove-WebApplication -Name $SiteName -Site "Default Web Site"
    Write-Host "✓ Removed existing web application: $SiteName" -ForegroundColor Green
}

# Clean previous publish folder
$publishFolder = "./publish"
if (Test-Path $publishFolder) {
    Write-Host "`n🧹 Cleaning previous publish folder..." -ForegroundColor Yellow
    Remove-Item -Path $publishFolder -Recurse -Force
    Write-Host "✓ Previous publish folder cleaned" -ForegroundColor Green
}

# Restore dependencies first
Write-Host "`n📦 Restoring NuGet packages..." -ForegroundColor Yellow
try {
    if ($useSolution) {
        $restoreOutput = dotnet restore $buildTarget --verbosity minimal 2>&1
    } else {
        $restoreOutput = dotnet restore --verbosity minimal 2>&1
    }
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Restore output:" -ForegroundColor Red
        Write-Host $restoreOutput -ForegroundColor Red
        throw "Restore failed with exit code $LASTEXITCODE"
    }
    Write-Host "✓ NuGet packages restored successfully" -ForegroundColor Green
} catch {
    Write-Error "Failed to restore packages: $_"
    Write-Host "`nTrying to restore with more details..." -ForegroundColor Yellow
    if ($useSolution) {
        dotnet restore $buildTarget --verbosity detailed
    } else {
        dotnet restore --verbosity detailed
    }
    exit 1
}

# Build the application first
Write-Host "`n🔨 Building application..." -ForegroundColor Yellow
try {
    if ($useSolution) {
        $buildOutput = dotnet build $buildTarget -c Release --no-restore --verbosity minimal 2>&1
    } else {
        $buildOutput = dotnet build -c Release --no-restore --verbosity minimal 2>&1
    }
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build output:" -ForegroundColor Red
        Write-Host $buildOutput -ForegroundColor Red
        throw "Build failed with exit code $LASTEXITCODE"
    }
    Write-Host "✓ Application built successfully" -ForegroundColor Green
} catch {
    Write-Error "Failed to build application: $_"
    Write-Host "`nTrying to build with more details..." -ForegroundColor Yellow
    if ($useSolution) {
        dotnet build $buildTarget -c Release --verbosity detailed
    } else {
        dotnet build -c Release --verbosity detailed
    }
    exit 1
}

# Publish the application
Write-Host "`n📦 Publishing application..." -ForegroundColor Yellow
try {
    $publishOutput = dotnet publish $projectFile -c Release -o $publishFolder --no-build --verbosity minimal 2>&1
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Publish output:" -ForegroundColor Red
        Write-Host $publishOutput -ForegroundColor Red
        throw "Publish failed with exit code $LASTEXITCODE"
    }
    Write-Host "✓ Application published successfully" -ForegroundColor Green
} catch {
    Write-Error "Failed to publish application: $_"
    Write-Host "`nTrying to publish with more details..." -ForegroundColor Yellow
    dotnet publish $projectFile -c Release -o $publishFolder --verbosity detailed
    exit 1
}

# Verify published files exist
$dllPath = Join-Path $publishFolder "ArzTi3Server.dll"
if (!(Test-Path $dllPath)) {
    Write-Error "Published DLL not found at $dllPath. Check publish output above."
    Write-Host "Contents of publish folder:" -ForegroundColor Yellow
    if (Test-Path $publishFolder) {
        Get-ChildItem $publishFolder -Recurse | Format-Table Name, Length, Directory
    } else {
        Write-Host "Publish folder does not exist!" -ForegroundColor Red
    }
    exit 1
}
Write-Host "✓ Published DLL verified: $dllPath" -ForegroundColor Green

# List published files
Write-Host "`n📋 Published files:" -ForegroundColor Yellow
Get-ChildItem $publishFolder | Select-Object Name, Length | Format-Table

# Create destination directory if it doesn't exist
if (!(Test-Path $PhysicalPath)) {
    New-Item -ItemType Directory -Path $PhysicalPath -Force | Out-Null
    Write-Host "✓ Created directory: $PhysicalPath" -ForegroundColor Green
}

# Copy published files
Write-Host "`n📁 Copying files to IIS directory..." -ForegroundColor Yellow
try {
    Copy-Item -Path "$publishFolder/*" -Destination $PhysicalPath -Recurse -Force
    Write-Host "✓ Files copied successfully" -ForegroundColor Green
} catch {
    Write-Error "Failed to copy files: $_"
    exit 1
}

# Verify key files exist in destination
$destDllPath = Join-Path $PhysicalPath "ArzTi3Server.dll"
$webConfigPath = Join-Path $PhysicalPath "web.config"

if (!(Test-Path $destDllPath)) {
    Write-Error "ArzTi3Server.dll not found in destination: $destDllPath"
    Write-Host "Files in destination:" -ForegroundColor Yellow
    Get-ChildItem $PhysicalPath | Format-Table Name, Length
    exit 1
}

if (!(Test-Path $webConfigPath)) {
    Write-Warning "web.config not found in destination. Creating one..."
    Write-Host "Creating basic web.config..." -ForegroundColor Yellow
    
    $webConfigContent = @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <location path="." inheritInChildApplications="false">
    <system.webServer>
      <handlers>
        <add name="aspNetCore" path="*" verb="*" modules="AspNetCoreModuleV2" resourceType="Unspecified" />
      </handlers>
      <aspNetCore processPath="dotnet" 
                  arguments=".\ArzTi3Server.dll" 
                  stdoutLogEnabled="true" 
                  stdoutLogFile=".\logs\stdout" 
                  hostingModel="inprocess"
                  forwardWindowsAuthToken="false">
        <environmentVariables>
          <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Production" />
          <environmentVariable name="ASPNETCORE_DETAILEDERRORS" value="true" />
        </environmentVariables>
      </aspNetCore>
      <httpErrors errorMode="Detailed" />
    </system.webServer>
  </location>
</configuration>
"@
    
    $webConfigContent | Out-File -FilePath $webConfigPath -Encoding UTF8
    Write-Host "✓ web.config created" -ForegroundColor Green
}

Write-Host "✓ Key files verified in destination" -ForegroundColor Green

# Create Application Pool
if (!(Get-IISAppPool -Name $AppPoolName -ErrorAction SilentlyContinue)) {
    Write-Host "`n🏊 Creating application pool: $AppPoolName" -ForegroundColor Yellow
    New-WebAppPool -Name $AppPoolName -Force
    Set-ItemProperty -Path "IIS:\AppPools\$AppPoolName" -Name managedRuntimeVersion -Value ""
    Set-ItemProperty -Path "IIS:\AppPools\$AppPoolName" -Name enable32BitAppOnWin64 -Value $false
    Set-ItemProperty -Path "IIS:\AppPools\$AppPoolName" -Name processModel.identityType -Value ApplicationPoolIdentity
    Write-Host "✓ Created application pool: $AppPoolName" -ForegroundColor Green
} else {
    Write-Host "✓ Application pool already exists: $AppPoolName" -ForegroundColor Green
}

# Create IIS Application
try {
    Write-Host "`n🌐 Creating web application: $SiteName" -ForegroundColor Yellow
    New-WebApplication -Name $SiteName -Site "Default Web Site" -PhysicalPath $PhysicalPath -ApplicationPool $AppPoolName -Force
    Write-Host "✓ Created web application: $SiteName" -ForegroundColor Green
} catch {
    Write-Error "Failed to create web application: $_"
    exit 1
}

# Create logs directory
$logsPath = Join-Path $PhysicalPath "logs"
if (!(Test-Path $logsPath)) {
    New-Item -ItemType Directory -Path $logsPath -Force | Out-Null
    Write-Host "✓ Created logs directory: $logsPath" -ForegroundColor Green
}

# Set permissions
Write-Host "`n🔐 Setting permissions..." -ForegroundColor Yellow
try {
    & icacls $PhysicalPath /grant "IIS_IUSRS:(OI)(CI)RX" /T /Q 2>$null
    & icacls $PhysicalPath /grant "IIS APPPOOL\${AppPoolName}:(OI)(CI)F" /T /Q 2>$null
    & icacls $logsPath /grant "IIS APPPOOL\${AppPoolName}:(OI)(CI)F" /T /Q 2>$null
    Write-Host "✓ Permissions set successfully" -ForegroundColor Green
} catch {
    Write-Warning "Failed to set some permissions: $_"
}

# Start application pool
try {
    Write-Host "`n▶️ Starting application pool: $AppPoolName" -ForegroundColor Yellow
    Start-WebAppPool -Name $AppPoolName
    Write-Host "✓ Started application pool: $AppPoolName" -ForegroundColor Green
} catch {
    Write-Error "Failed to start application pool: $_"
    exit 1
}

# Wait for application pool to start
Write-Host "⏳ Waiting for application pool to initialize..." -ForegroundColor Yellow
Start-Sleep -Seconds 5

# Check application pool status
$poolStatus = (Get-IISAppPool -Name $AppPoolName).State
Write-Host "Application Pool Status: $poolStatus" -ForegroundColor $(if ($poolStatus -eq "Started") { "Green" } else { "Red" })

# Test the application (using HTTP instead of HTTPS to avoid SSL issues)
Write-Host "`n🧪 Testing application..." -ForegroundColor Yellow
$testUrl = "http://localhost/$SiteName"

try {
    # Try HTTP first
    $response = Invoke-WebRequest -Uri $testUrl -UseBasicParsing -TimeoutSec 30 -ErrorAction Stop
    Write-Host "✓ Application is responding via HTTP (Status: $($response.StatusCode))" -ForegroundColor Green
    
    # Try to get a snippet of the response
    if ($response.Content.Length -gt 0) {
        Write-Host "✓ Response received (Content length: $($response.Content.Length) bytes)" -ForegroundColor Green
    }
    
} catch {
    Write-Warning "HTTP test failed: $($_.Exception.Message)"
    
    # Check if it's trying to redirect to HTTPS
    if ($_.Exception.Message -like "*SSL*" -or $_.Exception.Message -like "*TLS*") {
        Write-Host "ℹ️ SSL/TLS issue detected. Testing without SSL validation..." -ForegroundColor Yellow
        
        try {
            # Bypass SSL validation for testing
            [System.Net.ServicePointManager]::ServerCertificateValidationCallback = {$true}
            $response = Invoke-WebRequest -Uri $testUrl -UseBasicParsing -TimeoutSec 30 -ErrorAction Stop
            Write-Host "✓ Application is responding (bypassing SSL validation)" -ForegroundColor Green
        } catch {
            Write-Warning "Still failed even bypassing SSL: $($_.Exception.Message)"
        }
    }
    
    # Check Windows Event Viewer for detailed errors
    Write-Host "`n🔍 Checking Windows Event Logs for application errors..." -ForegroundColor Yellow
    try {
        $recentErrors = Get-WinEvent -LogName Application -MaxEvents 10 -ErrorAction SilentlyContinue | 
                       Where-Object { $_.LevelDisplayName -eq "Error" -and $_.TimeCreated -gt (Get-Date).AddMinutes(-5) }
        
        if ($recentErrors) {
            Write-Host "Recent application errors found:" -ForegroundColor Yellow
            $recentErrors | ForEach-Object {
                Write-Host "  - $($_.TimeCreated): $($_.LevelDisplayName) - $($_.Message)" -ForegroundColor Red
            }
        } else {
            Write-Host "✓ No recent application errors found in Event Log" -ForegroundColor Green
        }
    } catch {
        Write-Warning "Could not check Event Log: $_"
    }
    
    # Check stdout logs
    Write-Host "`n📝 Checking stdout logs..." -ForegroundColor Yellow
    $stdoutLogs = Get-ChildItem "$logsPath\stdout*.log" -ErrorAction SilentlyContinue | Sort-Object LastWriteTime -Descending | Select-Object -First 1
    if ($stdoutLogs) {
        Write-Host "Latest stdout log: $($stdoutLogs.FullName)" -ForegroundColor Cyan
        try {
            $logContent = Get-Content $stdoutLogs.FullName -Tail 20 -ErrorAction SilentlyContinue
            if ($logContent) {
                Write-Host "Last 20 lines of stdout log:" -ForegroundColor Yellow
                $logContent | ForEach-Object { Write-Host "  $_" -ForegroundColor White }
            }
        } catch {
            Write-Warning "Could not read stdout log: $_"
        }
    } else {
        Write-Host "ℹ️ No stdout logs found yet" -ForegroundColor Yellow
    }
}

# Display IIS application status
Write-Host "`n📊 IIS Configuration Summary:" -ForegroundColor Yellow
try {
    $app = Get-WebApplication -Name $SiteName -ErrorAction SilentlyContinue
    if ($app) {
        Write-Host "Application: $($app.Path)" -ForegroundColor Cyan
        Write-Host "Physical Path: $($app.PhysicalPath)" -ForegroundColor Cyan
        Write-Host "Application Pool: $($app.ApplicationPool)" -ForegroundColor Cyan
        
        $appPool = Get-IISAppPool -Name $app.ApplicationPool -ErrorAction SilentlyContinue
        if ($appPool) {
            Write-Host "App Pool State: $($appPool.State)" -ForegroundColor Cyan
            Write-Host "App Pool .NET Version: $($appPool.ManagedRuntimeVersion)" -ForegroundColor Cyan
        }
    }
} catch {
    Write-Warning "Could not retrieve IIS configuration: $_"
}

Write-Host "`n✅ Deployment process completed!" -ForegroundColor Green
Write-Host "🌐 Application URL: $testUrl" -ForegroundColor Cyan
Write-Host "📋 Swagger URL: $testUrl/swagger" -ForegroundColor Cyan
Write-Host "📝 Logs directory: $logsPath" -ForegroundColor Cyan

Write-Host "`n🔧 Next steps if application is not working:" -ForegroundColor Yellow
Write-Host "1. Check the application URL in your browser: $testUrl" -ForegroundColor Cyan
Write-Host "2. Check Windows Event Viewer > Application logs for detailed errors" -ForegroundColor Cyan
Write-Host "3. Review stdout logs in: $logsPath" -ForegroundColor Cyan
Write-Host "4. Verify .NET 6 Hosting Bundle is installed (required for ASP.NET Core)" -ForegroundColor Cyan
Write-Host "5. Ensure PostgreSQL server is accessible from this machine" -ForegroundColor Cyan

# Return to original location
Write-Host "`nScript completed from directory: $(Get-Location)" -ForegroundColor Cyan