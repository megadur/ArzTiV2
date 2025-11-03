# Diagnostic script for IIS application issues
param(
    [string]$SiteName = "ArzTi3",
    [string]$AppPoolName = "ArzTi3AppPool",
    [string]$PhysicalPath = "C:\inetpub\wwwroot\ArzTi3"
)

Write-Host "🔍 Diagnosing IIS Application: $SiteName" -ForegroundColor Green

# Check if running as administrator
$currentUser = [Security.Principal.WindowsIdentity]::GetCurrent()
$principal = New-Object Security.Principal.WindowsPrincipal($currentUser)
$isAdmin = $principal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
Write-Host "Administrator privileges: $isAdmin" -ForegroundColor $(if ($isAdmin) { "Green" } else { "Red" })

# Import WebAdministration module
try {
    Import-Module WebAdministration -ErrorAction Stop
    Write-Host "✓ WebAdministration module loaded" -ForegroundColor Green
} catch {
    Write-Error "Failed to load WebAdministration module: $_"
    exit 1
}

Write-Host "`n📊 IIS Configuration Check:" -ForegroundColor Yellow

# Check if Default Web Site is running
$defaultSite = Get-Website "Default Web Site" -ErrorAction SilentlyContinue
if ($defaultSite) {
    Write-Host "Default Web Site Status: $($defaultSite.State)" -ForegroundColor $(if ($defaultSite.State -eq "Started") { "Green" } else { "Red" })
    Write-Host "Default Web Site Bindings:" -ForegroundColor Cyan
    $defaultSite.Bindings.Collection | ForEach-Object {
        Write-Host "  - Protocol: $($_.Protocol), Binding: $($_.BindingInformation)" -ForegroundColor White
    }
} else {
    Write-Host "❌ Default Web Site not found!" -ForegroundColor Red
}

# Check application pool
$appPool = Get-IISAppPool -Name $AppPoolName -ErrorAction SilentlyContinue
if ($appPool) {
    Write-Host "`nApplication Pool '$AppPoolName':" -ForegroundColor Yellow
    Write-Host "  State: $($appPool.State)" -ForegroundColor $(if ($appPool.State -eq "Started") { "Green" } else { "Red" })
    Write-Host "  .NET Version: $($appPool.ManagedRuntimeVersion)" -ForegroundColor Cyan
    Write-Host "  Identity: $($appPool.ProcessModel.IdentityType)" -ForegroundColor Cyan
    Write-Host "  32-bit: $($appPool.Enable32BitAppOnWin64)" -ForegroundColor Cyan
} else {
    Write-Host "❌ Application Pool '$AppPoolName' not found!" -ForegroundColor Red
}

# Check web application
$webApp = Get-WebApplication -Name $SiteName -ErrorAction SilentlyContinue
if ($webApp) {
    Write-Host "`nWeb Application '$SiteName':" -ForegroundColor Yellow
    Write-Host "  Path: $($webApp.Path)" -ForegroundColor Cyan
    Write-Host "  Physical Path: $($webApp.PhysicalPath)" -ForegroundColor Cyan
    Write-Host "  Application Pool: $($webApp.ApplicationPool)" -ForegroundColor Cyan
} else {
    Write-Host "❌ Web Application '$SiteName' not found!" -ForegroundColor Red
}

# Check physical files
Write-Host "`n📁 Physical Files Check:" -ForegroundColor Yellow
if (Test-Path $PhysicalPath) {
    Write-Host "✓ Physical path exists: $PhysicalPath" -ForegroundColor Green
    
    # Check key files
    $keyFiles = @("ArzTi3Server.dll", "web.config", "appsettings.json", "appsettings.Production.json")
    foreach ($file in $keyFiles) {
        $filePath = Join-Path $PhysicalPath $file
        if (Test-Path $filePath) {
            $fileInfo = Get-Item $filePath
            Write-Host "  ✓ $file (Size: $($fileInfo.Length) bytes, Modified: $($fileInfo.LastWriteTime))" -ForegroundColor Green
        } else {
            Write-Host "  ❌ $file - Missing!" -ForegroundColor Red
        }
    }
    
    # Check logs directory
    $logsPath = Join-Path $PhysicalPath "logs"
    if (Test-Path $logsPath) {
        Write-Host "  ✓ Logs directory exists" -ForegroundColor Green
        $logFiles = Get-ChildItem $logsPath -Filter "stdout*.log" -ErrorAction SilentlyContinue
        if ($logFiles) {
            Write-Host "  Found $($logFiles.Count) log file(s):" -ForegroundColor Cyan
            $logFiles | Sort-Object LastWriteTime -Descending | Select-Object -First 3 | ForEach-Object {
                Write-Host "    - $($_.Name) (Modified: $($_.LastWriteTime))" -ForegroundColor White
            }
        } else {
            Write-Host "  ⚠️ No stdout log files found" -ForegroundColor Yellow
        }
    } else {
        Write-Host "  ❌ Logs directory missing!" -ForegroundColor Red
    }
} else {
    Write-Host "❌ Physical path does not exist: $PhysicalPath" -ForegroundColor Red
}

# Check web.config content
$webConfigPath = Join-Path $PhysicalPath "web.config"
if (Test-Path $webConfigPath) {
    Write-Host "`n📄 web.config Content:" -ForegroundColor Yellow
    try {
        $webConfigContent = Get-Content $webConfigPath -Raw
        Write-Host $webConfigContent -ForegroundColor White
    } catch {
        Write-Host "❌ Could not read web.config: $_" -ForegroundColor Red
    }
} else {
    Write-Host "❌ web.config not found!" -ForegroundColor Red
}

# Check .NET installation
Write-Host "`n🔍 .NET Runtime Check:" -ForegroundColor Yellow
try {
    $runtimes = dotnet --list-runtimes
    Write-Host ".NET Runtimes installed:" -ForegroundColor Cyan
    $runtimes | ForEach-Object { Write-Host "  $_" -ForegroundColor White }
    
    # Check specifically for ASP.NET Core 6.x
    $aspNetCore6 = $runtimes | Where-Object { $_ -like "*Microsoft.AspNetCore.App 6.*" }
    if ($aspNetCore6) {
        Write-Host "✓ ASP.NET Core 6.x runtime found" -ForegroundColor Green
    } else {
        Write-Host "❌ ASP.NET Core 6.x runtime NOT found!" -ForegroundColor Red
        Write-Host "  You need to install the .NET 6 Hosting Bundle" -ForegroundColor Yellow
    }
} catch {
    Write-Host "❌ Could not check .NET runtimes: $_" -ForegroundColor Red
}

# Check Windows Event Log for recent errors
Write-Host "`n📋 Recent Windows Event Log Errors:" -ForegroundColor Yellow
try {
    $recentErrors = Get-WinEvent -LogName Application -MaxEvents 20 -ErrorAction SilentlyContinue | 
                   Where-Object { 
                       $_.LevelDisplayName -eq "Error" -and 
                       $_.TimeCreated -gt (Get-Date).AddMinutes(-10) -and
                       ($_.ProviderName -like "*ASP.NET*Core*" -or $_.ProviderName -like "*IIS*" -or $_.Message -like "*$SiteName*")
                   }
    
    if ($recentErrors) {
        Write-Host "Found $($recentErrors.Count) recent relevant error(s):" -ForegroundColor Red
        $recentErrors | ForEach-Object {
            Write-Host "  Time: $($_.TimeCreated)" -ForegroundColor Cyan
            Write-Host "  Source: $($_.ProviderName)" -ForegroundColor Cyan
            Write-Host "  Message: $($_.Message)" -ForegroundColor Red
            Write-Host "  ---" -ForegroundColor Gray
        }
    } else {
        Write-Host "✓ No recent relevant errors found in Event Log" -ForegroundColor Green
    }
} catch {
    Write-Host "⚠️ Could not check Event Log: $_" -ForegroundColor Yellow
}

# Check latest stdout log
Write-Host "`n📝 Latest Stdout Log:" -ForegroundColor Yellow
$logsPath = Join-Path $PhysicalPath "logs"
if (Test-Path $logsPath) {
    $latestLog = Get-ChildItem $logsPath -Filter "stdout*.log" -ErrorAction SilentlyContinue | 
                 Sort-Object LastWriteTime -Descending | 
                 Select-Object -First 1
    
    if ($latestLog) {
        Write-Host "Latest log file: $($latestLog.Name)" -ForegroundColor Cyan
        try {
            $logContent = Get-Content $latestLog.FullName -ErrorAction SilentlyContinue
            if ($logContent) {
                Write-Host "Log content:" -ForegroundColor White
                $logContent | ForEach-Object { Write-Host "  $_" -ForegroundColor Gray }
            } else {
                Write-Host "  Log file is empty" -ForegroundColor Yellow
            }
        } catch {
            Write-Host "❌ Could not read log file: $_" -ForegroundColor Red
        }
    } else {
        Write-Host "ℹ️ No stdout log files found" -ForegroundColor Yellow
    }
}

# Network connectivity test
Write-Host "`n🌐 Network Connectivity Test:" -ForegroundColor Yellow
$testUrls = @(
    "http://localhost/$SiteName",
    "http://127.0.0.1/$SiteName"
)

foreach ($url in $testUrls) {
    Write-Host "Testing: $url" -ForegroundColor Cyan
    try {
        # Use simpler method that doesn't require SSL
        $request = [System.Net.WebRequest]::Create($url)
        $request.Method = "HEAD"
        $request.Timeout = 10000
        $response = $request.GetResponse()
        
        Write-Host "  ✓ Response: $($response.StatusCode) $($response.StatusDescription)" -ForegroundColor Green
        $response.Close()
    } catch [System.Net.WebException] {
        $webEx = $_.Exception
        if ($webEx.Response) {
            $statusCode = $webEx.Response.StatusCode
            Write-Host "  ⚠️ HTTP Error: $statusCode" -ForegroundColor Yellow
        } else {
            Write-Host "  ❌ Connection Error: $($webEx.Message)" -ForegroundColor Red
        }
    } catch {
        Write-Host "  ❌ Error: $($_.Exception.Message)" -ForegroundColor Red
    }
}

# Port listening check
Write-Host "`n🔌 Port Listening Check:" -ForegroundColor Yellow
try {
    $listeningPorts = netstat -an | Select-String ":80\s" | Select-String "LISTENING"
    if ($listeningPorts) {
        Write-Host "Port 80 listening status:" -ForegroundColor Cyan
        $listeningPorts | ForEach-Object { Write-Host "  $_" -ForegroundColor White }
    } else {
        Write-Host "❌ Port 80 is not listening!" -ForegroundColor Red
    }
} catch {
    Write-Host "⚠️ Could not check port status: $_" -ForegroundColor Yellow
}

Write-Host "`n✅ Diagnosis completed!" -ForegroundColor Green
Write-Host "🔧 Recommended next steps:" -ForegroundColor Yellow
Write-Host "1. If ASP.NET Core 6.x runtime is missing, install .NET 6 Hosting Bundle" -ForegroundColor Cyan
Write-Host "2. Check the stdout logs for application startup errors" -ForegroundColor Cyan
Write-Host "3. Verify the connection string in appsettings.Production.json" -ForegroundColor Cyan
Write-Host "4. Ensure Default Web Site is started" -ForegroundColor Cyan
Write-Host "5. Try accessing the application directly in a web browser" -ForegroundColor Cyan