﻿namespace SJLee
{
    partial class PathSetting
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
            this.lbModelDir = new System.Windows.Forms.Label();
            this.lbImageDir = new System.Windows.Forms.Label();
            this.txtModelDir = new System.Windows.Forms.TextBox();
            this.txtImageDir = new System.Windows.Forms.TextBox();
            this.btnSelModelDir = new System.Windows.Forms.Button();
            this.btnSelImageDir = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbModelDir
            // 
            this.lbModelDir.AutoSize = true;
            this.lbModelDir.Location = new System.Drawing.Point(3, 35);
            this.lbModelDir.Name = "lbModelDir";
            this.lbModelDir.Size = new System.Drawing.Size(86, 18);
            this.lbModelDir.TabIndex = 0;
            this.lbModelDir.Text = "모델 경로";
            // 
            // lbImageDir
            // 
            this.lbImageDir.AutoSize = true;
            this.lbImageDir.Location = new System.Drawing.Point(3, 83);
            this.lbImageDir.Name = "lbImageDir";
            this.lbImageDir.Size = new System.Drawing.Size(98, 18);
            this.lbImageDir.TabIndex = 1;
            this.lbImageDir.Text = "이미지경로";
            // 
            // txtModelDir
            // 
            this.txtModelDir.Location = new System.Drawing.Point(107, 25);
            this.txtModelDir.Name = "txtModelDir";
            this.txtModelDir.Size = new System.Drawing.Size(220, 28);
            this.txtModelDir.TabIndex = 2;
            // 
            // txtImageDir
            // 
            this.txtImageDir.Location = new System.Drawing.Point(107, 80);
            this.txtImageDir.Name = "txtImageDir";
            this.txtImageDir.Size = new System.Drawing.Size(220, 28);
            this.txtImageDir.TabIndex = 3;
            // 
            // btnSelModelDir
            // 
            this.btnSelModelDir.Location = new System.Drawing.Point(361, 32);
            this.btnSelModelDir.Name = "btnSelModelDir";
            this.btnSelModelDir.Size = new System.Drawing.Size(64, 28);
            this.btnSelModelDir.TabIndex = 4;
            this.btnSelModelDir.Text = "...";
            this.btnSelModelDir.UseVisualStyleBackColor = true;
            this.btnSelModelDir.Click += new System.EventHandler(this.btnSelModelDir_Click);
            // 
            // btnSelImageDir
            // 
            this.btnSelImageDir.Location = new System.Drawing.Point(361, 83);
            this.btnSelImageDir.Name = "btnSelImageDir";
            this.btnSelImageDir.Size = new System.Drawing.Size(64, 28);
            this.btnSelImageDir.TabIndex = 5;
            this.btnSelImageDir.Text = "...";
            this.btnSelImageDir.UseVisualStyleBackColor = true;
            this.btnSelImageDir.Click += new System.EventHandler(this.btnSelImageDir_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(327, 137);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 36);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "적용";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // PathSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnSelImageDir);
            this.Controls.Add(this.btnSelModelDir);
            this.Controls.Add(this.txtImageDir);
            this.Controls.Add(this.txtModelDir);
            this.Controls.Add(this.lbImageDir);
            this.Controls.Add(this.lbModelDir);
            this.Name = "PathSetting";
            this.Size = new System.Drawing.Size(552, 298);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbModelDir;
        private System.Windows.Forms.Label lbImageDir;
        private System.Windows.Forms.TextBox txtModelDir;
        private System.Windows.Forms.TextBox txtImageDir;
        private System.Windows.Forms.Button btnSelModelDir;
        private System.Windows.Forms.Button btnSelImageDir;
        private System.Windows.Forms.Button btnApply;
    }
}
