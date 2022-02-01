<#
 .Synopsis
 Ausstellen eines selbstsignierten Zertifikats mit Root-CA
 .Notes
 Update 26/01/22 - läuft fehlerfrei durch
#>

$DNSName = "powerpc"
$CertPw = "demo+123"
$PfxPfad = Join-Path -Path $PSScriptRoot -ChildPath "EFPowerPC.pfx"

# Schritt 1: Das Root-CA-Zertifikat erstellen

# Als StoreLocation muss Cert:\LocalMachine\my verwendet werden
# Das CA-Zertifikat muss später nach Cert:\LocalMachine\Root verschoben werden (Vertrauenswürdige Stammzertifizierungstellen)

$Rootcert = New-SelfSignedCertificate -CertStoreLocation Cert:\LocalMachine\My -DnsName "$DNSName-RootCA" `
 -TextExtension @("2.5.29.19={text}CA=true") -KeyUsage CertSign,CrlSign,DigitalSignature

# Schritt 2: Das Zertifikat als Pfx-Datei exportieren

$RootCertPassword = ConvertTo-SecureString -String $CertPw -Force -AsPlainText

$RootCertPath = Join-Path -Path Cert:\LocalMachine\My -ChildPath "$($RootCert.Thumbprint)"

# Sollte nicht erforderlich sein
$RootCertPath = @($RootCertPath)[0]

# Schritt 3: Root-Zertifikat exportieren
Export-PfxCertificate -Cert $RootCertPath -FilePath $PfxPfad -Password $RootcertPassword

# Schritt 4: Ein neues Zertifikat anlegen und mit dem Root CA-Zertifikat signieren

$TestCert = New-SelfSignedCertificate -CertStoreLocation Cert:\LocalMachine\My -DnsName $DNSName -KeyExportPolicy Exportable -KeyLength 2048 `
 -KeyUsage DigitalSignature,KeyEncipherment -Signer $RootCert

$TestCert

# Thumbprint des neuen Zertifikats in die Zwischenablage kopieren
# z.B. 535A8B2FB2E6C91D35EF7900BA4F59AC26AB8CB1
$TestCert.Thumbprint | Set-Clipboard

# Schritt 5: Root-CA nach My\Root verschieben (Vertrauenswürdige Stammzertifizierungsstellen)

Move-Item -Path $RootCertPath -Destination Cert:\LocalMachine\Root -Verbose
