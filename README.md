# Ein kleines WCF-Tutorial (basierend auf dem .NET Framework 4.x)
Enthält ein kleines WCF-Projekt bestehend aus:

- WCF-Service (WCFDokServiceV1)
- WCF-Service-Host als Konsolenanwendung (WCFDokServiceHostV1)
- WCF-GUI-Client als WinForms-Anwendung (WCFDokServerV1GUIClient)
- Ein Test-Projekt für den WCF-Service mit einigen Tests

Inzwischen (01/02/22) habe ich auch ein auf Https basierendes Projekt hinzugefügt (WCFDocServiceV2HttpsClient) und eine Reihe von PowerShell-Skripten (mit Stapeldateien würde es genauso gehen), über die u.a. ein Zertifikat angelegt und per netsh registriert wird.

Alles hat beim Testen funktioniert, was grundsätzlich bedeutet, dass es so geht.

Geladen werden alle Projekte über die Sln-Datei WCFDokServiceV1.sln

Alle Projekte lassen sich ausführen mit .NET Framework 4.62 ausführen. Es gibt keine weiteren Abhängigkeiten.

Für das Ausführen des WCF-GUI-Clients muss Visual Studio entweder ein weiteres Mal gestartet werden, oder der WCF-Service-Host *WCFDokServiceHostV1.exe* wird z.B. über die Befehlszeile gestartet.

Bei Fragen bitte kurze Mail an info@activetraining.de

