using Common.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.GuiController
{
    public class MainController
    {
        private static MainController? _instance;
        public static MainController Instance => _instance ??= new MainController();
        private FrmMain _form;
        private string _username;
        private MainController()
        {
        }
        public void ShowForm(string username, LoginResult lr)
        {
            _form = new FrmMain();
            _username = username;
            _form.Text = $"Main Form - Logged in as {username}";
            switch (lr)
            {
                case LoginResult.SuccessOneStep:
                    break;
                case LoginResult.SuccessTwoFa:
                    _form.BtnEnableTwoFa.Enabled = false;
                    _form.BtnEnableTwoFa.Text = "2FA Enabled";
                    break;
                case LoginResult.SuccessBackup:
                    break;
                default:
                    break;
            }
            _form.ShowDialog();
        }
        public async void EnableTwoFaInit(object? o, EventArgs e)
        {
            string? qrBase64 = await Communication.Instance.EnableTwoFaInit();
            if(qrBase64 == null)
            {
                return;
            }
            _form.PbQr.Image = ConvertToQrImage(qrBase64);
            _form.PnlVerify.Visible = true;
        }

        public Image ConvertToQrImage(string qrBase64)
        {
            byte[] imageBytes = Convert.FromBase64String(qrBase64);
            using MemoryStream ms = new(imageBytes);
            Image qrImage = Image.FromStream(ms);
            return qrImage;
        }

        public async void BtnOk_Click(object? sender, EventArgs e)
        {
            string code = _form.TxtCode.Text.Trim();
            if (code.Length != 6)
            {
                MessageBox.Show("Please enter all 6 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool result = await Communication.Instance.EnableTwoFaConfirm(code);
            if (result)
            {
                MessageBox.Show("2FA enabled successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _form.PnlVerify.Visible = false;
                _form.PbQr.Image = null;
                _form.TxtCode.Clear();
                _form.BtnEnableTwoFa.Enabled = false;
                _form.BtnEnableTwoFa.Text = "2FA Enabled";
            }
            else
            {
                MessageBox.Show("Failed to enable 2FA. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
