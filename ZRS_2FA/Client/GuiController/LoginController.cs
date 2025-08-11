using Common.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.GuiController
{
    public class LoginController
    {
        private static LoginController? _instance;
        public static LoginController Instance => _instance ??= new LoginController();
        private FrmLogin _form;
        private FrmLoginSecond _authenticationForm;
        private string Username { get; set; }

        private LoginController()
        {

        }

        public void ShowForm()
        {
            bool connected = Communication.Instance.Connnect();
            if (connected)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                _form = new();
                Application.Run(_form);
            }
            else
            {
                MessageBox.Show
                    ("Failed to connect to the server.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        public async void LoginUser(object? o, EventArgs e)
        {
            string username = _form.TxtUsername.Text;
            string password = _form.TxtPassword.Text;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            (string? errorMessage, LoginResult result) = await Communication.Instance.LoginFirstStep(username, password);
            if (errorMessage != null)
            {
                MessageBox.Show($"Login failed: {errorMessage}", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (result == LoginResult.SuccessOneStep)
            {
                Username = username;
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SwitchToMainForm(LoginResult.SuccessOneStep);
            }
            else if (result == LoginResult.TwoFactorRequired)
            {
                Username = username;
                MessageBox.Show("Two-factor authentication is required.", "Two-Factor Authentication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SwitchToAuthenticationForm();
            }
            else
            {
                MessageBox.Show("Login failed. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RevealPassword(object? o, EventArgs e)
        {
            if (_form.ChbShowPass.Checked)
            {
                _form.TxtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                _form.TxtPassword.UseSystemPasswordChar = true;
            }
        }

        public async void LoginUserSecondStep(object? o, EventArgs e)
        {
            string code = _authenticationForm.TxtCode.Text;
            if(!VerifyCode(code))
            {
                return;
            }
            (string? err, LoginResult result) = await Communication.Instance.LoginSecondStep(Username, code);
            if(err != null)
            {
                MessageBox.Show($"Login failed: {err}", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(result == LoginResult.SuccessTwoFa)
            {
                _authenticationForm.TxtCode.Clear();
                _authenticationForm.Close();
                _authenticationForm.Dispose();
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SwitchToMainForm(LoginResult.SuccessTwoFa);
            }
            else
            {
                MessageBox.Show("Unexpected error. Please check your code and try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool VerifyCode(string code)
        {
            if(string.IsNullOrWhiteSpace(code) || code.Length != 6 || !code.All(char.IsDigit))
            {
                MessageBox.Show("Invalid code format. Please enter a 6-digit numeric code.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public async void LoginUserBackupCode(object? o, EventArgs e)
        {
            string backupCode = _authenticationForm.TxtBackup.Text;
            if (string.IsNullOrWhiteSpace(backupCode))
            {
                MessageBox.Show("Please enter backup code.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            (string? err, LoginResult result) = await Communication.Instance.LoginBackupCode(Username, backupCode);
            if (err != null)
            {
                MessageBox.Show($"Login failed: {err}", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (result == LoginResult.SuccessTwoFa)
            {
                _authenticationForm.TxtBackup.Clear();
                _authenticationForm.Close();
                _authenticationForm.Dispose();
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SwitchToMainForm(LoginResult.SuccessBackup);
            }
            else
            {
                MessageBox.Show("Unexpected error. Please check your code and try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // form switching methods
        public void SwitchToRegisterForm(object? o, EventArgs e)
        {
            _form.Hide();
            RegistrationController.Instance.ShowForm();
            _form.Show();
        }

        public void SwitchToMainForm(LoginResult lr = LoginResult.SuccessOneStep)
        {
            _form.Hide();
            MainController.Instance.ShowForm(_form.TxtUsername.Text, lr);
            _form.TxtPassword.Clear();
            _form.TxtUsername.Clear();
            _form.Show();
        }

        public void SwitchToAuthenticationForm()
        {
            _form.Hide();
            _authenticationForm = new();
            _authenticationForm.ShowDialog(_form);
            _form.Show();
        }
    }
}
