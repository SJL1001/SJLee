using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJLee
{
    /*
    public partial class SetupForm : Form
    {


        /*
        public enum SetupType
        {
            Camera,
            Path,
            Coummusnicator,
            Inspection,
            SignalDelay
        }
        Dictionary<string, TabPage> _allTabs = new Dictionary<string, TabPage>();
        public SetupForm()
        {
            InitializeComponent();
            LoadOptionControl(SetupType.Camera);
            LoadOptionControl(SetupType.Path);
            LoadOptionControl(SetupType.Coummunicator);
            LoadOptionControl(SetupType.Inspection);
            LoadOptionControl(SetupType.SignalDelay);

            tabSetup.SelectedIndex = 0;


        }
        private void LoadOptionControl(SetupType setupType)
        {
            string tabName = setupType.ToString();


            foreach (TabPage tabPage in tabSetup.TabPages)
            {
                if (tabPage.Text == tabName)
                    return;
            }


            if (_allTabs.TryGetValue(tabName, out TabPage page))
            {
                tabSetup.TabPages.Add(page);
                return;
            }


            UserControl _inspProp = CreateUserControl(setupType);
            if (_inspProp == null)
                return;


            TabPage newTab = new TabPage(tabName)
            {
                Dock = DockStyle.Fill
            };
            _inspProp.Dock = DockStyle.Fill;
            newTab.Controls.Add(_inspProp);
            tabSetup.TabPages.Add(newTab);
            tabSetup.SelectedTab = newTab;

            _allTabs[tabName] = newTab;
        }


        private UserControl CreateUserControl(SetupType setupType)
        {
            UserControl curSetup = null;
            switch (setupType)
            {
                case SetupType.Camera:
                    CameraSetting camSetup = new CameraSetting(Global.Inst.InspStage.CamType);
                    curSetup = camSetup;
                    break;
                case SetupType.Path:
                    Communicator communicatorSetup = new Communicator();
                    curSetup = communicatorSetup;
                    break;
                case SetupType.Coummunicator:
                    PathSetting pathSetup = new PathSetting();
                    curSetup = pathSetup;
                    break;

                case SetupType.Inspection:
                    Inspection inspSetup = new Inspection();
                    curSetup = inspSetup;
                    break;

                    case SetupType.SignalDelay:
                        Signal_Delay sigSetup = new Signal_Delay();
                    curSetup = sigSetup;
                    break;

                default:
                    MessageBox.Show("유효하지 않은 옵션입니다.");
                    return null;
            }
            return curSetup;
        }
      */
    //}

    public enum SettingType
    {
        SettingPath = 0,
        SettingCamera
    }

    public partial class SetupForm : Form
    {
        public SetupForm()
        {
            InitializeComponent();

            //#SETUP#5 탭 콘트롤 추가, 아래 함수 함께 구현할것
            InitTabControl();
        }

        private void InitTabControl()
        {
            //카메라 설정 페이지 추가
            CameraSetting cameraSetting = new CameraSetting();
            AddTabControl(cameraSetting, "Camera");

            //경로 설정 페이지 추가
            PathSetting pathSetting = new PathSetting();
            AddTabControl(pathSetting, "Path");

            //#19_VISION_SEQUENCE#2 통신 설정 페이지 추가
            CommunicatorSetting commSetting = new CommunicatorSetting();
            AddTabControl(commSetting, "Communicator");
            //기본값으로 카메라 설정 페이지 보이도록 설정
            tabSetting.SelectTab(0);
        }

        //탭 추가 함수
        private void AddTabControl(UserControl control, string tabName)
        {
            // 새 탭 추가
            TabPage newTab = new TabPage(tabName)
            {
                Dock = DockStyle.Fill
            };
            control.Dock = DockStyle.Fill;
            newTab.Controls.Add(control);
            tabSetting.TabPages.Add(newTab);
        }
    }
}

