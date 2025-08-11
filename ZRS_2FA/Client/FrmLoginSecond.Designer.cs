namespace Client
{
    partial class FrmLoginSecond
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblEnterCode = new Label();
            lblBackup = new Label();
            txtCode = new TextBox();
            txtBackup = new TextBox();
            btnOkCode = new Button();
            btnBackupOk = new Button();
            SuspendLayout();
            // 
            // lblEnterCode
            // 
            lblEnterCode.AutoSize = true;
            lblEnterCode.Location = new Point(231, 13);
            lblEnterCode.Name = "lblEnterCode";
            lblEnterCode.Size = new Size(130, 20);
            lblEnterCode.TabIndex = 0;
            lblEnterCode.Text = "Enter 6 digit code:";
            // 
            // lblBackup
            // 
            lblBackup.AutoSize = true;
            lblBackup.Location = new Point(140, 118);
            lblBackup.Name = "lblBackup";
            lblBackup.Size = new Size(326, 20);
            lblBackup.TabIndex = 1;
            lblBackup.Text = "Use backup code instead if you lost your device:";
            // 
            // txtCode
            // 
            txtCode.BorderStyle = BorderStyle.FixedSingle;
            txtCode.Location = new Point(214, 49);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(125, 27);
            txtCode.TabIndex = 2;
            // 
            // txtBackup
            // 
            txtBackup.BorderStyle = BorderStyle.FixedSingle;
            txtBackup.Location = new Point(214, 152);
            txtBackup.Name = "txtBackup";
            txtBackup.Size = new Size(125, 27);
            txtBackup.TabIndex = 3;
            // 
            // btnOkCode
            // 
            btnOkCode.Location = new Point(345, 47);
            btnOkCode.Name = "btnOkCode";
            btnOkCode.Size = new Size(52, 29);
            btnOkCode.TabIndex = 4;
            btnOkCode.Text = "OK";
            btnOkCode.UseVisualStyleBackColor = true;
            // 
            // btnBackupOk
            // 
            btnBackupOk.Location = new Point(345, 150);
            btnBackupOk.Name = "btnBackupOk";
            btnBackupOk.Size = new Size(52, 29);
            btnBackupOk.TabIndex = 5;
            btnBackupOk.Text = "OK";
            btnBackupOk.UseVisualStyleBackColor = true;
            // 
            // FrmLoginSecond
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(623, 208);
            Controls.Add(btnBackupOk);
            Controls.Add(btnOkCode);
            Controls.Add(txtBackup);
            Controls.Add(txtCode);
            Controls.Add(lblBackup);
            Controls.Add(lblEnterCode);
            Name = "FrmLoginSecond";
            Text = "Authentication";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblEnterCode;
        private Label lblBackup;
        private TextBox txtCode;
        private TextBox txtBackup;
        private Button btnOkCode;
        private Button btnBackupOk;

        public Label LblEnterCode { get => lblEnterCode; set => lblEnterCode = value; }
        public Label LblBackup { get => lblBackup; set => lblBackup = value; }
        public TextBox TxtCode { get => txtCode; set => txtCode = value; }
        public TextBox TxtBackup { get => txtBackup; set => txtBackup = value; }
        public Button BtnOkCode { get => btnOkCode; set => btnOkCode = value; }
        public Button BtnBackupOk { get => btnBackupOk; set => btnBackupOk = value; }
    }
}