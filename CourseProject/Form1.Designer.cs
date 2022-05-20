namespace CourseProject
{
    partial class Form1
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
            this.MainGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commitChanges = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.tableControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.tableControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.MainGrid.Location = new System.Drawing.Point(12, 42);
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.RowTemplate.Height = 25;
            this.MainGrid.Size = new System.Drawing.Size(546, 189);
            this.MainGrid.TabIndex = 0;
            this.MainGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.MainGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainGrid_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // commitChanges
            // 
            this.commitChanges.Location = new System.Drawing.Point(564, 38);
            this.commitChanges.Name = "commitChanges";
            this.commitChanges.Size = new System.Drawing.Size(224, 30);
            this.commitChanges.TabIndex = 3;
            this.commitChanges.Text = "Зафиксировать изменения";
            this.commitChanges.UseVisualStyleBackColor = true;
            this.commitChanges.Click += new System.EventHandler(this.commitChanges_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(564, 74);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(224, 23);
            this.updateButton.TabIndex = 4;
            this.updateButton.Text = "Внести изменения";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // tableControl
            // 
            this.tableControl.Controls.Add(this.tabPage1);
            this.tableControl.Controls.Add(this.tabPage2);
            this.tableControl.Location = new System.Drawing.Point(12, 12);
            this.tableControl.Name = "tableControl";
            this.tableControl.SelectedIndex = 0;
            this.tableControl.Size = new System.Drawing.Size(546, 30);
            this.tableControl.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(538, 2);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(538, 57);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableControl);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.commitChanges);
            this.Controls.Add(this.MainGrid);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.tableControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView MainGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button commitChanges;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.TabControl tableControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}
