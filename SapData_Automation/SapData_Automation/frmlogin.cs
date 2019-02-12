using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapData_Automation
{
    public partial class frmlogin : Form
    {
        public log4net.ILog ProcessLogger;
        public log4net.ILog ExceptionLogger;
        private TextBox txtSAPPassword;
        private CheckBox chkSaveInfo;
        Sunisoft.IrisSkin.SkinEngine se = null;
        frmAboutBox aboutbox;
        private System.Timers.Timer timerAlter1;
        private string ipadress;
        int logis = 0;
        private OrdersControl OrdersControl;
        //存放要显示的信息
        List<string> messages;
        //要显示信息的下标索引
        int index = 0;


        public frmlogin()
        {
            InitializeComponent();
            aboutbox = new frmAboutBox();

        }

        private void dockPanel2_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void 关于系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aboutbox.ShowDialog();
        }

        private void 导入彩票数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.scrollingText1.Visible = true;
            toolStrip1.Visible = false;


            if (OrdersControl == null)
            {
                OrdersControl = new OrdersControl();
                OrdersControl.FormClosed += new FormClosedEventHandler(FrmOMS_FormClosed);
            }
            if (OrdersControl == null)
            {
                OrdersControl = new OrdersControl();
            }
            OrdersControl.Show(this.dockPanel2);
        }
        void FrmOMS_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is OrdersControl)
            {
                OrdersControl = null;
            }
        }

        private void 下载模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ZFCEPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "System"), "");

            //System.Diagnostics.Process.Start("explorer.exe", ZFCEPath);
            string DesktopPath = Convert.ToString(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\System");
            CopyFolder(ZFCEPath, DesktopPath);

            MessageBox.Show("下载完成，请到桌面查看！");

        }
        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    File.Copy(c, destFile, true);//覆盖模式
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }
            else
            {
                throw new DirectoryNotFoundException("源目录不存在！");
            }
        }


    }
}
