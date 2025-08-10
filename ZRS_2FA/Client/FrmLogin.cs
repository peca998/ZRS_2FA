using Client.GuiController;

namespace Client
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
            ChbShowPass.CheckedChanged += LoginController.Instance.RevealPassword;
            llblRegister.LinkClicked += LoginController.Instance.SwitchToRegisterForm;
            BtnLogin.Click += LoginController.Instance.LoginUser;
        }
    }
}
