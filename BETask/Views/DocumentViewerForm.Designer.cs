namespace BETask.Views
{
    partial class DocumentViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.docViewer = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // docViewer
            // 
            this.docViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.docViewer.Location = new System.Drawing.Point(0, 0);
            this.docViewer.MinimumSize = new System.Drawing.Size(20, 20);
            this.docViewer.Name = "docViewer";
            this.docViewer.Size = new System.Drawing.Size(1135, 664);
            this.docViewer.TabIndex = 0;
            // 
            // DocumentViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 664);
            this.Controls.Add(this.docViewer);
            this.Name = "DocumentViewerForm";
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DocumentViewerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser docViewer;
    }
}