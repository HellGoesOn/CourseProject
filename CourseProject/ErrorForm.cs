using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CourseProject
{
    public partial class ErrorForm : Form
    {
        public ErrorForm()
        {
            InitializeComponent();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {

        }

        public void SetText(string value)
        {
            this.outputBox.Text = value;
        }
    }
}
