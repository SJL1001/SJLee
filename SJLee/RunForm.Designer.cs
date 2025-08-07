namespace SJLee
{
    partial class RunForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RunForm));
            this.btnGrab = new System.Windows.Forms.Button();
            this.runImageList = new System.Windows.Forms.ImageList(this.components);
            this.btnStartLive = new System.Windows.Forms.Button();
            this.btnStopLive = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGrab
            // 
            this.btnGrab.ImageIndex = 2;
            this.btnGrab.ImageList = this.runImageList;
            this.btnGrab.Location = new System.Drawing.Point(11, 12);
            this.btnGrab.Name = "btnGrab";
            this.btnGrab.Size = new System.Drawing.Size(126, 46);
            this.btnGrab.TabIndex = 0;
            this.btnGrab.Text = "촬상";
            this.btnGrab.UseVisualStyleBackColor = true;
            this.btnGrab.Click += new System.EventHandler(this.btnGrab_Click);
            // 
            // runImageList
            // 
            this.runImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("runImageList.ImageStream")));
            this.runImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.runImageList.Images.SetKeyName(0, "icons8-live-video-on-30.png");
            this.runImageList.Images.SetKeyName(1, "icons8-search-50.png");
            this.runImageList.Images.SetKeyName(2, "icons8-camera-50.png");
            this.runImageList.Images.SetKeyName(3, "icons8-stop-circled-50.png");
            this.runImageList.Images.SetKeyName(4, "icons8-circled-play-button-50.png");
            this.runImageList.Images.SetKeyName(5, "icons8-pause-50.png");
            // 
            // btnStartLive
            // 
            this.btnStartLive.ImageIndex = 0;
            this.btnStartLive.ImageList = this.runImageList;
            this.btnStartLive.Location = new System.Drawing.Point(11, 82);
            this.btnStartLive.Name = "btnStartLive";
            this.btnStartLive.Size = new System.Drawing.Size(126, 44);
            this.btnStartLive.TabIndex = 1;
            this.btnStartLive.Text = "연속촬영";
            this.btnStartLive.UseVisualStyleBackColor = true;
            this.btnStartLive.Click += new System.EventHandler(this.btnStartLive_Click);
            // 
            // btnStopLive
            // 
            this.btnStopLive.ImageIndex = 5;
            this.btnStopLive.ImageList = this.runImageList;
            this.btnStopLive.Location = new System.Drawing.Point(154, 68);
            this.btnStopLive.Name = "btnStopLive";
            this.btnStopLive.Size = new System.Drawing.Size(74, 58);
            this.btnStopLive.TabIndex = 2;
            this.btnStopLive.Text = "중단";
            this.btnStopLive.UseVisualStyleBackColor = true;
            this.btnStopLive.Click += new System.EventHandler(this.btnStopLive_Click);
            // 
            // btnStart
            // 
            this.btnStart.ImageIndex = 1;
            this.btnStart.ImageList = this.runImageList;
            this.btnStart.Location = new System.Drawing.Point(254, 25);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(90, 74);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "검사";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnStopLive);
            this.Controls.Add(this.btnStartLive);
            this.Controls.Add(this.btnGrab);
            this.Name = "RunForm";
            this.Text = "RunForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGrab;
        private System.Windows.Forms.Button btnStartLive;
        private System.Windows.Forms.Button btnStopLive;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ImageList runImageList;
    }
}