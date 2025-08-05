using OpenCvSharp;
using OpenCvSharp.Extensions;
using SaigeVision.Net.V2.IAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJLee
{
    internal class InspStage : IDisposable
    {

        public static readonly int MAX_GRAB_BUF = 5;

        private ImageSpace _imageSpace = null;
        private GrabModel _grabManager = null;
        private CameraType _camType = CameraType.WebCam;
    //    public CameraType CamType { get => _camType; }

        SaigeAI _saigeAI;
    //    BlobAlgorithm _blobAlgorithm = null; // Blob 알고리즘 인스턴스
        private PreviewImage _previewImage = null;
        //#10_INSPWINDOW#8 모델과 선택된 ROI 윈도우 변수 선언
        private Model _model = null;

        private InspWindow _selectedInspWindow = null;
        public InspStage() { }
          
        public ImageSpace ImageSpace
        {
            get => _imageSpace;
        }
        public SaigeAI AIModule
        {
            get
            {
                if (_saigeAI is null)
                    _saigeAI = new SaigeAI();
                return _saigeAI;
            }
        }
        /*
        public BlobAlgorithm BlobAlgorithm
        {
            get => _blobAlgorithm;
        }
        */
        public PreviewImage PreView
        {
            get => _previewImage;
        }

        //#10_INSPWINDOW#9 현재 모델 프로퍼티 생성
        public Model CurModel
        {
            get => _model;
        }
        /*
        private bool _liveMode = false;
        public bool LiveMode
        {
            get => _liveMode;
            set => _liveMode = value;
        }
        */
        public bool LiveMode { get; set; } = false;
        public bool Initialize()
        {
            _imageSpace = new ImageSpace();
            //_blobAlgorithm = new BlobAlgorithm();
            _previewImage = new PreviewImage();
            _model = new Model();
            InitGrabManager(_camType);

            return true;
        }
        private void LoadSetting()
        {
            //카메라 설정 타입 얻기
            _camType = SettingXml.Inst.CamType;
        }
        private void InitGrabManager(CameraType type)
        {
            if (_grabManager != null)
            {
                _grabManager.TransferCompleted -= _multiGrab_TransferCompleted;
                _grabManager.Close();
                _grabManager.Dispose();
                _grabManager = null;
            }         

            switch (type)
            {
               
                case CameraType.WebCam:
                    _grabManager = new WebCam();
                    break;
                case CameraType.HikRobotCam:
                    _grabManager = new HikRobotCam();
                    break;
            }

            if (_grabManager != null && _grabManager.InitGrab())
            {
                _grabManager.TransferCompleted += _multiGrab_TransferCompleted;
                InitModelGrab(MAX_GRAB_BUF);
            }
        }

        public void  ChangeCameraType(CameraType newType)
        {
            if (_camType == newType)
                return;
            if (_grabManager != null)
            {
                _grabManager.TransferCompleted -= _multiGrab_TransferCompleted;
                _grabManager.Close();
                _grabManager.Dispose();
                _grabManager = null;
            }
            if (_imageSpace != null)
            {
                _imageSpace.Dispose();
                _imageSpace = null;
            }
            _imageSpace = new ImageSpace();
            _camType = newType;
            InitGrabManager(_camType);
        }
        public void InitModelGrab(int bufferCount)
        {
            if (_grabManager == null)
                return;

            int pixelBpp = 8;
            _grabManager.GetPixelBpp(out pixelBpp);

            int inspectionWidth;
            int inspectionHeight;
            int inspectionStride;
            _grabManager.GetResolution(out inspectionWidth, out inspectionHeight, out inspectionStride);

            if (_imageSpace != null)
            {
                _imageSpace.SetImageInfo(pixelBpp, inspectionWidth, inspectionHeight, inspectionStride);
            }

            SetBuffer(bufferCount);

            //_grabManager.SetExposureTime(25000);

          //  UpdateProperty();

        }
        private void UpdateProperty(InspWindow inspWindow)
        {
            if (inspWindow is null)
                return;

            PropertiesForm propertiesForm = MainForm.GetDockForm<PropertiesForm>();
            if (propertiesForm is null)
                return;

            propertiesForm.UpdateProperty(inspWindow);
        }
        public void SetBuffer(int bufferCount)
        {
            if (_grabManager == null)
                return;

            if (_imageSpace.BufferCount == bufferCount)
                return;

            _imageSpace.InitImageSpace(bufferCount);
            _grabManager.InitBuffer(bufferCount);

            for (int i = 0; i < bufferCount; i++)
            {
                _grabManager.SetBuffer(
                    _imageSpace.GetInspectionBuffer(i),
                    _imageSpace.GetnspectionBufferPtr(i),
                    _imageSpace.GetInspectionBufferHandle(i),
                    i);
            }
        }

        //#10_INSPWINDOW#12 inspWindow에 대한 검사구현
        public void TryInspection(InspWindow inspWindow = null)
        {
            if (inspWindow is null)
            {
                if (_selectedInspWindow is null)
                    return;

                inspWindow = _selectedInspWindow;
            }

            UpdateDiagramEntity();

            List<DrawInspectInfo> totalArea = new List<DrawInspectInfo>();

            Rect windowArea = inspWindow.WindowArea;

            foreach (var inspAlgo in inspWindow.AlgorithmList)
            {
                //검사 영역 초기화
                inspAlgo.TeachRect = windowArea;
                inspAlgo.InspRect = windowArea;

                InspectType inspType = inspAlgo.InspectType;

                switch (inspType)
                {
                    case InspectType.InspBinary:
                        {
                            BlobAlgorithm blobAlgo = (BlobAlgorithm)inspAlgo;

                            Mat srcImage = Global.Inst.InspStage.GetMat();
                            blobAlgo.SetInspData(srcImage);

                            if (blobAlgo.DoInspect())
                            {
                                List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
                                int resultCnt = blobAlgo.GetResultRect(out resultArea);
                                if (resultCnt > 0)
                                {
                                    totalArea.AddRange(resultArea);
                                }
                            }

                            break;
                        }
                }

                if (inspAlgo.DoInspect())
                {
                    List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
                    int resultCnt = inspAlgo.GetResultRect(out resultArea);
                    if (resultCnt > 0)
                    {
                        totalArea.AddRange(resultArea);
                    }
                }
            }

            if (totalArea.Count > 0)
            {
                //찾은 위치를 이미지상에서 표시
                var cameraForm = MainForm.GetDockForm<CameraForm>();
                if (cameraForm != null)
                {
                    cameraForm.AddRect(totalArea);
                }
            }
        }
        //#10_INSPWINDOW#13 ImageViewCtrl에서 ROI 생성,수정,이동,선택 등에 대한 함수
        public void SelectInspWindow(InspWindow inspWindow)
        {
            _selectedInspWindow = inspWindow;

            var propForm = MainForm.GetDockForm<PropertiesForm>();
            if (propForm != null)
            {
                if (inspWindow is null)
                {
                    propForm.ResetProperty();
                    return;
                }

                //속성창을 현재 선택된 ROI에 대한 것으로 변경
                propForm.ShowProperty(inspWindow);
            }

            UpdateProperty(inspWindow);

            Global.Inst.InspStage.PreView.SetInspWindow(inspWindow);
        }
        //ImageViwer에서 ROI를 추가하여, InspWindow생성하는 함수
        public void AddInspWindow(InspWindowType windowType, Rect rect)
        {
            InspWindow inspWindow = _model.AddInspWindow(windowType);
            if (inspWindow is null)
                return;

            inspWindow.WindowArea = rect;
            inspWindow.IsTeach = false;
            UpdateProperty(inspWindow);
            UpdateDiagramEntity();

            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.SelectDiagramEntity(inspWindow);
                SelectInspWindow(inspWindow);
            }
        }
        public bool AddInspWindow(InspWindow sourceWindow, OpenCvSharp.Point offset)
        {
            InspWindow cloneWindow = sourceWindow.Clone(offset);
            if (cloneWindow is null)
                return false;

            if (!_model.AddInspWindow(cloneWindow))
                return false;

            UpdateProperty(cloneWindow);
            UpdateDiagramEntity();

            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.SelectDiagramEntity(cloneWindow);
                SelectInspWindow(cloneWindow);
            }

            return true;
        }
        //입력된 윈도우 이동
        public void MoveInspWindow(InspWindow inspWindow, OpenCvSharp.Point offset)
        {
            if (inspWindow == null)
                return;

            inspWindow.OffsetMove(offset);
            UpdateProperty(inspWindow);
        }

        //#MODEL#10 기존 ROI 수정되었을때, 그 정보를 InspWindow에 반영
        public void ModifyInspWindow(InspWindow inspWindow, Rect rect)
        {
            if (inspWindow == null)
                return;

            inspWindow.WindowArea = rect;
            inspWindow.IsTeach = false;

            UpdateProperty(inspWindow);
        }
        //#MODEL#11 InspWindow 삭제하기
        public void DelInspWindow(InspWindow inspWindow)
        {
            _model.DelInspWindow(inspWindow);
            UpdateDiagramEntity();
        }


        public void DelInspWindow(List<InspWindow> inspWindowList)
        {
            _model.DelInspWindowList(inspWindowList);
            UpdateDiagramEntity();
        }
        /*
        private bool DisplayResult()
        {
            if (_blobAlgorithm is null)
                return false;

            List<DrawInspectInfo> resultArea = new List<DrawInspectInfo>();
            int resultCnt = _blobAlgorithm.GetResultRect(out resultArea);
            if (resultCnt > 0)
            {
                //찾은 위치를 이미지상에서 표시
                var cameraForm = MainForm.GetDockForm<CameraForm>();
                if (cameraForm != null)
                {
                    cameraForm.ResetDisplay();
                    cameraForm.AddRect(resultArea);
                }
            }

            return true;
        }
        */
     

        public void Grab(int bufferIndex)
        {

            if (_camType == CameraType.None)
                return;
            if (_grabManager == null)
                return;

            _grabManager.Grab(bufferIndex, true);
        }

        //영상 취득 완료 이벤트 발생시 후처리
        private async void _multiGrab_TransferCompleted(object sender, object e)
        {
            int bufferIndex = (int)e;
            Console.WriteLine($"_multiGrab_TransferCompleted {bufferIndex}");

            _imageSpace.Split(bufferIndex);

            DisplayGrabImage(bufferIndex);
           
            if (_previewImage != null)
            {
                Bitmap bitmap = _imageSpace.GetBitmap(bufferIndex);
                _previewImage.SetImage(BitmapConverter.ToMat(bitmap));
            }

            if(LiveMode)
            {
                //이 함수는 await를 사용하여 비동기적으로 실행되어, 함수를 async로 선언해야 합니다.
                await Task.Delay(100); 
                _grabManager.Grab(bufferIndex, true); // 다음 Grab 호출
            }
        }
        public void StartLive()
        {
            if (_camType == CameraType.None)
                return;
            LiveMode = true;
            Grab(0);   // 첫 Grab 시작
        }

        public void StopLive()
        {
            LiveMode = false;
            // 필요하다면 현재 Grab 중단 등 추가
        }
        private void DisplayGrabImage(int bufferIndex)
        {
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateDisplay();
            }
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
            Bitmap bitmap= null;
            var cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                bitmap = cameraForm.GetDisplayImage();
            }

            return bitmap;
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
              
        public Bitmap GetBitmap(int bufferIndex = -1)   
        {
            
            if (Global.Inst.InspStage.ImageSpace is null)
                return null;

            return Global.Inst.InspStage.ImageSpace.GetBitmap();        
        }

        public Mat GetMat()
        {
            return Global.Inst.InspStage.ImageSpace.GetMat();
        }
        public void UpdateDiagramEntity()
        {
            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateDiagramEntity();
            }

            ModelTreeForm modelTreeForm = MainForm.GetDockForm<ModelTreeForm>();
            if (modelTreeForm != null)
            {
                modelTreeForm.UpdateDiagramEntity();
            }
        }
        public void RedrawMainView()
        {
            CameraForm cameraForm = MainForm.GetDockForm<CameraForm>();
            if (cameraForm != null)
            {
                cameraForm.UpdateImageViewer();
            }
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
