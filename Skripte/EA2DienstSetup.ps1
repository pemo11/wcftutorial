<#
 .Synopsis
Einrichten des EA2-Webservices
 .Notes
 Der Dienst wird auf "manuell" starten gesetzt - eine Alternative ist "Automatic"
#>

$ExePath = "D:\GitRepos\e2a.webservice\bin\Debug\E2A_EF_Webservice.exe"
$ExePath = "D:\EurekaFach\WCF-Training\wcftutorial\WCFDocServiceV2\bin\Debug\WCFDocServiceV2.exe"
$Servicename = "WCFDocService"
$DisplayName = "Der WCF-Document-Service"
$Description = "Ein weiterer Test-Service zu WCF"
$AdminUsername = "powerpc\pemo20"
$AdminUserPw = "demo+123"

# Den Dienst zuerst entfernen, sofern vorhanden
$Ps1Pfad = Join-Path -Path $PSScriptRoot -ChildPath "DienstEntfernen.ps1"
.$Ps1Pfad -ServiceName $Servicename

# LASTEXITCODE enthält den Wert, mit dem das Skript per Exit beendet wurde oder 0
# Wurde es regulär beendet oder weil der Dienst nicht vorhanden ist?
if ($LASTEXITCODE -eq 0 -or $LASTEXITCODE -eq -2)
{
    # Dieses Skript richtet den Dienst ein
    $Ps1Pfad = Join-Path -Path $PSScriptRoot -ChildPath "DienstEinrichten.ps1"
    .$Ps1Pfad -ServiceName $Servicename `
     -DisplayName $DisplayName `
     -Description $Description `
     -ExePath $ExePath `
     -Username $AdminUsername  `
     -Password $AdminUserPw

    # Den eingerichteten Dienst zur Kontrolle abfragen
    Get-Service -Name $Servicename
}
else 
{
    Write-Warning "!!! Dienst kann nicht eingerichtet werden - Entfernen des Dienstes endete mit ExitCode=$LASTEXITCODE !!!"
}