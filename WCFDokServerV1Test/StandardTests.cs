// file: StandardTests.cs

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using WCFDokServiceV1;

namespace WCFDokServerV1Test
{
    [TestClass]
    public class StandardTests
    {
        // Einfacher Test von GetDokumente()
        [TestMethod]
        public void GetDokumenteTest1()
        {
            DokumentServiceV1 dokService = new DokumentServiceV1();
            var result = dokService.GetDokumente();
            Assert.IsTrue(result.Count > 0);
        }

        // Einfacher Test von GetDokument()
        [TestMethod]
        public void GetDokumentTest1()
        {
            DokumentServiceV1 dokService = new DokumentServiceV1();
            int dokId = 1001;
            var result = dokService.GetDokument(dokId);
            Assert.IsTrue(result != null);
        }

        // Einfacher Test von SearchDokument()
        [TestMethod]
        public void SearchDokumentTest1()
        {
            DokumentServiceV1 dokService = new DokumentServiceV1();
            string filter = "DMS";
            var result = dokService.SearchDokumente(filter);
            Assert.IsTrue(result.Count == 2);
        }

    }
}
