using Client.GuiController;
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
    public partial class FrmLoginSecond : Form
    {
        public FrmLoginSecond()
        {
            InitializeComponent();
            BtnOkCode.Click += LoginController.Instance.LoginUserSecondStep;
            BtnBackupOk.Click += LoginController.Instance.LoginUserBackupCode;
            TxtCode.GotFocus += (s, e) =>
            {
                AcceptButton = BtnOkCode;
            };
            TxtBackup.GotFocus += (s, e) =>
            {
                AcceptButton = BtnBackupOk;
            };
        }
    }
}
