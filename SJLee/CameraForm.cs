using OpenCvSharp;
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
        //#18_IMAGE_CHANNEL#3 현재 선택된 이미지 채널을 저장하는 변수
        //_currentImageChannel 변수 모두 찾아서, 관련 코드 수정할것
        eImageChannel _currentImageChannel = eImageChannel.Gray;
        public CameraForm()
        {
            InitializeComponent();

            imageViewCtrlMain.DiagramEntityEvent += imageViewCtrlMain_DiagramEntityEvent;
            //#18_IMAGE_CHANNEL#1 메인툴바 이벤트 처리
            mainViewToolbar.ButtonChanged += Toolbar_ButtonChanged;
        }
        private void imageViewCtrlMain_DiagramEntityEvent(object sender, DiagramEntityEventArgs e)
        {
            SLogger.Write($"ImageViewer Action {e.ActionType.ToString()}");
            switch (e.ActionType)
            {

                case EntityActionType.Select:
                    Global.Inst.InspStage.SelectInspWindow(e.InspWindow);
                    imageViewCtrlMain.Focus();
                    break;
                case EntityActionType.Inspect:
                    UpdateDiagramEntity();
                    Global.Inst.InspStage.TryInspection(e.InspWindow);
                    break;
                case EntityActionType.Add:
                    Global.Inst.InspStage.AddInspWindow(e.WindowType, e.Rect);
                    break;
                case EntityActionType.Copy:
                    Global.Inst.InspStage.AddInspWindow(e.InspWindow, e.OffsetMove);
                    break;
                case EntityActionType.Move:
                    Global.Inst.InspStage.MoveInspWindow(e.InspWindow, e.OffsetMove);
                    break;
                case EntityActionType.Resize:
                    Global.Inst.InspStage.ModifyInspWindow(e.InspWindow, e.Rect);
                    break;
                case EntityActionType.Delete:
                    Global.Inst.InspStage.DelInspWindow(e.InspWindow);
                    break;
                case EntityActionType.DeleteList:
                    Global.Inst.InspStage.DelInspWindow(e.InspWindowList);
                    break;
            }
        }
        //#3_CAMERAVIEW_PROPERTY#1 이미지 경로를 받아 PictureBox에 이미지를 로드하는 메서드
        public void LoadImage(string filePath)
        {
            if (File.Exists(filePath) == false)
                return;

            //#4_IMAGE_VIEWER#6 이미지 뷰어 컨트롤을 사용하여 이미지를 로드
            //picMainview.Image = Image.FromFile(filePath);
            Image bitmap = Image.FromFile(filePath);
            imageViewCtrlMain.LoadBitmap((Bitmap)bitmap);
        }
        public Mat GetDisplayImage()
        {
            return Global.Inst.InspStage.ImageSpace.GetMat(0, _currentImageChannel);
        }


        private void CameraForm_Resize(object sender, EventArgs e)
        {
            int margin = 0;
            imageViewCtrlMain.Width = this.Width - mainViewToolbar.Width - margin * 2;
            imageViewCtrlMain.Height = this.Height - margin * 2;

            imageViewCtrlMain.Location = new System.Drawing.Point(margin, margin);
        }

        public void UpdateDisplay(Bitmap bitmap = null)
        {
            if (bitmap == null)
            {
                //#6_INSP_STAGE#3 업데이트시 bitmap이 없다면 InspSpace에서 가져온다
                bitmap = Global.Inst.InspStage.GetBitmap(0, _currentImageChannel);
                if (bitmap == null)
                    return;
            }

            if (imageViewCtrlMain != null)
                imageViewCtrlMain.LoadBitmap(bitmap);
        }

       
        public void UpdateImageViewer()
        {
            imageViewCtrlMain.UpdateInspParam();
            imageViewCtrlMain.Invalidate();
        }
        public void UpdateDiagramEntity()
        {
            imageViewCtrlMain.ResetEntity();

            Model model = Global.Inst.InspStage.CurModel;
            List<DiagramEntity> diagramEntityList = new List<DiagramEntity>();

            foreach (InspWindow window in model.InspWindowList)
            {
                if (window is null)
                    continue;

                DiagramEntity entity = new DiagramEntity()
                {
                    LinkedWindow = window,
                    EntityROI = new Rectangle(
                        window.WindowArea.X, window.WindowArea.Y,
                            window.WindowArea.Width, window.WindowArea.Height),
                    EntityColor = imageViewCtrlMain.GetWindowColor(window.InspWindowType),
                    IsHold = window.IsTeach
                };
                diagramEntityList.Add(entity);
            }

            imageViewCtrlMain.SetDiagramEntityList(diagramEntityList);
        }

        public void SelectDiagramEntity(InspWindow window)
        {
            imageViewCtrlMain.SelectDiagramEntity(window);
        }
        //#8_INSPECT_BINARY#18 imageViewer에 검사 결과 정보를 연결해주기 위한 함수
        public void ResetDisplay()
        {
            imageViewCtrlMain.ResetEntity();
        }

        //FIXME 검사 결과를 그래픽으로 출력하기 위한 정보를 받는 함수
        public void AddRect(List<DrawInspectInfo> rectInfos)
        {
            imageViewCtrlMain.AddRect(rectInfos);
        }

        public void AddRoi(InspWindowType inspWindowType)
        {
            imageViewCtrlMain.NewRoi(inspWindowType);
        }

        //#13_INSP_RESULT#6 검사 양불판정 갯수 설정 함수
        public void SetInspResultCount(int totalArea, int okCnt, int ngCnt)
        {
            imageViewCtrlMain.SetInspResultCount(new InspectResultCount(totalArea, okCnt, ngCnt));
        }
        //#17_WORKING_STATE#5 작업 상태 화면 표시 설정
        public void SetWorkingState(WorkingState workingState)
        {
            string state = "";
            switch (workingState)
            {
                case WorkingState.INSPECT:
                    state = "INSPECT";
                    break;

                case WorkingState.LIVE:
                    state = "LIVE";
                    break;

                case WorkingState.ALARM:
                    state = "ALARM";
                    break;
            }

            imageViewCtrlMain.WorkingState = state;
            imageViewCtrlMain.Invalidate();
        }
        //#18_IMAGE_CHANNEL#2 메인툴바의 버튼 이벤트를 처리하는 함수
        private void Toolbar_ButtonChanged(object sender, ToolbarEventArgs e)
        {
            switch (e.Button)
            {
                case ToolbarButton.ShowROI:
                    if (e.IsChecked)
                        UpdateDiagramEntity();
                    else
                        imageViewCtrlMain.ResetEntity();
                    break;
                case ToolbarButton.ChannelColor:
                    _currentImageChannel = eImageChannel.Color;
                    UpdateDisplay();
                    break;
                case ToolbarButton.ChannelGray:
                    _currentImageChannel = eImageChannel.Gray;
                    UpdateDisplay();
                    break;
                case ToolbarButton.ChannelRed:
                    _currentImageChannel = eImageChannel.Red;
                    UpdateDisplay();
                    break;
                case ToolbarButton.ChannelGreen:
                    _currentImageChannel = eImageChannel.Green;
                    UpdateDisplay();
                    break;
                case ToolbarButton.ChannelBlue:
                    _currentImageChannel = eImageChannel.Blue;
                    UpdateDisplay();
                    break;
            }
        }
        public void SetImageChannel(eImageChannel channel)
        {
            mainViewToolbar.SetSelectButton(channel);
            UpdateDisplay();
        }
        private void CameraForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainViewToolbar.ButtonChanged -= Toolbar_ButtonChanged;

            imageViewCtrlMain.DiagramEntityEvent -= imageViewCtrlMain_DiagramEntityEvent;

            this.FormClosed -= CameraForm_FormClosed;
        }
    }


}
