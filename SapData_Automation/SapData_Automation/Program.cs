using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace SapData_Automation
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            #region Noway
            DateTime oldDate = DateTime.Now;
            DateTime dt3;
            string endday = DateTime.Now.ToString("yyyy/MM/dd");
            dt3 = Convert.ToDateTime(endday);
            DateTime dt2;
            dt2 = Convert.ToDateTime("2019/02/28");

            TimeSpan ts = dt2 - dt3;
            int timeTotal = ts.Days;
            if (timeTotal < 0)
            {
                MessageBox.Show("缺失系统文件，或电脑系统更新导致，请联系开发人员 !", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            #endregion

            //Application.Run(new frmlogin());
            Application.Run(new frmMain());
            //Application.Run(new nfrmProductMain(""));
          //  Application.Run(new NewfrmProductMain(""));
            //Application.Run(new test());
        }
    }
}
