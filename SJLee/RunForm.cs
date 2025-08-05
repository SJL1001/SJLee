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
    public partial class RunForm : DockContent
    {

       // private Timer _liveTimer = new Timer();
       // private bool _isLive = false;
     //   private int _liveBufferIndex = 0;
       // private int _grabBufferIndex = 0;
        public RunForm()
        {
            InitializeComponent();          
        }
        private void btnGrab_Click(object sender, EventArgs e)
        {
            Global.Inst.InspStage.Grab(0);
           // _grabBufferIndex = (_grabBufferIndex + 1) % InspStage.MAX_GRAB_BUF;
        }

        private void btnStartLive_Click(object sender, EventArgs e)
        {
      

            Global.Inst.InspStage.StartLive();
        }

        private void btnStopLive_Click(object sender, EventArgs e)
        {
           
            Global.Inst.InspStage.StopLive();
        }

     
        //#8_INSPECT_BINARY#20 검사 시작 버튼을 디자인창에서 만들고, 검사 함수 호출
        private void btnStart_Click(object sender, EventArgs e)
        {
            Global.Inst.InspStage.TryInspection();
        }
    }
}
