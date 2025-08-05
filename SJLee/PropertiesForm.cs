using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SJLee
{
    public enum PropertyType
    {
        Binary,
        Filter,
        AiModule
    }

    public partial class PropertiesForm : DockContent
    {
        Dictionary<string, TabPage> _allTabs = new Dictionary<string, TabPage>();
        public PropertiesForm()
        {
            InitializeComponent();

            /*
            LoadOptionControl(PropertyType.Filter);
            LoadOptionControl(PropertyType.Binary);
            LoadOptionControl(PropertyType.AiModule);
            */
        }
        private void LoadOptionControl(InspectType inspType)
        {
            string tabName = inspType.ToString();


            foreach (TabPage tabPage in tabPropControl.TabPages)
            {
                if (tabPage.Text == tabName)
                    return;
            }


            if (_allTabs.TryGetValue(tabName, out TabPage page))
            {
                tabPropControl.TabPages.Add(page);
                return;
            }


            UserControl _inspProp = CreateUserControl(inspType);
            if (_inspProp == null)
                return;


            TabPage newTab = new TabPage(tabName)
            {
                Dock = DockStyle.Fill
            };
            _inspProp.Dock = DockStyle.Fill;
            newTab.Controls.Add(_inspProp);
            tabPropControl.TabPages.Add(newTab);
            tabPropControl.SelectedTab = newTab;

            _allTabs[tabName] = newTab;
        }


        private UserControl CreateUserControl(InspectType inspPropType)
        {
            UserControl curProp = null;
            switch (inspPropType)
            {
                case InspectType.InspBinary:
                    BinaryProp blobProp = new BinaryProp();
                    blobProp.RangeChanged += RangeSlider_RangeChanged;
                    blobProp.PropertyChanged += PropertyChanged;
                    curProp = blobProp;
                    break;
                case InspectType.InspFilter:
                    ImageFilterProp filterProp = new ImageFilterProp();
                    curProp = filterProp;
                    break;
                case InspectType.InspAIModule:
                    AiModuleProp aiProp = new AiModuleProp();
                    curProp = aiProp;
                    break;
                default:
                    MessageBox.Show("유효하지 않은 옵션입니다.");
                    return null;
            }
            return curProp;
        }
        //#11_MODEL_TREE#3 InspWindow에서 사용하는 알고리즘을 모두 탭에 추가
        public void ShowProperty(InspWindow window)
        {
            foreach (InspAlgorithm algo in window.AlgorithmList)
            {
                LoadOptionControl(algo.InspectType);
            }
        }

        public void ResetProperty()
        {
            tabPropControl.TabPages.Clear();
        }

        public void UpdateProperty(InspWindow window)
        {
            if (window is null)
                return;

            foreach (TabPage tabPage in tabPropControl.TabPages)
            {
                if (tabPage.Controls.Count > 0)
                {
                    UserControl uc = tabPage.Controls[0] as UserControl;

                    if (uc is BinaryProp binaryProp)
                    {
                        BlobAlgorithm blobAlgo = (BlobAlgorithm)window.FindInspAlgorithm(InspectType.InspBinary);
                        if (blobAlgo is null)
                            continue;

                        binaryProp.SetAlgorithm(blobAlgo);
                    }
                }
            }
        }
        private void RangeSlider_RangeChanged(object sender, RangeChangedEventArgs e)
        {
            // 속성값을 이용하여 이진화 임계값 설정
            int lowerValue = e.LowerValue;
            int upperValue = e.UpperValue;
            bool invert = e.Invert;
            ShowBinaryMode showBinMode = e.ShowBinMode;
            Global.Inst.InspStage.PreView?.SetBinary(lowerValue, upperValue, invert, showBinMode);
        }

        private void PropertyChanged(object sender, EventArgs e)
        {
            Global.Inst.InspStage.RedrawMainView();
        }
    }
}
