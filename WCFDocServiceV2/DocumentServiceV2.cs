// File: DALDB.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Xml.Linq;

using WCFDocServiceV2.DAL;

namespace WCFDocServiceV2
{
    [ServiceBehavior(Namespace = "http://wcf/documents", Name = "WCFDocuments")]
    public class DocumentServiceV2 : IDocumentService
    {
        XDocument xDoc = null;
        string infoMessage = "Keine Meldung";

        public DocumentServiceV2()
        {
            // Die XML-Daten einlesen
            using (StreamReader sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("WCFDocServiceV2.DAL.Documents.xml")))
            {
                xDoc = XDocument.Load(sr);
            }
        }
        public Document GetDocument(int DocId)
        {
            Document document = null;
            try
            {
                document = xDoc.Descendants("document").Where(d => d.Attribute("id").Value == DocId.ToString()).Select(d =>
                 new Document
                 {
                     DocId = Int32.Parse(d.Attribute("id").Value),
                     CreateDate = DateTime.Parse(d.Attribute("createDate").Value),
                     DocAuthor = d.Element("author").Value,
                     DocTitle = d.Element("title").Value,
                     MediaId = (MediaType)Enum.Parse(typeof(MediaType), d.Element("mediaType").Value)
                 }).SingleOrDefault();
                infoMessage = $"*** GetDocument-Abfrage liefert ein Ergebnis: {document != null} ***";
                LogHelper.LogInfo(infoMessage);
                return document;
            }
            catch (SystemException ex)
            {
                infoMessage = $"Fehler in GetDocument ({ex.Message})";
                LogHelper.LogError(infoMessage, ex);
            }
            return document;
        }

        public List<Document> GetDocuments()
        {
            List<Document> documents = new List<Document>();
            try
            {
                var Docs = xDoc.Descendants("document").Select(d =>
                    new Document
                    {
                        DocId = Int32.Parse(d.Attribute("id").Value),
                        CreateDate = DateTime.Parse(d.Attribute("createDate").Value),
                        DocAuthor = d.Element("author").Value,
                        DocTitle = d.Element("title").Value,
                        MediaId = (MediaType)Enum.Parse(typeof(MediaType), d.Element("mediaType").Value)
                    });
                // Sollte sich in die LINQ-Abfrage einbauen lassen
                foreach (var Doc in Docs)
                {
                    documents.Add(Doc);
                }
                infoMessage = $"*** GetDocuments-Abfrage liefert {documents.Count} Dokumente ***";
                LogHelper.LogInfo(infoMessage);
            }

            catch (SystemException ex)
            {
                infoMessage = $"Fehler in GetDocumente ({ex.Message})";
                LogHelper.LogError(infoMessage, ex);
            }
            return documents;
        }

        public List<Document> SearchDocuments(string Filter)
        {
            List<Document> documents = new List<Document>();
            try
            {
                var Docs = xDoc.Descendants("document").Where(d => d.Element("title").Value.Contains(Filter)).Select(d =>
                    new Document
                    {
                        DocId = Int32.Parse(d.Attribute("id").Value),
                        CreateDate = DateTime.Parse(d.Attribute("createDate").Value),
                        DocAuthor = d.Element("author").Value,
                        DocTitle = d.Element("title").Value,
                        MediaId = (MediaType)Enum.Parse(typeof(MediaType), d.Element("mediaType").Value)
                    });
                // Sollte sich in die LINQ-Abfrage einbauen lassen
                foreach (var Doc in Docs)
                {
                    documents.Add(Doc);
                }
                infoMessage = $"*** SearchDocuments-Abfrage liefert {documents.Count} Dokumente ***";
                LogHelper.LogInfo(infoMessage);

            }
            catch (SystemException ex)
            {
                infoMessage = $"Fehler in SearchDocuments ({ex.Message})";
                LogHelper.LogError(infoMessage, ex);
            }
            return documents;
        }
    }
}
