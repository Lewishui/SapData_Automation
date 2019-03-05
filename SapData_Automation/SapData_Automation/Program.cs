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
         

            //Application.Run(new frmlogin());
            Application.Run(new frmMain());
            //Application.Run(new nfrmProductMain(""));
          //  Application.Run(new NewfrmProductMain(""));
            //Application.Run(new test());
        }
    }
}
