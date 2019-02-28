using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SapData_Automation
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.label1.Text = String.Format("Version {0}", AssemblyVersion);
        }

        private void crystalButton2_Click(object sender, EventArgs e)
        {
            var form = new NewfrmProductMain("");

            if (form.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void crystalButton6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }
      
    }
}
