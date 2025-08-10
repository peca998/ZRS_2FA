namespace Client
{
    partial class FrmLogin
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblUsername = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            chbShowPass = new CheckBox();
            llblRegister = new LinkLabel();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(48, 23);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(78, 20);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Username:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(48, 83);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(73, 20);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Password:";
            // 
            // txtUsername
            // 
            txtUsername.BackColor = SystemColors.Window;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Location = new Point(142, 23);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(180, 27);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Location = new Point(142, 77);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(180, 27);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogin.Location = new Point(216, 131);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(94, 29);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            // 
            // chbShowPass
            // 
            chbShowPass.AutoSize = true;
            chbShowPass.Location = new Point(328, 80);
            chbShowPass.Name = "chbShowPass";
            chbShowPass.Size = new Size(142, 24);
            chbShowPass.TabIndex = 5;
            chbShowPass.Text = "Reveal password";
            chbShowPass.UseVisualStyleBackColor = true;
            // 
            // llblRegister
            // 
            llblRegister.AutoSize = true;
            llblRegister.Location = new Point(142, 176);
            llblRegister.Name = "llblRegister";
            llblRegister.Size = new Size(254, 20);
            llblRegister.TabIndex = 6;
            llblRegister.TabStop = true;
            llblRegister.Text = "Don't have an account? Register here";
            // 
            // FrmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 218);
            Controls.Add(llblRegister);
            Controls.Add(chbShowPass);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Name = "FrmLogin";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private CheckBox chbShowPass;
        private LinkLabel llblRegister;

        public Label LblUsername { get => lblUsername; set => lblUsername = value; }
        public Label LblPassword { get => lblPassword; set => lblPassword = value; }
        public TextBox TxtUsername { get => txtUsername; set => txtUsername = value; }
        public TextBox TxtPassword { get => txtPassword; set => txtPassword = value; }
        public Button BtnLogin { get => btnLogin; set => btnLogin = value; }
        public CheckBox ChbShowPass { get => chbShowPass; set => chbShowPass = value; }
        public LinkLabel LlblRegister { get => llblRegister; set => llblRegister = value; }
    }
}
