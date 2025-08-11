using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            PbQr.SizeMode = PictureBoxSizeMode.StretchImage;
            PnlVerify.Visible = false;
            BtnOk.Click += GuiController.MainController.Instance.BtnOk_Click;
            RtxtBackupCodes.Visible = false;
            LblBackupCodes.Visible = false;
        }
    }
}
