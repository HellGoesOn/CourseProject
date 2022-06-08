using System.Windows.Forms;

namespace CourseProject
{
    partial class MainForm
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
            this.deleteSelected = new System.Windows.Forms.Button();
            this.contextPanel = new System.Windows.Forms.Panel();
            this.addEntry = new System.Windows.Forms.Button();
            this.filterBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).BeginInit();
            this.tableControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainGrid
            // 
            this.MainGrid.AllowUserToAddRows = false;
            this.MainGrid.AllowUserToDeleteRows = false;
            this.MainGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MainGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.MainGrid.Location = new System.Drawing.Point(12, 42);
            this.MainGrid.MultiSelect = false;
            this.MainGrid.Name = "MainGrid";
            this.MainGrid.ReadOnly = true;
            this.MainGrid.RowTemplate.Height = 25;
            this.MainGrid.Size = new System.Drawing.Size(546, 189);
            this.MainGrid.TabIndex = 0;
            this.MainGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainGrid_CellClick);
            this.MainGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.MainGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.MainGrid_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // commitChanges
            // 
            this.commitChanges.Location = new System.Drawing.Point(564, 282);
            this.commitChanges.Name = "commitChanges";
            this.commitChanges.Size = new System.Drawing.Size(224, 30);
            this.commitChanges.TabIndex = 3;
            this.commitChanges.Text = "Зафиксировать изменения";
            this.commitChanges.UseVisualStyleBackColor = true;
            this.commitChanges.Click += new System.EventHandler(this.commitChanges_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(564, 318);
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
            this.tabPage2.Size = new System.Drawing.Size(538, 2);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // deleteSelected
            // 
            this.deleteSelected.Location = new System.Drawing.Point(564, 378);
            this.deleteSelected.Name = "deleteSelected";
            this.deleteSelected.Size = new System.Drawing.Size(224, 23);
            this.deleteSelected.TabIndex = 6;
            this.deleteSelected.Text = "Удалить запись";
            this.deleteSelected.UseVisualStyleBackColor = true;
            this.deleteSelected.Click += new System.EventHandler(this.deleteSelected_Click);
            // 
            // contextPanel
            // 
            this.contextPanel.Location = new System.Drawing.Point(12, 237);
            this.contextPanel.Name = "contextPanel";
            this.contextPanel.Size = new System.Drawing.Size(546, 201);
            this.contextPanel.TabIndex = 7;
            // 
            // addEntry
            // 
            this.addEntry.Location = new System.Drawing.Point(564, 347);
            this.addEntry.Name = "addEntry";
            this.addEntry.Size = new System.Drawing.Size(224, 25);
            this.addEntry.TabIndex = 8;
            this.addEntry.Text = "Добавить запись";
            this.addEntry.UseVisualStyleBackColor = true;
            this.addEntry.Click += new System.EventHandler(this.addEntry_Click);
            // 
            // textBox1
            // 
            this.filterBox.Location = new System.Drawing.Point(564, 36);
            this.filterBox.Name = "textBox1";
            this.filterBox.Size = new System.Drawing.Size(224, 23);
            this.filterBox.TabIndex = 9;
            this.filterBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(564, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "Фильтрация";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filterBox);
            this.Controls.Add(this.addEntry);
            this.Controls.Add(this.contextPanel);
            this.Controls.Add(this.deleteSelected);
            this.Controls.Add(this.tableControl);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.commitChanges);
            this.Controls.Add(this.MainGrid);
            this.Name = "MainForm";
            this.Text = "Контроль Выполнения Нагрузки Преподатавателей: Главная";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed_1);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MainGrid)).EndInit();
            this.tableControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MainForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #endregion

        private System.Windows.Forms.DataGridView MainGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Button commitChanges;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.TabControl tableControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button deleteSelected;
        private Panel contextPanel;
        private Button addEntry;
        private TextBox filterBox;
        private Label label1;
    }
}
