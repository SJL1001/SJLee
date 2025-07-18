﻿using System;
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
        public CameraForm()
        {
            InitializeComponent();
        }
        public void LoadImage(string filePath)
        {
            if (File.Exists(filePath) == false)
                return;

            picMainview.Image = Image.FromFile(filePath);
        }
    }
}
