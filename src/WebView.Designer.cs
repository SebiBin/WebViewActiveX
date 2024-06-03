namespace WebViewActiveX
{
    partial class WebView
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod wygenerowany przez Projektanta składników

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować 
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.WebViewControl = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.WebViewControl)).BeginInit();
            this.SuspendLayout();
            // 
            // WebViewControl
            // 
            this.WebViewControl.AllowExternalDrop = true;
            this.WebViewControl.BackColor = System.Drawing.Color.White;
            this.WebViewControl.CreationProperties = null;
            this.WebViewControl.DefaultBackgroundColor = System.Drawing.Color.White;
            this.WebViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebViewControl.Location = new System.Drawing.Point(0, 0);
            this.WebViewControl.Name = "WebViewControl";
            this.WebViewControl.Size = new System.Drawing.Size(800, 450);
            this.WebViewControl.TabIndex = 0;
            this.WebViewControl.ZoomFactor = 1D;
            // 
            // WebView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.WebViewControl);
            this.Name = "WebView";
            this.Size = new System.Drawing.Size(800, 450);
            ((System.ComponentModel.ISupportInitialize)(this.WebViewControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 WebViewControl;
    }
}
