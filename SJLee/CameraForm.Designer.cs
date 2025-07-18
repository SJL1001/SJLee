namespace SJLee
{
    partial class CameraForm
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
            this.imagePanel = new System.Windows.Forms.Panel();
            this.picMainview = new System.Windows.Forms.PictureBox();
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMainview)).BeginInit();
            this.SuspendLayout();
            // 
            // imagePanel
            // 
            this.imagePanel.Controls.Add(this.picMainview);
            this.imagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imagePanel.Location = new System.Drawing.Point(0, 0);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(1205, 808);
            this.imagePanel.TabIndex = 1;
            // 
            // picMainview
            // 
            this.picMainview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picMainview.Location = new System.Drawing.Point(0, 0);
            this.picMainview.Name = "picMainview";
            this.picMainview.Size = new System.Drawing.Size(1205, 808);
            this.picMainview.TabIndex = 0;
            this.picMainview.TabStop = false;
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 808);
            this.Controls.Add(this.imagePanel);
            this.Name = "CameraForm";
            this.Text = "CameraForm";
            this.imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMainview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.PictureBox picMainview;
    }
}