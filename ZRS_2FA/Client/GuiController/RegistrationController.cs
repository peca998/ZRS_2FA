using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.GuiController
{
    public class RegistrationController
    {
        private static RegistrationController? _instance;
        public static RegistrationController Instance => _instance ??= new RegistrationController();
        private FrmRegister _form;
        private RegistrationController()
        {

        }

        public void ShowForm()
        {
            _form = new();
            _form.ShowDialog();
        }

        public async void RegisterUser(object? o, EventArgs e)
        {
            if(!VerifyInput(_form.TxtUsername.Text, _form.TxtPassword.Text, _form.TxtConfirmPassword.Text))
            {
                return;
            }
            string username = _form.TxtUsername.Text;
            string password = _form.TxtPassword.Text;
            string? result = await Communication.Instance.Register(username, password);
            if (result == null)
            {
                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _form.Close();
            }
            else
            {
                MessageBox.Show($"Registration failed: {result}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool VerifyInput(string username, string password, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("All fields are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (username.Length < 3 || username.Length > 20)
            {
                MessageBox.Show("Username must be between 3 and 20 characters.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (password.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            // Additional validation can be added here (e.g., password strength)
            return true;
        }
    }
}
