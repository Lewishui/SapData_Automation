using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SapData_Automation
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void crystalButton2_Click(object sender, EventArgs e)
        {
            var form = new frmProductMain("");

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void crystalButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
