// File: IDocumentService.cs

using System;
using System.Collections.Generic;
using System.ServiceModel;

using WCFDocServiceV2.DAL;

namespace WCFDocServiceV2
{

    [ServiceContractAttribute(Namespace = "http://wcf/documents",ConfigurationName = "WCFDocuments")]
    public interface IDocumentService
    {
        [OperationContract]
        List<Document> GetDocuments();

        [OperationContract]
        Document GetDocument(int DocId);

        [OperationContract]
        List<Document> SearchDocuments(String Filter);



    }

}
