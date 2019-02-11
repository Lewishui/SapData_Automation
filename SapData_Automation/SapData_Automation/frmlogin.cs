using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
                OrdersControl = new OrdersControl( );
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
    }
}
