namespace Client
{
    partial class FrmMain
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
            btnEnableTwoFa = new Button();
            pbQr = new PictureBox();
            pnlVerify = new Panel();
            lblEnterCode = new Label();
            btnOk = new Button();
            txtCode = new TextBox();
            rtxtBackupCodes = new RichTextBox();
            lblBackupCodes = new Label();
            ((System.ComponentModel.ISupportInitialize)pbQr).BeginInit();
            pnlVerify.SuspendLayout();
            SuspendLayout();
            // 
            // btnEnableTwoFa
            // 
            btnEnableTwoFa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEnableTwoFa.Location = new Point(175, 38);
            btnEnableTwoFa.Name = "btnEnableTwoFa";
            btnEnableTwoFa.Size = new Size(122, 29);
            btnEnableTwoFa.TabIndex = 0;
            btnEnableTwoFa.Text = "Enable 2FA";
            btnEnableTwoFa.UseVisualStyleBackColor = true;
            // 
            // pbQr
            // 
            pbQr.Location = new Point(104, 95);
            pbQr.Name = "pbQr";
            pbQr.Size = new Size(250, 250);
            pbQr.TabIndex = 1;
            pbQr.TabStop = false;
            // 
            // pnlVerify
            // 
            pnlVerify.Controls.Add(lblEnterCode);
            pnlVerify.Controls.Add(btnOk);
            pnlVerify.Controls.Add(txtCode);
            pnlVerify.Location = new Point(50, 363);
            pnlVerify.Name = "pnlVerify";
            pnlVerify.Size = new Size(378, 59);
            pnlVerify.TabIndex = 2;
            // 
            // lblEnterCode
            // 
            lblEnterCode.AutoSize = true;
            lblEnterCode.Location = new Point(24, 22);
            lblEnterCode.Name = "lblEnterCode";
            lblEnterCode.Size = new Size(130, 20);
            lblEnterCode.TabIndex = 2;
            lblEnterCode.Text = "Enter 6 digit code:";
            // 
            // btnOk
            // 
            btnOk.Location = new Point(309, 18);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(49, 29);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            txtCode.BorderStyle = BorderStyle.FixedSingle;
            txtCode.Location = new Point(160, 20);
            txtCode.Name = "txtCode";
            txtCode.Size = new Size(125, 27);
            txtCode.TabIndex = 0;
            // 
            // rtxtBackupCodes
            // 
            rtxtBackupCodes.Location = new Point(104, 133);
            rtxtBackupCodes.Name = "rtxtBackupCodes";
            rtxtBackupCodes.Size = new Size(250, 120);
            rtxtBackupCodes.TabIndex = 3;
            rtxtBackupCodes.Text = "";
            // 
            // lblBackupCodes
            // 
            lblBackupCodes.AutoSize = true;
            lblBackupCodes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBackupCodes.Location = new Point(140, 110);
            lblBackupCodes.Name = "lblBackupCodes";
            lblBackupCodes.Size = new Size(179, 20);
            lblBackupCodes.TabIndex = 4;
            lblBackupCodes.Text = "Save your backup codes:";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 433);
            Controls.Add(lblBackupCodes);
            Controls.Add(rtxtBackupCodes);
            Controls.Add(pnlVerify);
            Controls.Add(pbQr);
            Controls.Add(btnEnableTwoFa);
            Name = "FrmMain";
            Text = "FrmMain";
            ((System.ComponentModel.ISupportInitialize)pbQr).EndInit();
            pnlVerify.ResumeLayout(false);
            pnlVerify.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnEnableTwoFa;
        private PictureBox pbQr;
        private Panel pnlVerify;
        private Label lblEnterCode;
        private Button btnOk;
        private TextBox txtCode;
        private RichTextBox rtxtBackupCodes;
        private Label lblBackupCodes;

        public Button BtnEnableTwoFa { get => btnEnableTwoFa; set => btnEnableTwoFa = value; }
        public PictureBox PbQr { get => pbQr; set => pbQr = value; }
        public Panel PnlVerify { get => pnlVerify; set => pnlVerify = value; }
        public Label LblEnterCode { get => lblEnterCode; set => lblEnterCode = value; }
        public Button BtnOk { get => btnOk; set => btnOk = value; }
        public TextBox TxtCode { get => txtCode; set => txtCode = value; }
        public RichTextBox RtxtBackupCodes { get => rtxtBackupCodes; set => rtxtBackupCodes = value; }
        public Label LblBackupCodes { get => lblBackupCodes; set => lblBackupCodes = value; }
    }
}