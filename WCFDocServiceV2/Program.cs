// File: Program.cs
using System.ServiceProcess;

namespace WCFDocServiceV2
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new DocService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
