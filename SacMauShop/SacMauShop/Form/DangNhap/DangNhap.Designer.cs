
namespace SacMauShop
{
    partial class DangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DangNhap));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblpassword = new System.Windows.Forms.Label();
            this.pnlpassword = new System.Windows.Forms.Panel();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.lbluser = new System.Windows.Forms.Label();
            this.pnluser = new System.Windows.Forms.Panel();
            this.txtuser = new System.Windows.Forms.TextBox();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.lblpassword);
            this.panel2.Controls.Add(this.pnlpassword);
            this.panel2.Controls.Add(this.txtpassword);
            this.panel2.Controls.Add(this.lbluser);
            this.panel2.Controls.Add(this.pnluser);
            this.panel2.Controls.Add(this.txtuser);
            this.panel2.Location = new System.Drawing.Point(30, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 258);
            this.panel2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(275, 228);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Quên mật khẩu";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblpassword
            // 
            this.lblpassword.AutoSize = true;
            this.lblpassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.7F);
            this.lblpassword.ForeColor = System.Drawing.Color.Silver;
            this.lblpassword.Location = new System.Drawing.Point(50, 177);
            this.lblpassword.Name = "lblpassword";
            this.lblpassword.Size = new System.Drawing.Size(101, 25);
            this.lblpassword.TabIndex = 5;
            this.lblpassword.Text = "Mật khẩu";
            this.lblpassword.Click += new System.EventHandler(this.lbluser_Click);
            // 
            // pnlpassword
            // 
            this.pnlpassword.BackColor = System.Drawing.Color.Silver;
            this.pnlpassword.Location = new System.Drawing.Point(50, 202);
            this.pnlpassword.Name = "pnlpassword";
            this.pnlpassword.Size = new System.Drawing.Size(300, 2);
            this.pnlpassword.TabIndex = 3;
            // 
            // txtpassword
            // 
            this.txtpassword.BackColor = System.Drawing.Color.White;
            this.txtpassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.txtpassword.Location = new System.Drawing.Point(50, 177);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '✤';
            this.txtpassword.Size = new System.Drawing.Size(300, 25);
            this.txtpassword.TabIndex = 4;
            this.txtpassword.Enter += new System.EventHandler(this.txtuser_Enter);
            this.txtpassword.Leave += new System.EventHandler(this.txtuser_Leave);
            // 
            // lbluser
            // 
            this.lbluser.AutoSize = true;
            this.lbluser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lbluser.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.7F);
            this.lbluser.ForeColor = System.Drawing.Color.Silver;
            this.lbluser.Location = new System.Drawing.Point(50, 50);
            this.lbluser.Name = "lbluser";
            this.lbluser.Size = new System.Drawing.Size(157, 25);
            this.lbluser.TabIndex = 2;
            this.lbluser.Text = "Tên đăng nhập";
            this.lbluser.Click += new System.EventHandler(this.lbluser_Click);
            // 
            // pnluser
            // 
            this.pnluser.BackColor = System.Drawing.Color.Silver;
            this.pnluser.Location = new System.Drawing.Point(50, 75);
            this.pnluser.Name = "pnluser";
            this.pnluser.Size = new System.Drawing.Size(300, 2);
            this.pnluser.TabIndex = 1;
            // 
            // txtuser
            // 
            this.txtuser.BackColor = System.Drawing.Color.White;
            this.txtuser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtuser.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.txtuser.Location = new System.Drawing.Point(50, 50);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(300, 25);
            this.txtuser.TabIndex = 1;
            this.txtuser.Enter += new System.EventHandler(this.txtuser_Enter);
            this.txtuser.Leave += new System.EventHandler(this.txtuser_Leave);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.Black;
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(168, 403);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(140, 40);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "Đăng nhập";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 106);
            this.panel1.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(412, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(23, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "_";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(441, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(73, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 72);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đăng nhập";
            // 
            // DangNhap
            // 
            this.AcceptButton = this.simpleButton1;
            this.Appearance.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 462);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.Name = "DangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblpassword;
        private System.Windows.Forms.Panel pnlpassword;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.Label lbluser;
        private System.Windows.Forms.Panel pnluser;
        private System.Windows.Forms.TextBox txtuser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}