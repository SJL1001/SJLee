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
            this.imageViewCtrlMain = new SJLee.ImageViewCtrl();
            this.mainViewToolbar = new SJLee.MainViewToolbar();
            this.SuspendLayout();
            // 
            // imageViewCtrlMain
            // 
            this.imageViewCtrlMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageViewCtrlMain.Location = new System.Drawing.Point(0, 0);
            this.imageViewCtrlMain.Name = "imageViewCtrlMain";
            this.imageViewCtrlMain.Size = new System.Drawing.Size(1036, 808);
            this.imageViewCtrlMain.TabIndex = 0;
            this.imageViewCtrlMain.WorkingState = "";
            // 
            // mainViewToolbar
            // 
            this.mainViewToolbar.Dock = System.Windows.Forms.DockStyle.Right;
            this.mainViewToolbar.Location = new System.Drawing.Point(1042, 0);
            this.mainViewToolbar.Name = "mainViewToolbar";
            this.mainViewToolbar.Size = new System.Drawing.Size(163, 808);
            this.mainViewToolbar.TabIndex = 1;
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 808);
            this.Controls.Add(this.mainViewToolbar);
            this.Controls.Add(this.imageViewCtrlMain);
            this.Name = "CameraForm";
            this.Text = "CameraForm";
            this.Resize += new System.EventHandler(this.CameraForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private ImageViewCtrl imageViewCtrlMain;
        private MainViewToolbar mainViewToolbar;
    }
}