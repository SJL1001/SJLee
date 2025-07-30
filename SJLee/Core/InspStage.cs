using SaigeVision.Net.V2.IAD;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJLee
{
    internal class InspStage : IDisposable
    {
        SaigeAI _saigeAI; 

        public InspStage() { }

        public SaigeAI AIModule
        {
            get
            {
                if (_saigeAI is null)
                    _saigeAI = new SaigeAI();
                return _saigeAI;
            }
        }
        public bool Initialize()
        {

            return true;
        }
        public void UpdateDisplay(Bitmap bitmapresult)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateDisplay(bitmapresult);
            }
        }

        
        public Bitmap GetCurrentImage()
        {
            Bitmap bitmapresult = null;
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                bitmapresult = cameraForm.GetDisplayImage();
            }

            return bitmapresult;
        }

        private Bitmap _originalImage = null;

        public void SetOriginalImage(Bitmap image)
        {
            _originalImage?.Dispose();
            if (image != null)
            {
                _originalImage = image.Clone(
                    new Rectangle(0, 0, image.Width, image.Height),
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            }
        }

        public Bitmap GetOriginalImage()
        {
            if (_originalImage == null)
                return null;

            return _originalImage.Clone(
                new Rectangle(0, 0, _originalImage.Width, _originalImage.Height),
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }


        #region Disposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                  
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
