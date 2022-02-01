<#
 .Synopsis
 URL registrieren und SSL Zertifikat registrieren
 .Notes
 Anlegt 26/01/22 - für die Umstellung des E2A-Webservice auf Https
 Das Zertifikat wurde bereits mit einem andere Skript angelegt
 01/02/22 - Skript läuft fehlerfrei durch
#>

#requires -RunAsAdministrator

$Port = "8010"
# $Uri = "E2A_EF_Webservice.EFService"
$Uri = "WCFDoumentService"
$User = "powerpc\pemo20"

# Funktioniert nur, wenn es die Zuordnung bereits gibt
netsh http delete urlacl url=http://+:$Port/

# Dieser Aufruf funktioniert, wenn der Port nicht bereits vergeben wurde
netsh http add urlacl url=https://+:$Port/$Uri user=$User

netsh http show urlacl | Select-String -SimpleMatch $Uri

# Auflisten aller registrierten Zertifikate
netsh http show sslcert ipport=0.0.0.0:$Port

# Zertifikat vorher entfernen, falls bereits vorhanden (um Fehlermeldung beim Hinzufügen zu vermeiden)
netsh http delete sslcert ipport=0.0.0.0:$Port

# Kann "frei" vergebeben werden
$AppId = "d7d7fd44-5bf0-4b15-9236-c4710ca0a182"

$CertThumb = "535A8B2FB2E6C91D35EF7900BA4F59AC26AB8CB1"

$NshCmd = "netsh http add sslcert ipport=0.0.0.0:$Port certHash=$CertThumb appId=`"{$AppId}`" clientcertnegotiation=enable"

# Dieser Aufruf funktioniert
Invoke-Expression $NshCmd

netsh http show sslcert ipport=0.0.0.0:$Port

# IP-Adresse vorher entfernen, falls bereits vorhanden (um Fehlermeldung beim Hinzufügen zu vermeiden)
netsh http delete iplisten ipaddress=0.0.0.0:$Port

# Dieser Aufruf funktioniert auch
netsh http add iplisten ipaddress=0.0.0.0:$Port