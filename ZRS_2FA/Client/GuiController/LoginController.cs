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
            (string? errorMessage, LoginResult result) = await Communication.Instance.Login(username, password);
            if (errorMessage != null)
            {
                MessageBox.Show($"Login failed: {errorMessage}", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (result == LoginResult.Success)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SwitchToMainForm();
            }
            else if (result == LoginResult.TwoFactorRequired)
            {
                MessageBox.Show("Two-factor authentication is required.", "Two-Factor Authentication", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Handle two-factor authentication here
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

        public void SwitchToRegisterForm(object? o, EventArgs e)
        {
            _form.Hide();
            RegistrationController.Instance.ShowForm();
            _form.Show();
        }

        public void SwitchToMainForm()
        {
            _form.Hide();
            MainController.Instance.ShowForm(_form.TxtUsername.Text);
            _form.TxtPassword.Clear();
            _form.TxtUsername.Clear();
            _form.Show();
        }
    }
}
