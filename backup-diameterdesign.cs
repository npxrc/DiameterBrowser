namespace DiameterBrowser
{
    partial class DiameterApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiameterApp));
            this.webbrowser = new CefSharp.WinForms.ChromiumWebBrowser();
            this.url = new System.Windows.Forms.TextBox();
            this.navigate = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.forward = new System.Windows.Forms.Button();
            this.refresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // webbrowser
            // 
            this.webbrowser.ActivateBrowserOnCreation = false;
            this.webbrowser.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.webbrowser.Location = new System.Drawing.Point(91, 44);
            this.webbrowser.Name = "webbrowser";
            this.webbrowser.Size = new System.Drawing.Size(727, 444);
            this.webbrowser.TabIndex = 0;
            // 
            // url
            // 
            this.url.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.url.Location = new System.Drawing.Point(637, 1);
            this.url.Name = "url";
            this.url.Size = new System.Drawing.Size(189, 22);
            this.url.TabIndex = 1;
            this.url.Text = "enter a url";
            // 
            // navigate
            // 
            this.navigate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.navigate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.navigate.Location = new System.Drawing.Point(832, 1);
            this.navigate.Name = "navigate";
            this.navigate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.navigate.Size = new System.Drawing.Size(75, 23);
            this.navigate.TabIndex = 2;
            this.navigate.Text = "go";
            this.navigate.UseVisualStyleBackColor = true;
            // 
            // back
            // 
            this.back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.back.Location = new System.Drawing.Point(0, 0);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(20, 20);
            this.back.TabIndex = 3;
            this.back.Text = "<";
            this.back.UseVisualStyleBackColor = true;
            // 
            // forward
            // 
            this.forward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.forward.Location = new System.Drawing.Point(20, 0);
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(20, 20);
            this.forward.TabIndex = 4;
            this.forward.Text = ">";
            this.forward.UseVisualStyleBackColor = true;
            // 
            // refresh
            // 
            this.refresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refresh.Location = new System.Drawing.Point(40, 0);
            this.refresh.Margin = new System.Windows.Forms.Padding(0);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(75, 20);
            this.refresh.TabIndex = 5;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            // 
            // DiameterApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(909, 485);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.forward);
            this.Controls.Add(this.back);
            this.Controls.Add(this.navigate);
            this.Controls.Add(this.url);
            this.Controls.Add(this.webbrowser);
            this.Font = new System.Drawing.Font("Noto Sans", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(320, 420);
            this.Name = "DiameterApp";
            this.Text = "Diameter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CefSharp.WinForms.ChromiumWebBrowser webbrowser;
        private System.Windows.Forms.TextBox url;
        private System.Windows.Forms.Button navigate;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.Button forward;
        private System.Windows.Forms.Button refresh;
    }
}

