namespace SJLee
{
    partial class CameraSetting
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelCam = new System.Windows.Forms.Label();
            this.cbCamera = new System.Windows.Forms.ComboBox();
            this.btnApplyCam = new System.Windows.Forms.Button();
            this.lblCurrentType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelCam
            // 
            this.labelCam.AutoSize = true;
            this.labelCam.Location = new System.Drawing.Point(30, 78);
            this.labelCam.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCam.Name = "labelCam";
            this.labelCam.Size = new System.Drawing.Size(62, 18);
            this.labelCam.TabIndex = 0;
            this.labelCam.Text = "카메라";
            // 
            // cbCamera
            // 
            this.cbCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCamera.FormattingEnabled = true;
            this.cbCamera.Location = new System.Drawing.Point(137, 66);
            this.cbCamera.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbCamera.Name = "cbCamera";
            this.cbCamera.Size = new System.Drawing.Size(171, 26);
            this.cbCamera.TabIndex = 1;
            // 
            // btnApplyCam
            // 
            this.btnApplyCam.Location = new System.Drawing.Point(261, 220);
            this.btnApplyCam.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApplyCam.Name = "btnApplyCam";
            this.btnApplyCam.Size = new System.Drawing.Size(107, 34);
            this.btnApplyCam.TabIndex = 2;
            this.btnApplyCam.Text = "적용";
            this.btnApplyCam.UseVisualStyleBackColor = true;
            this.btnApplyCam.Click += new System.EventHandler(this.btnApplyCam_Click);
            // 
            // lblCurrentType
            // 
            this.lblCurrentType.AutoSize = true;
            this.lblCurrentType.Location = new System.Drawing.Point(33, 165);
            this.lblCurrentType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentType.Name = "lblCurrentType";
            this.lblCurrentType.Size = new System.Drawing.Size(62, 18);
            this.lblCurrentType.TabIndex = 3;
            this.lblCurrentType.Text = "타입 : ";
            // 
            // Camera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCurrentType);
            this.Controls.Add(this.btnApplyCam);
            this.Controls.Add(this.cbCamera);
            this.Controls.Add(this.labelCam);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Camera";
            this.Size = new System.Drawing.Size(460, 411);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCam;
        private System.Windows.Forms.ComboBox cbCamera;
        private System.Windows.Forms.Button btnApplyCam;
        private System.Windows.Forms.Label lblCurrentType;
    }
}
