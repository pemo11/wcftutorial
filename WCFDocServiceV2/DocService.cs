using System;
using System.ServiceModel;
using System.ServiceProcess;

using WCFDocServiceV2.DAL;

namespace WCFDocServiceV2
{
    public partial class DocService : ServiceBase
    {
        private string infoMessage;
        private ServiceHost host;
        private string hostAddress;

        public DocService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            infoMessage = "*** OnStart-Ereignis ***";
            LogHelper.LogInfo(infoMessage);
            try
            {
                host = new ServiceHost(typeof(DocumentServiceV2));
                if (host.BaseAddresses.Count > 0)
                {
                    hostAddress = host.BaseAddresses[0].AbsoluteUri.ToString();
                    host.Open();
                    infoMessage = $"*** Der Host wurde mit Adresse={hostAddress} geöffnet";
                    LogHelper.LogInfo(infoMessage);
                }
                else
                {
                    infoMessage = "!!! Es konnte keine Host-Adresse abgerufen werden !!!";
                    LogHelper.LogError(infoMessage, null);
                    host.Close();
                }
            }
            catch (SystemException ex)
            {
                infoMessage = $"!!! Fehler beim Öffnen des Host ({ex.Message}) !!!";
                LogHelper.LogError(infoMessage, ex);
            }

        }

        protected override void OnStop()
        {
            infoMessage = "*** OnStop-Ereignis ***";
            LogHelper.LogInfo(infoMessage);
        }
    }
}
