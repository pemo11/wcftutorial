// File: IDocumentService.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

using WCFDokServiceV1.DAL;

namespace WCFDokServiceV1
{

    [ServiceContractAttribute(Namespace = "http://ef/documents",ConfigurationName = "EFDocuments")]
    public interface IDokumentService
    {
        [OperationContract]
        List<Dokument> GetDokumente();

        [OperationContract]
        Dokument GetDokument(int DokId);

        [OperationContract]
        List<Dokument> SearchDokumente(String Filter);



    }

}
