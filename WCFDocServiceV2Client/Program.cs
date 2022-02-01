using System;
using System.Collections.Generic;

namespace WCFDocServiceV2Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WCFService.DocumentServiceClient client = new WCFService.DocumentServiceClient();
            List<WCFService.Document> documents = new List<WCFService.Document>(client.GetDocuments());
            foreach(WCFService.Document document in documents)
            {
                Console.WriteLine(document.DocTitle);
            }
            Console.WriteLine("*** Auftrag ausgeführt ***");
            Console.ReadLine();
        }
    }
}
