namespace CourseProject
{
    partial class ConfirmDeletionForm
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
            this.buttOk = new System.Windows.Forms.Button();
            this.buttCancel = new System.Windows.Forms.Button();
            this.bruhMomento = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttOk
            // 
            this.buttOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttOk.Location = new System.Drawing.Point(12, 147);
            this.buttOk.Name = "buttOk";
            this.buttOk.Size = new System.Drawing.Size(75, 23);
            this.buttOk.TabIndex = 0;
            this.buttOk.Text = "ОК";
            this.buttOk.UseVisualStyleBackColor = true;
            this.buttOk.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttCancel
            // 
            this.buttCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttCancel.Location = new System.Drawing.Point(163, 147);
            this.buttCancel.Name = "buttCancel";
            this.buttCancel.Size = new System.Drawing.Size(75, 23);
            this.buttCancel.TabIndex = 1;
            this.buttCancel.Text = "Отмена";
            this.buttCancel.UseVisualStyleBackColor = true;
            // 
            // bruhMomento
            // 
            this.bruhMomento.AutoSize = true;
            this.bruhMomento.Location = new System.Drawing.Point(42, 69);
            this.bruhMomento.Name = "bruhMomento";
            this.bruhMomento.Size = new System.Drawing.Size(164, 15);
            this.bruhMomento.TabIndex = 2;
            this.bruhMomento.Text = "Удалить выбранную запись?";
            this.bruhMomento.Click += new System.EventHandler(this.bruhMomento_Click);
            // 
            // ConfirmDeletionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 182);
            this.Controls.Add(this.bruhMomento);
            this.Controls.Add(this.buttCancel);
            this.Controls.Add(this.buttOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConfirmDeletionForm";
            this.Text = "Подтверждение";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttOk;
        private System.Windows.Forms.Button buttCancel;
        private System.Windows.Forms.Label bruhMomento;
    }
}