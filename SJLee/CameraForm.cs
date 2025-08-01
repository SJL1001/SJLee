using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SJLee
{
    public partial class CameraForm : DockContent
    {
        public CameraForm()
        {
            InitializeComponent();
        }
        public void LoadImage(string filePath)
        {
            if (File.Exists(filePath) == false)
                return;

            Image bitmap = Image.FromFile(filePath);
            imageViewCtrlMain.LoadBitmap((Bitmap)bitmap);

           // Global.Inst.InspStage.GetCurrentImage();

        }

        /*
        public void SaveImage(string filePath)
        {
            if (imageViewCtrlMain != null )
            {
                imageViewCtrlMain.Image.Save(filePath);
            }
            else
            {
                MessageBox.Show("이미지가 없습니다.", "error");
            }
        
        }
        */
        

        private void CameraForm_Resize(object sender, EventArgs e)
        {
            int margin = 0;
            imageViewCtrlMain.Width = this.Width - margin * 2;
            imageViewCtrlMain.Height = this.Height - margin * 2;

            imageViewCtrlMain.Location = new System.Drawing.Point(margin, margin);
        }

        public void UpdateDisplay(Bitmap bitmap = null)
        {
            if (bitmap == null)
            {                
                bitmap = Global.Inst.InspStage.GetBitmap(0);
                if (bitmap == null)
                    return;
            }
            if (imageViewCtrlMain != null)
                imageViewCtrlMain.LoadBitmap(bitmap);
        }

        public Bitmap GetDisplayImage()
        {
            Bitmap curImage = null;

            if (imageViewCtrlMain != null)
                curImage = imageViewCtrlMain.GetCurBitmap();

            return curImage;
        }
    }

}
