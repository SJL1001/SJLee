using SaigeVision.Net.V2;
using SaigeVision.Net.V2.IAD;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJLee
{
    internal class SaigeAI : IDisposable
    {
        private enum EngineType 
        {
           
        }
        private Dictionary<string, IADResult> _IADResults;

        IADEngine _iADEngine = null;
        IADResult _iADresult = null;
        Bitmap _inspImage = null;

        public SaigeAI()
        {
           _IADResults = new Dictionary<string, IADResult>();
        }

        public void LoadEngine(string modelPath)
        {
            if(this._iADEngine != null)
              this._iADEngine.Dispose();

            _iADEngine = new IADEngine(modelPath, 0);

            IADOption option = _iADEngine.GetInferenceOption();

            option.CalcScoremap = false;

            option.CalcHeatmap = false;

            option.CalcObjectAreaAndApplyThreshold
                = true;

            option.CalcObjectAreaAndApplyThreshold = true;

            option.CalcTime = true;

            _iADEngine.SetInferenceOption(option);

        }

        public bool InspIAD(Bitmap bmpImage)
        {
            if(_iADEngine == null)
            {
                Console.WriteLine("엔진이 초기화되지 않았습니다. LoadEngine 메서드를 호출하여 엔진을 초기화하세요.");
                return false;
            }

            _inspImage = bmpImage;

            SrImage srImage = new SrImage(bmpImage);

            Stopwatch sw = Stopwatch.StartNew();

            _iADresult = _iADEngine.Inspection(srImage);
                    
            sw.Stop();

            return true;
        }

        private void DrawIADResult(IADResult result, Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            int step = 10;

            foreach (var prediction in result.SegmentedObjects)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(127, prediction.ClassInfo.Color));
                
                using (GraphicsPath gp = new GraphicsPath())
                {
                    if (prediction.Contour.Value.Count < 3) continue;
                    gp.AddPolygon(prediction.Contour.Value.ToArray());
                    foreach (var innerValue in prediction.Contour.InnerValue)
                    {
                        gp.AddPolygon(innerValue.ToArray());
                    }
                    g.FillPath(brush, gp);
                }
                step += 50;
            }
        }

        public Bitmap GetResultImage()
        {
            if (_iADresult == null || _inspImage is null)
                return null;

            Bitmap resultImage = _inspImage.Clone(new Rectangle(0, 0, _inspImage.Width, _inspImage.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
             DrawIADResult(_iADresult, resultImage);

            return resultImage;
        }




        #region Disposable
        private bool disposed = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                  
                    if (_iADEngine != null)
                        _iADEngine.Dispose();
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
