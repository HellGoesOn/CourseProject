namespace CourseProject
{
    partial class LoginForm
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
            this.lgBox = new System.Windows.Forms.TextBox();
            this.pswdBox = new System.Windows.Forms.TextBox();
            this.logIn = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.pswd = new System.Windows.Forms.Label();
            this.usr = new System.Windows.Forms.Label();
            this.showPswd = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lgBox
            // 
            this.lgBox.Location = new System.Drawing.Point(130, 167);
            this.lgBox.Name = "lgBox";
            this.lgBox.Size = new System.Drawing.Size(189, 23);
            this.lgBox.TabIndex = 0;
            // 
            // pswdBox
            // 
            this.pswdBox.Location = new System.Drawing.Point(130, 217);
            this.pswdBox.Name = "pswdBox";
            this.pswdBox.PasswordChar = '*';
            this.pswdBox.Size = new System.Drawing.Size(189, 23);
            this.pswdBox.TabIndex = 1;
            // 
            // logIn
            // 
            this.logIn.Location = new System.Drawing.Point(191, 268);
            this.logIn.Name = "logIn";
            this.logIn.Size = new System.Drawing.Size(75, 23);
            this.logIn.TabIndex = 2;
            this.logIn.Text = "Войти";
            this.logIn.UseVisualStyleBackColor = true;
            this.logIn.Click += new System.EventHandler(this.logIn_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(191, 297);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(75, 23);
            this.exit.TabIndex = 3;
            this.exit.Text = "Выход";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // pswd
            // 
            this.pswd.AutoSize = true;
            this.pswd.Location = new System.Drawing.Point(130, 199);
            this.pswd.Name = "pswd";
            this.pswd.Size = new System.Drawing.Size(96, 15);
            this.pswd.TabIndex = 4;
            this.pswd.Text = "Введите пароль:";
            // 
            // usr
            // 
            this.usr.AutoSize = true;
            this.usr.Location = new System.Drawing.Point(130, 149);
            this.usr.Name = "usr";
            this.usr.Size = new System.Drawing.Size(89, 15);
            this.usr.TabIndex = 5;
            this.usr.Text = "Введите логин:";
            // 
            // showPswd
            // 
            this.showPswd.AutoSize = true;
            this.showPswd.Location = new System.Drawing.Point(130, 243);
            this.showPswd.Name = "showPswd";
            this.showPswd.Size = new System.Drawing.Size(136, 19);
            this.showPswd.TabIndex = 6;
            this.showPswd.Text = "Отображать пароль";
            this.showPswd.UseVisualStyleBackColor = true;
            this.showPswd.CheckedChanged += new System.EventHandler(this.showPswd_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(139, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 27);
            this.label1.TabIndex = 7;
            this.label1.Text = "Добро пожаловать!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(310, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Пожалуйста, авторизуйтесь чтобы продолжить работу";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CourseProject.Properties.Resources.V6;
            this.pictureBox1.Location = new System.Drawing.Point(325, 217);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(120, 106);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 322);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.showPswd);
            this.Controls.Add(this.usr);
            this.Controls.Add(this.pswd);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.logIn);
            this.Controls.Add(this.pswdBox);
            this.Controls.Add(this.lgBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoginForm";
            this.Text = "Добро пожаловать";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox lgBox;
        private System.Windows.Forms.TextBox pswdBox;
        private System.Windows.Forms.Button logIn;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Label pswd;
        private System.Windows.Forms.Label usr;
        private System.Windows.Forms.CheckBox showPswd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}