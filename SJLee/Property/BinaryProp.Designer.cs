namespace SJLee
{
    partial class BinaryProp
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
            this.chkUse = new System.Windows.Forms.CheckBox();
            this.grpBinary = new System.Windows.Forms.GroupBox();
            this.cbHighlight = new System.Windows.Forms.ComboBox();
            this.lbHighlight = new System.Windows.Forms.Label();
            this.binRangeTrackbar = new SJLee.RangeTrackbar();
            this.grpBinary.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkUse
            // 
            this.chkUse.Location = new System.Drawing.Point(265, 79);
            this.chkUse.Name = "chkUse";
            this.chkUse.Size = new System.Drawing.Size(104, 24);
            this.chkUse.TabIndex = 6;
            this.chkUse.Text = "검사";
            this.chkUse.CheckedChanged += new System.EventHandler(this.chkUse_CheckedChanged);
            // 
            // grpBinary
            // 
            this.grpBinary.Controls.Add(this.lbHighlight);
            this.grpBinary.Controls.Add(this.cbHighlight);
            this.grpBinary.Controls.Add(this.binRangeTrackbar);
            this.grpBinary.Location = new System.Drawing.Point(45, 129);
            this.grpBinary.Name = "grpBinary";
            this.grpBinary.Size = new System.Drawing.Size(413, 327);
            this.grpBinary.TabIndex = 5;
            this.grpBinary.TabStop = false;
            this.grpBinary.Text = "groupBox1";
            // 
            // cbHighlight
            // 
            this.cbHighlight.FormattingEnabled = true;
            this.cbHighlight.Location = new System.Drawing.Point(122, 219);
            this.cbHighlight.Name = "cbHighlight";
            this.cbHighlight.Size = new System.Drawing.Size(241, 26);
            this.cbHighlight.TabIndex = 1;
            this.cbHighlight.SelectedIndexChanged += new System.EventHandler(this.cbHighlight_SelectedIndexChanged);
            // 
            // lbHighlight
            // 
            this.lbHighlight.AutoSize = true;
            this.lbHighlight.Location = new System.Drawing.Point(7, 219);
            this.lbHighlight.Name = "lbHighlight";
            this.lbHighlight.Size = new System.Drawing.Size(54, 18);
            this.lbHighlight.TabIndex = 2;
            this.lbHighlight.Text = "label1";
            // 
            // binRangeTrackbar
            // 
            this.binRangeTrackbar.Location = new System.Drawing.Point(45, 93);
            this.binRangeTrackbar.Name = "binRangeTrackbar";
            this.binRangeTrackbar.Size = new System.Drawing.Size(299, 67);
            this.binRangeTrackbar.TabIndex = 0;
            this.binRangeTrackbar.ValueLeft = 80;
            this.binRangeTrackbar.ValueRight = 200;
            // 
            // BinaryProp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpBinary);
            this.Controls.Add(this.chkUse);
            this.Name = "BinaryProp";
            this.Size = new System.Drawing.Size(725, 531);
            this.grpBinary.ResumeLayout(false);
            this.grpBinary.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkUse;
     
   
        private System.Windows.Forms.GroupBox grpBinary;
        private System.Windows.Forms.Label lbHighlight;
        private System.Windows.Forms.ComboBox cbHighlight;
        private RangeTrackbar binRangeTrackbar;
    }
}
