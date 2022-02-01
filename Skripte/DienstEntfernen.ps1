<#
 .Synopsis
 Entfernen eines Systemdienstes
 .Notes
 Entfernen kann scheitern, wenn die Dienstekonsole geöffnet ist
#>

[CmdletBinding()]
param([String]$ServiceName)

$ReturnCodes = @{
    9  = "Die Exe-Dienstdatei exitistiert offenbar nicht"
	16 = "Der Dienst wird bereits entfernt"
}

if ($ServiceName -eq "")
{
	write-Warning "!!! Falscher Aufruf - richtig z.B. .\RemoveDienst.ps1 VISEFService"
	Exit -1
}

# Gibt es denn Dienst?
if ($null -eq (Get-Service $ServiceName -ErrorAction Ignore))
{
	Write-Warning "!!! Den Dienst $ServiceName gibt es nicht - er kann daher auch nicht entfernt werden"
	Exit -2

}

Stop-Service $ServiceName -ErrorAction Ignore -Verbose

$Ret = (Get-WmiObject Win32_Service -Filter "Name='$ServiceName'").Delete()
if ($Ret.ReturnValue -eq 0)
{
	Write-Verbose "*** Der Dienst wurde erfolgreich entfernt."
	Exit 0
}
else
{
	$ReturnReason = "Unbekannt"
	if ($ReturnCodes.ContainsKey([Int]$Ret.ReturnValue))
	{
		$ReturnReason = $ReturnCodes[[Int]$Ret.ReturnValue]
	}
	 Write-Warning "!!! Der Dienst konnte nicht entfernt werden - ReturnValue=$($Ret.ReturnValue) - $ReturnReason"
	 Exit $Ret.ReturnValue
}