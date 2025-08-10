using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BETask.Views
{
    public partial class DocumentViewerForm : Form
    {
        public DocumentViewerForm()
        {
            InitializeComponent();
        }
        public DocumentViewerForm(string url)
        {
            InitializeComponent();
            docViewer.Navigate(url);
        }

        private void DocumentViewerForm_Load(object sender, EventArgs e)
        {
          
        }
    }
}
