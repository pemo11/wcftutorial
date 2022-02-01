// File: Program.cs

using System;
using System.ServiceModel;
using System.Runtime.InteropServices;
using WCFDokServiceV1;
using WCFDokServiceV1.DAL;

namespace WCFDokServiceHostV1
{
    class Program
    {
        private static string logMessage;

        static void Main(string[] args)
        {
            // PM: Nur provisorisch
            logMessage = $"### Config-Pfad={RuntimeEnvironment.SystemConfigurationFile} ###";
            LogHelper.LogInfo(logMessage);

            // bei typeof() wird die Klasse angegeben, die das Interface implementiert - ansonsten kann keine Host-Adresse abgerufen werden
            using (ServiceHost host = new ServiceHost(typeof(DokumentServiceV1)))
            {
                if (host.BaseAddresses.Count > 0)
                {
                    string hostAdr = host.BaseAddresses[0].AbsoluteUri.ToString();
                    host.Open();
                    logMessage = $"*** Der Host ist bereit unter der Adresse {hostAdr} ***";
                    LogHelper.LogInfo(logMessage);
                    Console.ReadLine();
                    NLog.LogManager.Shutdown();
                    host.Close();
                }
                else
                {
                    logMessage = "!!! Es konnte keine Host-Adresse abgerufen werden !!!";
                    LogHelper.LogInfo(logMessage);
                    Console.ReadLine();
                }
            }
        }
    }
}
