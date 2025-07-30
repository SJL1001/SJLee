using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SJLee.SaigeAI;

namespace SJLee
{
    public partial class AiModuleProp : UserControl
    {
        SaigeAI _saigeAI; 
        string _modelPath = string.Empty;
        public AiModuleProp()
        {
            InitializeComponent();
            comboBoxAiModule.DataSource = Enum.GetValues(typeof(EngineType));
        }
    
        private void btnSelAIModel_Click(object sender, EventArgs e)
        {
            string filter = GetFilterBySelectedEngineType();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "AI 모델 파일 선택";
                openFileDialog.Filter = filter;
                openFileDialog.Multiselect = false;
                openFileDialog.InitialDirectory = @"C:\Saige\SaigeVision\engine\Examples\data\sfaw2023\models";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _modelPath = openFileDialog.FileName;
                    txtAIModelPath.Text = _modelPath;
                }
            }
        }

        private string GetFilterBySelectedEngineType()
        {
            if (comboBoxAiModule.SelectedItem is EngineType selectedType)
            {
                switch (selectedType)
                {
                    case EngineType.CLS:
                        return "Classification Models (*.saigecls)|*.saigecls";
                    case EngineType.IAD:
                        return "IAD Models (*.saigeiad)|*.saigeiad";
                    case EngineType.DET:
                        return "Detection Models (*.saigedet)|*.saigedet";
                 case EngineType.SEG:
                        return "Segmentation Models (*.saigeseg)|*.saigeseg";
                    default:
                        return "All Files (*.*)|*.*";
                }
            }

            return "All Files (*.*)|*.*";
        }

        private void btnLoadModel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_modelPath))
            {
                MessageBox.Show("모델 파일을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_saigeAI == null)
            {
                _saigeAI = Global.Inst.InspStage.AIModule;
            }
            if (comboBoxAiModule.SelectedItem is EngineType selectedType)
            {
                try
                {
                    _saigeAI.LoadEngine(_modelPath, selectedType);
                    MessageBox.Show("모델이 성공적으로 로드되었습니다.", "정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"모델 로드 실패: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("엔진 타입을 선택해주세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        private void btnInspAI_Click(object sender, EventArgs e)
        {
            if (_saigeAI == null)
            {
                MessageBox.Show("AI 모듈이 초기화되지 않았습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bitmap originalImage = Global.Inst.InspStage.GetOriginalImage();
            if (originalImage == null)
            {
                MessageBox.Show("원본 이미지가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          //  Bitmap bitmapresult = Global.Inst.InspStage.GetCurrentImage();

            

          //  _saigeAI.RunInspection(bitmapresult);

            _saigeAI.RunInspection(originalImage);

            Bitmap resultImage = _saigeAI.GetResultImage();

            Global.Inst.InspStage.UpdateDisplay(resultImage);
        }
        

       
    }
}
