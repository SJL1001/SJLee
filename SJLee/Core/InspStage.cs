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
