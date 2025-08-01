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

        private Timer _liveTimer = new Timer();
        private bool _isLive = false;
        private int _liveBufferIndex = 0;
        public RunForm()
        {
            InitializeComponent();
        }
        private void btnGrab_Click(object sender, EventArgs e)
        {
            Global.Inst.InspStage.Grab(0);
        }

        private void btnStartLive_Click(object sender, EventArgs e)
        {
            if (!_isLive)
            {
                _isLive = true;
                _liveTimer.Interval = 50; // (원하는 fps)
                _liveTimer.Tick += LiveTimer_Tick;
                _liveTimer.Start();
                btnStartLive.Enabled = false;
                btnStopLive.Enabled = true;
            }
        }

        private void btnStopLive_Click(object sender, EventArgs e)
        {
            if (_isLive)
            {
                _isLive = false;
                _liveTimer.Stop();
                btnStartLive.Enabled = true;
                btnStopLive.Enabled = false;
            }
        }
        private void LiveTimer_Tick(object sender, EventArgs e)
        {
            var inspStage = Global.Inst.InspStage; // 기존 파이프라인에서 가져옴
            if (inspStage != null)
            {
                _liveBufferIndex = (_liveBufferIndex + 1) % InspStage.MAX_GRAB_BUF;
                inspStage.Grab(_liveBufferIndex);   // Grab만 호출하면 끝!
            }
        }
    }
}
