using SaigeVision.Net.V2;
using SaigeVision.Net.V2.Classification;
using SaigeVision.Net.V2.Detection;
using SaigeVision.Net.V2.IAD;
using SaigeVision.Net.V2.Segmentation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SJLee
{
    internal class SaigeAI : IDisposable
    {
        public enum EngineType { IAD, DET, CLS, SEG }
        private Dictionary<string, IADResult> _iadResults;
        private Dictionary<string, DetectionResult> _detResults;
        private Dictionary<string, ClassificationResult> _clsResults;
        private Dictionary<string, SegmentationResult> _segResults;
        private EngineType _engineType;

        IADEngine _iadEngine = null;
        IADResult _iadResult = null;
<<<<<<< HEAD
        Bitmap _inspImage = null;
        DetectionEngine _detEngine = null;
        DetectionResult _detResult = null;
        ClassificationEngine _clsEngine = null;
        ClassificationResult _clsResult = null;
        SegmentationEngine _segEngine = null;
        SegmentationResult _segResult = null;
=======
       
        DetectionEngine _detEngine = null;
        DetectionResult _detResult = null;
        ClassificationEngine _clsEngine = null;
        ClassificationResult _clsResult = null;
        SegmentationEngine _segEngine = null;
        SegmentationResult _segResult = null;
        Bitmap _inspImage = null;
>>>>>>> 250730#1


        public SaigeAI()
        {
            _iadResults = new Dictionary<string, IADResult>();
            _detResults = new Dictionary<string, DetectionResult>();
            _clsResults = new Dictionary<string, ClassificationResult>();
<<<<<<< HEAD
=======
            _segResults = new Dictionary<string , SegmentationResult>();
>>>>>>> 250730#1
        }

        public void LoadEngine(string modelPath, EngineType type)
        {
            _engineType = type;
<<<<<<< HEAD

=======
          
>>>>>>> 250730#1
            if (_iadEngine != null)
                _iadEngine.Dispose();
            _iadEngine = null;

            if (_clsEngine != null)
                _clsEngine.Dispose();
            _clsEngine = null;

            if (_detEngine != null)
                _detEngine.Dispose();
            _detEngine = null;

            if (_segEngine != null)
                _segEngine.Dispose();
            _segEngine = null;
            switch (type)
            {
                case EngineType.IAD:
                    _iadEngine = new IADEngine(modelPath, 0);
                    var iadOpt = _iadEngine.GetInferenceOption();
                    iadOpt.CalcObject = true;
                    iadOpt.CalcObjectAreaAndApplyThreshold = true;
                    iadOpt.CalcObjectScoreAndApplyThreshold = true;
                    iadOpt.CalcTime = true;
                    _iadEngine.SetInferenceOption(iadOpt);
                    break;

                case EngineType.DET:
                    _detEngine = new DetectionEngine(modelPath, 0);
                    var detOpt = _detEngine.GetInferenceOption();
                    detOpt.CalcTime = true;
                    _detEngine.SetInferenceOption(detOpt);
                    break;

                case EngineType.CLS:
                    _clsEngine = new ClassificationEngine(modelPath, 0);
                    var clsOpt = _clsEngine.GetInferenceOption();
                    clsOpt.CalcTime = true;
                    clsOpt.AdditionalScores[0] = 0;
                    clsOpt.CalcClassActivationMap = true;
                    _clsEngine.SetInferenceOption(clsOpt);
                    break;
<<<<<<< HEAD

                case EngineType.SEG:
                    _segEngine = new SegmentationEngine(modelPath, 0);
                    var segOpt = _segEngine.GetInferenceOption();
                    segOpt.CalcTime = true;
                    segOpt.CalcObject = true;
                    segOpt.CalcScoremap = false;
                    segOpt.CalcObjectAreaAndApplyThreshold = true;
                    segOpt.CalcObjectScoreAndApplyThreshold = true;
                    segOpt.OversizedImageHandling = OverSizeImageFlags.resize_to_fit;

                    _segEngine.SetInferenceOption(segOpt);
                    break;
            }

        }

=======

                case EngineType.SEG:
                    _segEngine = new SegmentationEngine(modelPath, 0);
                    var segOpt = _segEngine.GetInferenceOption();
                    segOpt.CalcTime = true;
                    segOpt.CalcObject = true;
                    segOpt.CalcScoremap = false;
                    segOpt.CalcObjectAreaAndApplyThreshold = true;
                    segOpt.CalcObjectScoreAndApplyThreshold = true;
                    segOpt.OversizedImageHandling = OverSizeImageFlags.resize_to_fit;

                    _segEngine.SetInferenceOption(segOpt);
                    break;
            }


        }

>>>>>>> 250730#1
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
        private void DrawDetectionResult(DetectionResult result, Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            int step = 10;

<<<<<<< HEAD

            foreach (var prediction in result.DetectedObjects)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(127, prediction.ClassInfo.Color));

=======
           
            foreach (var prediction in result.DetectedObjects)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(127, prediction.ClassInfo.Color));
                
>>>>>>> 250730#1
                using (GraphicsPath gp = new GraphicsPath())
                {
                    float x = (float)prediction.BoundingBox.X;
                    float y = (float)prediction.BoundingBox.Y;
                    float width = (float)prediction.BoundingBox.Width;
                    float height = (float)prediction.BoundingBox.Height;
                    gp.AddRectangle(new RectangleF(x, y, width, height));
                    g.DrawPath(new Pen(brush, 10), gp);
                }
                step += 50;
            }
        }

        private void DrawSegResult(SegmentationResult result, Bitmap bmp)
        {
            Graphics g = Graphics.FromImage(bmp);
            int step = 10;

<<<<<<< HEAD

=======
          
>>>>>>> 250730#1
            foreach (var prediction in result.SegmentedObjects)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(127, prediction.ClassInfo.Color));
                using (GraphicsPath gp = new GraphicsPath())
                {
                    if (prediction.Contour.Value.Count < 4) continue;
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

        public bool RunInspection(Bitmap bmpImage)
        {
            if (bmpImage == null)
                return false;

<<<<<<< HEAD
            _inspImage = bmpImage.Clone(new Rectangle(0, 0, bmpImage.Width, bmpImage.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
=======
>>>>>>> 250730#1
            _iadResult = null;
            _detResult = null;
            _clsResult = null;
            _segResult = null;

<<<<<<< HEAD
           
            SrImage srImage = new SrImage(_inspImage);
=======
            _inspImage = (Bitmap)bmpImage.Clone();
            SrImage srImage = new SrImage(bmpImage);
>>>>>>> 250730#1
            Stopwatch sw = Stopwatch.StartNew();

            bool success = false;

            switch (_engineType)
            {
                case EngineType.IAD:
                    if (_iadEngine == null)
                    {
                        Console.WriteLine("IAD 엔진이 초기화되지 않았습니다.");
                        return false;
                    }
                    _iadResult = _iadEngine.Inspection(srImage);
                    success = _iadResult != null;
                    break;

                case EngineType.DET:
                    if (_detEngine == null)
                    {
                        Console.WriteLine("Detection 엔진이 초기화되지 않았습니다.");
                        return false;
                    }
                    _detResult = _detEngine.Inspection(srImage);
                    success = _detResult != null;
<<<<<<< HEAD
                    break;


                case EngineType.CLS:
                    if (_clsEngine == null)
                    {
                        Console.WriteLine("Classification 엔진이 초기화되지 않았습니다.");
                        return false;
                    }
                    _clsResult = _clsEngine.Inspection(srImage);
                    success = _clsResult != null;
                    break;

                case EngineType.SEG:
                    if (_segEngine == null)
                    {
                        Console.WriteLine("Segmentation 엔진이 초기화되지 않았습니다.");
                        return false;
                    }
                    _segResult = _segEngine.Inspection(srImage);
                    success = _segResult != null;
                    break;

=======
                    break;

                
            case EngineType.CLS:
                if (_clsEngine == null)
                {
                    Console.WriteLine("Classification 엔진이 초기화되지 않았습니다.");
                    return false;
                }
                _clsResult = _clsEngine.Inspection(srImage);
                success = _clsResult != null;
                break;

                case EngineType.SEG:
                    if(_segEngine == null)
                    {
                        Console.WriteLine("Segmentation 엔진이 초기화되지 않았습니다.");
                        return false;
                    }
                    _segResult = _segEngine.Inspection(srImage);
                    success = _segResult != null;
                    break;

>>>>>>> 250730#1

                default:
                    Console.WriteLine("지원하지 않는 엔진 타입입니다.");
                    return false;
            }

            sw.Stop();
            return success;
        }


        public Bitmap GetResultImage()
        {

<<<<<<< HEAD
=======
     
>>>>>>> 250730#1
            if (_inspImage == null)
                return null;
            Bitmap resultImage = _inspImage.Clone(
        new Rectangle(0, 0, _inspImage.Width, _inspImage.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            switch (_engineType)
            {
                case EngineType.IAD:
                    if (_iadResult != null)
                        DrawIADResult(_iadResult, resultImage);
                    break;

                case EngineType.DET:
                    if (_detResult != null)
                        DrawDetectionResult(_detResult, resultImage);
                    break;

                /*
            case EngineType.CLS:
                if (_cLSresult != null && _cLSresult.ActivationMap != null)
                    DrawCLSResult(_cLSresult, resultImage); // CAM 오버레이 함수 필요
                break;
                */
                case EngineType.SEG:
                    if (_segResult != null)
                        DrawSegResult(_segResult, resultImage);
                    break;
            }

            return resultImage ;
        }




        #region Disposable
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                    if (_iadEngine != null)
                        _iadEngine.Dispose();
                    if (_clsEngine != null)
                        _clsEngine.Dispose();
                    if (_detEngine != null)
                        _detEngine.Dispose();
                    if (_segEngine != null)
                        _segEngine.Dispose();

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
