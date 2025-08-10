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
            SuspendLayout();
            // 
            // btnEnableTwoFa
            // 
            btnEnableTwoFa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEnableTwoFa.Location = new Point(71, 57);
            btnEnableTwoFa.Name = "btnEnableTwoFa";
            btnEnableTwoFa.Size = new Size(94, 29);
            btnEnableTwoFa.TabIndex = 0;
            btnEnableTwoFa.Text = "Enable 2FA";
            btnEnableTwoFa.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(264, 146);
            Controls.Add(btnEnableTwoFa);
            Name = "FrmMain";
            Text = "FrmMain";
            ResumeLayout(false);
        }

        #endregion

        private Button btnEnableTwoFa;
    }
}