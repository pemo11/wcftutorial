<#
 .Synopsis
Einrichten eines Systemdienstes
 .Notes
 Der Dienst wird auf "manuell" starten gesetzt - eine Alternative ist "Automatic"

 Aufruf mit
 .\Diensteinrichten.ps1 -ServiceName VISEFService -DisplayName "VIS-EF-Service" -Description "VIS-Justiz-EF-Anbindung" -ExePath .\VISEFSystemService.exe -Username Admin -Password Adminpw
#>

[CmdletBinding()]
param([String]$Servicename, [String]$DisplayName, [String]$Description, [String]$ExePath, [String]$Username, [String]$Password)

$Params = @{
	Name = $Servicename
	DisplayName = $DisplayName
	Description = $Description
	BinaryPath = $ExePath
	StartupType = "Manual"
}

# Den Systemdienst registrieren
New-Service @params

# Das angegebene Benutzerkonto festlegen
$service = Get-WmiObject -Class Win32_Service -Filter "Name='$ServiceName'"
$ret = $service.change($null,$null,$null,$null,$null,$null,$Username,$Password,$null,$null,$null)
if ($ret.ReturnValue -eq 0)
{
    Start-Service -Name $ServiceName
	if ($?)
	{
		Write-Verbose "*** Die Kontoinformation wurde erfolgreich aktualisiert und Dienst wurde gestartet."
	}
	else
	{
		Write-Warning "!!! Der Dienst konnte nicht gestartet werden $($Error[0])"
	}
}
else
{
    Write-Warning "!!! Die Kontoinformation konnte nicht aktualisiert werden - ReturnValue=$($ret.ReturnValue)"
}

# Optional: EventLog-Quelle registrieren
if([System.Diagnostics.Eventlog]::SourceExists($Servicename) -eq $False)
{
	New-EventLog -LogName Application -Source $Servicename
}
