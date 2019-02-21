using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapData_Automation
{
    public partial class frmImportpath : Form
    {

        public string folderpath;

        public frmImportpath()
        {
            InitializeComponent();
        }

        private void openFileBtton_Click(object sender, EventArgs e)
        {
            //string folderpath = "";

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择sap所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                folderpath = dialog.SelectedPath;
                pathTextBox.Text  = dialog.SelectedPath;
                folderpath = pathTextBox.Text;

            }
            else
                return;
        }

        private void importButton_Click(object sender, EventArgs e)
        {
           
        }
    }
}
