//file: Form1.cs

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using WCFDokServerV1GUIClient.ServiceReference1;

namespace WCFDokServerV1GUIClient
{
    public partial class fmMain : Form
    {
        public fmMain()
        {
            InitializeComponent();
            this.Load += (sender, eventargs) =>
            {
                tbFilter.Text = "DMS";
            };
        }

        private void bnFilterAbfrage_Click(object sender, EventArgs e)
        {
            string filter = tbFilter.Text;
            DokumentServiceClient client = new DokumentServiceClient();
            var result = client.SearchDokumente(filter);
            this.dataGridView1.DataSource = result;
        }

    }
}
