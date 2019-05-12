using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DCTS.CustomComponents;
using Order.Buiness;
using Order.Common;
using Order.DB;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.InteropServices;
using System.Timers;

namespace SapData_Automation
{
    public partial class NewfrmProductMain : DockContent
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ShowScrollBar(IntPtr hWnd, int bar, int show);

        DateTime startAt;
        DateTime endAt;
        List<clsProductinfo> Productinfolist_Server;
        int rowcount;
        string txfind;
        private SortableBindingList<clsProductinfo> sortableOrderList;
        List<int> changeindex;
        private List<string> Alist = new List<string>();
        private List<string> cacheAlist = new List<string>();
        private Hashtable dataGridChanges = null;
        private string nowfile;
        DataGridView clickdav;
        string folderpath;
        List<string> crlist;
        DataTable qtyTable_dav11;
        int Pasterow = 0;
        DataTable qtyTable_dav12;
        int jisuancishu;
        int dataGridView13_cloumncount;

        int isallsave = 0;
        int allsave_index = 0;
        bool iscache = false;

        DataTable qtyTable_dav6;
        DataTable qtyTable_dav5;
        DataTable qtyTable_dav7;
        DataTable qtyTable8;

        DataTable qtyTable_dav3;
        DataTable qtyTable_dav4;
        //DataTable qtyTable_dav5;
        bool isreopen = false;

        DataTable qtyTable_dav2;
        DataTable qtyTable_dav8;
        DataTable qtyTable_dav13;
        DataTable qtyTable_dav16;
        DataTable qtyTable_dav14;
        DataTable qtyTable_dav15;

        DataTable qtyTable_dav18;
        DataTable qtyTable_dav19;
        DataTable qtyTable_dav20;
        DataTable qtyTable_dav21;
        DataTable qtyTable_dav23;

        DataTable qtyTable_dav24;
        DataTable qtyTable_dav26;
        DataTable qtyTable_dav27;

        DataTable qtyTable_dav9;
        DataTable qtyTable_dav10;
        DataTable qtyTable_dav28;
        string cache_path = AppDomain.CurrentDomain.BaseDirectory + "cache\\";



        public NewfrmProductMain(string user)
        {
            InitializeComponent();
            this.dataGridChanges = new Hashtable();
            changeindex = new List<int>();
            this.WindowState = FormWindowState.Maximized;

            AdjustSubformSize();
            //panel5.HorizontalScroll.Visible = true;
            //panel5.HorizontalScroll.Value = panel5.HorizontalScroll.Maximum;
            //panel5.HorizontalScroll.Value = panel5.HorizontalScroll.Maximum;
        }

        private void AdjustSubformSize()
        {
            //var size = this.Parent.Size;
            //size.Height = size.Height - 100;
            //size.Width = size.Width - 50;
            //form.Size = size;

            dataGridView6.Height = 180;
            dataGridView7.Height = 180;
            dataGridView8.Height = 180;
            dataGridView9.Height = 180;


            dataGridView2.Height = 180;
            dataGridView3.Height = 180;
            dataGridView4.Height = 180;
            dataGridView5.Height = 180;


            dataGridView19.Height = 180;
            dataGridView18.Height = 180;
            dataGridView17.Height = 180;


            dataGridView24.Height = 180;
            dataGridView22.Height = 180;
            dataGridView21.Height = 180;

            //


            dataGridView25.Height = 180;
            dataGridView23.Height = 180;
            dataGridView1.Height = 180;

            dataGridView26.Height = 180;
            dataGridView27.Height = 180;
            dataGridView28.Height = 180;
            dataGridView29.Height = 180;



        }

        private void AdjustSubformSize1(Form form)
        {
            var size = this.Parent.Size;
            size.Height = size.Height - 100;
            size.Width = size.Width - 50;
            form.Size = size;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            新建ToolStripMenuItem_Click(null, EventArgs.Empty);

            //var form = new frmaddProcuct("");

            //if (form.ShowDialog() == DialogResult.OK)
            //{

            //}
            //   toolStripButton2_Click(null, EventArgs.Empty);
            if (folderpath != "")
                openfile(folderpath);
        }

        private IEnumerable<DataGridViewRow> GetSelectedRowsBySelectedCells(DataGridView dgv)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                rows.Add(cell.OwningRow);

            }
            rowcount = dgv.SelectedCells.Count;

            return rows.Distinct();
        }

        private void filterButton_Click(object sender, EventArgs e)
        {

        }

        private void BindDataGridView()
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (nowfile == null || nowfile == "")
                {

                    MessageBox.Show("请选择文件或者新建后再次尝试保存！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;


                }
                int s = this.tabControl1.SelectedIndex;
                string wtx = "";
                if (isallsave == 1)
                    s = allsave_index;

                #region control.sap

                if (s == 1)
                {
                    nowfile = Alist.Find(v => v.Contains("control.sap"));

                    //工况数

                    wtx = textBox1.Text;
                    //计算量1
                    if (radioButton1.Checked == true)
                        wtx += "\r\n\r\n" + "1";
                    else
                        wtx += "\r\n\r\n" + "0";
                    //计算量2
                    if (radioButton3.Checked == false && radioButton4.Checked == false)
                        wtx += " " + "0";
                    if (radioButton3.Checked == true)
                        wtx += " " + "1";
                    if (radioButton4.Checked == true)
                        wtx += " " + "2";
                    //计算量3

                    if (radioButton5.Checked == false && radioButton6.Checked == false)
                        wtx += " " + "0";
                    if (radioButton6.Checked == true)
                        wtx += " " + "1";
                    if (radioButton5.Checked == true)
                        wtx += " " + "2";
                    //求解器
                    wtx += "\r\n\r\n" + textBox2.Text;
                    //温度梯度
                    wtx += "\r\n\r\n" + textBox3.Text;
                    //位移约束
                    wtx += "\r\n\r\n" + textBox4.Text;
                    //最大开闭次数:
                    wtx += "\r\n\r\n" + textBox7.Text;
                    //方程迭代误差
                    wtx += "\r\n\r\n" + textBox6.Text;
                    //初始条件读入
                    wtx += "\r\n\r\n" + textBox5.Text;
                    //非线性迭代误差
                    wtx += "\r\n\r\n" + textBox10.Text;
                    //最大非线性迭代次数
                    wtx += "\r\n\r\n" + textBox9.Text;

                    //位移清0步
                    wtx += "\r\n\r\n" + textBox12.Text;
                    //惯性阻尼系数
                    wtx += "\r\n\r\n" + textBox11.Text;
                    //接续计算
                    wtx += "\r\n\r\n" + textBox14.Text;

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw.Flush();
                    sw.Close();


                    //
                    #region parallel.sap

                    var ex = Alist.Find(v => v.Contains("parallel.sap"));
                    if (ex != null && ex != "")
                    {
                        wtx = "           " + textBox8.Text;
                        wtx += "           " + textBox34.Text;
                        wtx += "          " + textBox38.Text;

                        sw = new StreamWriter(ex);
                        sw.WriteLine(wtx);
                        sw.Flush();
                        sw.Close();

                    }
                    #endregion



                    //MessageBox.Show("更新完成，请查看！");

                }
                #endregion


                #region  temp_para.sap
                else if (s == 3)
                {
                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("temp_para.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("temp_para.sap"));


                    //表面散热系数总数

                    wtx = textBox13.Text;

                    //水管总数:
                    wtx += " " + textBox15.Text;

                    //冷却期数:
                    wtx += " " + textBox16.Text;

                    //热学参数
                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw.WriteLine("");
                    sw = wxdav(nowfile, this.dataGridView2, sw);
                    sw.WriteLine("");
                    sw = wxdav(nowfile, this.dataGridView3, sw);
                    sw.WriteLine("");
                    #region old
                    //sw = wxdav(nowfile, this.dataGridView4, sw);
                    //sw.WriteLine("");
                    //sw = wxdav_cloumn2(nowfile, this.dataGridView5, sw);

                    #endregion
                    #region new 20190416
                    //sw = wxdav(nowfile, this.dataGridView4, sw);
                    //sw.WriteLine("");
                    sw = wxdav_temp_para(nowfile, this.dataGridView5, sw, dataGridView4);


                    #endregion
                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");
                }
                #endregion

                #region Els_para.sap

                else if (s == 2)
                {
                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("els_para.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("els_para.sap"));



                    wtx = textBox17.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView6, sw);
                    sw.WriteLine("");
                    sw = wxdav(nowfile, this.dataGridView7, sw);
                    sw.WriteLine("");
                    //荷载
                    if (radioButton8.Checked == true)
                        wtx = "" + "1";
                    else
                        wtx = "" + "0";
                    if (radioButton9.Checked == true)
                        wtx += " " + "1";
                    else
                        wtx += " " + "0";
                    if (radioButton10.Checked == true)
                        wtx += " " + "1";
                    else
                        wtx += " " + "0";
                    if (radioButton11.Checked == true)
                        wtx += " " + "1";
                    else
                        wtx += " " + "0";

                    //渗透力::
                    wtx += " " + textBox18.Text;
                    //自生体积变形定义点数
                    wtx += "\r\n\r\n" + textBox19.Text;

                    wtx += "\r\n";
                    sw.WriteLine(wtx);

                    //自生体积变形

                    sw = wxdav(nowfile, this.dataGridView8, sw);
                    // 
                    sw.WriteLine("");
                    //氧化镁
                    if (radioButton12.Checked == false)
                        wtx = "" + "0";
                    else if (radioButton12.Checked == true)
                        wtx = "" + "1";
                    wtx += "\r\n";
                    sw.WriteLine(wtx);


                    sw = wxdav(nowfile, this.dataGridView9, sw);

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");

                }

                #endregion

                #region 填 placement_time_of_element.sap


                else if (s == 4)
                {

                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("placement_time_of_element.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("placement_time_of_element.sap"));

                    wtx = textBox20.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_cloumn0(nowfile, this.dataGridView10, sw);

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");

                }

                #endregion
                #region 非线性参数 strength_data.sap

                else if (s == 6)
                {
                    nowfile = Alist.Find(v => v.Contains("strength_data.sap"));
                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("strength_data.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("strength_data.sap"));

                    wtx = textBox21.Text;
                    wtx += " " + textBox22.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView11, sw);
                    sw.WriteLine("");
                    sw = wxdav(nowfile, this.dataGridView12, sw);

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");

                }

                #endregion


                #region 浇筑次序 sup_step.sap

                else if (s == 7)
                {
                    var ex = Alist.Find(v => v.Contains("sup_step.sap") && !v.Contains("num_sup_step.sap"));
                    if (iscache == false)
                        ex = Alist.Find(v => v.Contains("sup_step.sap") && !v.Contains("num_sup_step.sap"));
                    else
                        ex = cacheAlist.Find(v => v.Contains("sup_step.sap") && !v.Contains("num_sup_step.sap"));

                    //wtx = textBox23.Text;
                    //wtx += "\r\n";
                    //new 删除 这个文件换成 下方独立保存
                    wtx = "";
                    StreamWriter sw = new StreamWriter(ex);
                    sw.WriteLine(wtx);
                    sw = wxdav_sup_step(nowfile, this.dataGridView13, sw);

                    sw.Flush();
                    sw.Close();

                    //new
                    #region num_sup_step.sap


                    if (iscache == false)
                        ex = Alist.Find(v => v.Contains("num_sup_step.sap"));
                    else
                        ex = cacheAlist.Find(v => v.Contains("num_sup_step.sap"));

                    if (ex != null && ex != "")
                    {
                        wtx = "           " + textBox23.Text;


                        sw = new StreamWriter(ex);
                        sw.WriteLine(wtx);
                        sw.Flush();
                        sw.Close();

                    }
                    #endregion



                    //MessageBox.Show("更新完成，请查看！");

                }

                #endregion


                #region 时步条件定义 Temp_bdy_3.sap

                else if (s == 8)
                {
                    nowfile = Alist.Find(v => v.Contains("temp_bdy_3.sap"));

                    wtx = textBox24.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_cloumn0(nowfile, this.dataGridView14, sw);

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");

                }

                #endregion

                #region 输出位移点 point_disp_output.sap


                else if (s == 9)
                {

                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("point_disp_output.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("point_disp_output.sap"));


                    wtx = textBox25.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView15, sw);

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");

                }

                #endregion


                #region  接缝单元数据 Joint_mesh.sap

                else if (s == 10)
                {
                    //nowfile = Alist.Find(v => v.Contains("joint_mesh.sap"));


                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("joint_mesh.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("joint_mesh.sap"));


                    //缝单元总数


                    wtx = textBox28.Text;

                    //缝材料总数

                    wtx += " " + textBox27.Text + "\r\n";

                    //刚度系数
                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView19, sw);
                    //强度系数
                    sw.WriteLine("");
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView18, sw);
                    //缝单元节点编
                    sw.WriteLine("");
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView17, sw);

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");
                }
                #endregion

                #region 灌浆数据 grouting_step.sap

                else if (s == 11)
                {
                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("grouting_step.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("grouting_step.sap"));

                    wtx = textBox26.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_cloumn0(nowfile, this.dataGridView16, sw);

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");

                }

                #endregion

                #region  给定节点温度 Temp_fix.sap


                else if (s == 12)
                {
                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("temp_fix.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("temp_fix.sap"));

                    //缝单元总数


                    wtx = textBox29.Text;

                    //缝材料总数

                    wtx += " " + textBox30.Text + "\r\n";

                    //刚度系数
                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView20, sw);

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");
                }
                #endregion

                #region  库水河水温度 Temp_water.sap

                else if (s == 13)
                {

                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("temp_water.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("temp_water.sap"));

                    //初次蓄水日期


                    wtx = textBox32.Text;

                    //蓄水结束日期

                    wtx += " " + textBox31.Text;

                    //库水温数据行（水深）数

                    wtx += "\r\n\r\n" + textBox33.Text + "\r\n";

                    //库水温信息

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView24, sw);

                    sw.WriteLine("");
                    //下游水温类型:
                    wtx = textBox36.Text;

                    //泄水孔高程:

                    wtx += " " + textBox37.Text;

                    //下游水温表数据行数:

                    wtx += "\r\n\r\n" + textBox35.Text + "\r\n";

                    sw.WriteLine(wtx);
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView22, sw);
                    sw.WriteLine("");
                    sw = wxdav_Temp_water(nowfile, this.dataGridView21, sw);

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");
                }
                #endregion


                #region  渗流参数 seepage_data.sap

                else if (s == 14)
                {

                    if (iscache == false)
                        nowfile = Alist.Find(v => v.Contains("seepage_data.sap"));
                    else
                        nowfile = cacheAlist.Find(v => v.Contains("seepage_data.sap"));



                    wtx = " " + textBox40.Text;
                    wtx += "\r\n";
                    //饱和渗透系数
                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_cloumn0(nowfile, this.dataGridView25, sw);

                    sw.WriteLine("");

                    wtx = textBox43.Text;
                    wtx += " " + textBox42.Text;
                    wtx += " " + textBox44.Text;
                    wtx += "\r\n\r\n" + textBox39.Text + "\r\n";

                    //每组数据行数

                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView23, sw);
                    sw.WriteLine("");
                    //吸力-饱和度-相对渗透系数

                    //sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView1, sw);
                    sw.WriteLine("");

                    //不透水面个数
                    wtx = textBox45.Text + "\r\n";

                    sw.WriteLine(wtx);
                    sw = wxdav_cloumn0(nowfile, this.dataGridView26, sw);
                    sw.WriteLine("");
                    //已知水位点数

                    wtx = textBox48.Text + "\r\n";

                    sw.WriteLine(wtx);
                    sw = wxdav_cloumn0(nowfile, this.dataGridView27, sw);
                    sw.WriteLine("");
                    //不透水点个数
                    wtx = textBox51.Text + "\r\n";

                    sw.WriteLine(wtx);
                    sw = wxdav_cloumn0(nowfile, this.dataGridView28, sw);
                    sw.WriteLine("");
                    //可能溢出面个数
                    wtx = textBox54.Text + "\r\n";

                    sw.WriteLine(wtx);
                    sw = wxdav_cloumn0(nowfile, this.dataGridView29, sw);
                    sw.WriteLine("");

                    sw.Flush();
                    sw.Close();
                    //MessageBox.Show("更新完成，请查看！");

                }
                #endregion
                //如批量保存不每次都读取一次
                if (folderpath != null && folderpath != "" && iscache == false && isallsave == 0)
                {
                    openfile(folderpath);
                    this.toolStripLabel1.Text = "刷新完成";
                }
                if (s > 0)
                {


                    if (isallsave == 0)
                        MessageBox.Show("更新完成，请查看！");
                    isallsave = 0;

                }
            }
            catch (Exception ex)
            {
                dataGridChanges.Clear();
                return;
                throw;
            }
        }

        private StreamWriter wxdav(string strFileName, DataGridView dav, StreamWriter sw)
        {
            //FileStream fa = new FileStream(strFileName, FileMode.Create);
            //  sw = new StreamWriter(fa, Encoding.Unicode);
            string delimiter = "\t";
            string strHeader = " ";
            for (int i = 0; i < dav.Columns.Count; i++)
            {
                strHeader += dav.Columns[i].HeaderText + delimiter;
            }
            //  sw.WriteLine(strHeader);

            //output rows data
            strHeader = " ";
            for (int j = 0; j < dav.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 1; k < dav.Columns.Count; k++)
                {
                    if (dav.Rows[j].Cells[k].Value != null)
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;
                        if (dav.Rows[j].Cells[k].Value.ToString() == "LIP201507-35")
                        {

                        }
                        int count = strRowValue.Length - strRowValue.Replace("\t", "").Length;
                    }
                    else
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value + delimiter;
                    }
                }
                //    strRowValue += "\r\n";
                if (strRowValue.Replace("\t", "").Length != 0)
                    sw.WriteLine(strRowValue);
            }
            return sw;

        }
        private StreamWriter wxdav_cloumn0(string strFileName, DataGridView dav, StreamWriter sw)
        {
            //FileStream fa = new FileStream(strFileName, FileMode.Create);
            //  sw = new StreamWriter(fa, Encoding.Unicode);
            string delimiter = "\t";
            string strHeader = "";
            for (int i = 0; i < dav.Columns.Count; i++)
            {
                strHeader += dav.Columns[i].HeaderText + delimiter;
            }
            //  sw.WriteLine(strHeader);

            //output rows data
            for (int j = 0; j < dav.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 0; k < dav.Columns.Count; k++)
                {
                    if (dav.Rows[j].Cells[k].Value != null)
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;
                        if (dav.Rows[j].Cells[k].Value.ToString() == "LIP201507-35")
                        {

                        }

                    }
                    else
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value + delimiter;
                    }
                }
                sw.WriteLine(strRowValue);
            }
            return sw;
            //sw.Close();
            //fa.Close();
            //   MessageBox.Show("Dear User, Down File  Successful ！", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private StreamWriter wxdav_cloumn2(string strFileName, DataGridView dav, StreamWriter sw)
        {
            //FileStream fa = new FileStream(strFileName, FileMode.Create);
            //  sw = new StreamWriter(fa, Encoding.Unicode);
            string delimiter = "\t";
            string strHeader = " ";
            for (int i = 0; i < dav.Columns.Count; i++)
            {
                strHeader += dav.Columns[i].HeaderText + delimiter;
            }
            //  sw.WriteLine(strHeader);

            //output rows data
            strHeader = " ";
            for (int j = 0; j < dav.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 2; k < dav.Columns.Count; k++)
                {
                    if (dav.Rows[j].Cells[k].Value != null)
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;
                        if (dav.Rows[j].Cells[k].Value.ToString() == "LIP201507-35")
                        {

                        }

                    }
                    else
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value + delimiter;
                    }
                }
                //    strRowValue += "\r\n";
                sw.WriteLine(strRowValue);
            }
            return sw;

        }
        private StreamWriter wxdav_temp_para(string strFileName, DataGridView dav, StreamWriter sw, DataGridView dav2)
        {
            //FileStream fa = new FileStream(strFileName, FileMode.Create);
            //  sw = new StreamWriter(fa, Encoding.Unicode);
            string delimiter = "\t";
            string strHeader = " ";
            for (int i = 0; i < dav.Columns.Count; i++)
            {
                strHeader += dav.Columns[i].HeaderText + delimiter;
            }
            //  sw.WriteLine(strHeader);

            //output rows data
            strHeader = " ";
            for (int x = 0; x < dav2.Rows.Count; x++)//水管定义
            {

                //水管定义行数据
                string strRowValue = "";

                for (int k = 1; k < dav2.Columns.Count; k++)
                {
                    if (dav2.Rows[x].Cells[k].Value != null)
                    {
                        strRowValue += dav2.Rows[x].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;

                    }
                    else
                    {
                        strRowValue += dav2.Rows[x].Cells[k].Value + delimiter;
                    }
                }
                if (x > 0)
                    sw.WriteLine("");
                sw.WriteLine(strRowValue);
                // sw.WriteLine("");
                for (int j = 0; j < dav.Rows.Count; j++)//通水
                {
                    strRowValue = "";
                    if (dav.Rows[j].Cells[0].Value != null && dav.Rows[j].Cells[0].Value.ToString().Trim() == dav2.Rows[x].Cells[0].Value.ToString().Trim())
                    {
                        for (int k = 2; k < dav.Columns.Count; k++)
                        {

                            if (dav.Rows[j].Cells[k].Value != null)
                            {
                                strRowValue += dav.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;
                                if (dav.Rows[j].Cells[k].Value.ToString() == "LIP201507-35")
                                {

                                }

                            }
                            else
                            {
                                strRowValue += dav.Rows[j].Cells[k].Value + delimiter;
                            }
                        }

                        //    strRowValue += "\r\n";
                        sw.WriteLine(strRowValue);
                    }
                }
            }
            return sw;

        }

        private StreamWriter wxdav_sup_step(string strFileName, DataGridView dav, StreamWriter sw)
        {
            //FileStream fa = new FileStream(strFileName, FileMode.Create);
            //  sw = new StreamWriter(fa, Encoding.Unicode);
            string delimiter = "\t";
            string delimiter2 = "\r\n";

            string strHeader = "";
            for (int i = 0; i < dav.Columns.Count; i++)
            {
                strHeader += dav.Columns[i].HeaderText + delimiter;
            }
            //  sw.WriteLine(strHeader);

            //output rows data
            for (int j = 0; j < dav.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 1; k < dav.Columns.Count; k++)
                {
                    if (k < 4)
                    {
                        if (dav.Rows[j].Cells[k].Value != null)
                            strRowValue += dav.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;
                        else
                            strRowValue += dav.Rows[j].Cells[k].Value + delimiter;
                    }
                    else
                    {
                        if (k == 4)
                        {
                            strRowValue += delimiter2;

                        }
                        if (dav.Rows[j].Cells[k].Value != null)
                        {
                            strRowValue += dav.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;


                        }
                        else
                        {
                            strRowValue += dav.Rows[j].Cells[k].Value + delimiter;
                        }
                    }
                }
                sw.WriteLine(strRowValue);
            }
            return sw;
            //sw.Close();
            //fa.Close();
            //   MessageBox.Show("Dear User, Down File  Successful ！", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private StreamWriter wxdav_Joint_mesh(string strFileName, DataGridView dav, StreamWriter sw)
        {

            string delimiter = "\t";
            string strHeader = "";
            for (int i = 0; i < dav.Columns.Count; i++)
            {
                strHeader += dav.Columns[i].HeaderText + delimiter;
            }

            for (int j = 0; j < dav.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 1; k < dav.Columns.Count; k++)
                {
                    if (dav.Rows[j].Cells[k].Value != null)
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;


                    }
                    else
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value + delimiter;
                    }
                }
                sw.WriteLine(strRowValue);
            }
            return sw;

        }
        private StreamWriter wxdav_Temp_water(string strFileName, DataGridView dav, StreamWriter sw)
        {

            string delimiter = "\t";
            string strHeader = "";
            for (int i = 0; i < dav.Columns.Count; i++)
            {
                strHeader += dav.Columns[i].HeaderText + delimiter;
            }

            for (int j = 0; j < dav.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 1; k < dav.Columns.Count; k++)
                {
                    strRowValue = k + delimiter;

                    if (dav.Rows[j].Cells[k].Value != null)
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;


                    }
                    else
                    {
                        strRowValue += dav.Rows[j].Cells[k].Value + delimiter;
                    }

                    sw.WriteLine(strRowValue);
                }
                break;

            }
            return sw;

        }

        private IEnumerable<int> GetChangedOrderIds()
        {

            List<int> rows = new List<int>();
            foreach (DictionaryEntry entry in dataGridChanges)
            {
                var key = entry.Key as string;
                if (key.EndsWith("_changed"))
                {
                    int row = Int32.Parse(key.Split('_')[0]);
                    rows.Add(row);
                }

            }
            return rows.Distinct();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                string cell_key = e.RowIndex.ToString() + "_" + e.ColumnIndex.ToString() + "_changed";

                if (dataGridChanges.ContainsKey(cell_key))
                {
                    e.CellStyle.BackColor = Color.Red;
                    e.CellStyle.SelectionBackColor = Color.DarkRed;

                }
            }
            catch (Exception ex)
            {


            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WorkerArgument arg = e.UserState as WorkerArgument;
            if (!arg.HasError)
            {
                this.toolStripLabel1.Text = String.Format("{0}/{1}", arg.CurrentIndex, arg.OrderCount);
                this.ProgressValue = e.ProgressPercentage;
            }
            else
            {
                this.toolStripLabel1.Text = arg.ErrorMessage;
            }

        }

        public int ProgressValue
        {
            get { return this.pbStatus.Value; }
            set { pbStatus.Value = value; }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (folderpath != null && folderpath != "")
            {
                isreopen = true;//不执行tx change

                openfile(folderpath);
            }
            isreopen = false;

            MessageBox.Show("还原完成！");



            return;

            int s = this.tabControl1.SelectedIndex;
            if (s == 1)
            {

                //工况数
                textBox1.Text = "";
                radioButton1.Checked = false;

                radioButton3.Checked = false;

                radioButton4.Checked = false;

                radioButton6.Checked = false;

                radioButton5.Checked = false;
                //求解器
                textBox2.Text = "";
                //温度梯度
                textBox3.Text = "";

                //位移约束
                textBox4.Text = "";

                //最大开闭次数:
                textBox7.Text = "";

                //方程迭代误差
                textBox6.Text = "";

                //初始条件读入
                textBox5.Text = "";

                //非线性迭代误差
                textBox10.Text = "";

                //最大非线性迭代次数
                textBox9.Text = "";
                //位移清0步
                textBox12.Text = "";
                textBox11.Text = "";
                textBox14.Text = "";
            }
            else if (s == 2)
            {

                textBox17.Text = "";
                dataGridView6.DataSource = null;
                dataGridView7.DataSource = null;
                radioButton8.Checked = false;
                radioButton9.Checked = false;
                radioButton10.Checked = false;
                radioButton11.Checked = false;
                textBox18.Text = "";
                textBox19.Text = "";
                radioButton12.Checked = false;
                dataGridView8.DataSource = null;
            }
            else if (s == 3)
            {

                textBox13.Text = "";
                textBox15.Text = "";
                textBox16.Text = "";
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                dataGridView4.DataSource = null;
                dataGridView5.DataSource = null;


            }
            else if (s == 4)
            {

                textBox20.Text = "";

                dataGridView10.DataSource = null;

            }
            else if (s == 6)
            {

                textBox21.Text = "";
                textBox22.Text = "";
                dataGridView11.DataSource = null;
                dataGridView12.DataSource = null;

            }
            else if (s == 7)
            {

                textBox23.Text = "";

                dataGridView13.DataSource = null;


            }
            else if (s == 8)
            {

                textBox24.Text = "";

                dataGridView14.DataSource = null;
            }
            else if (s == 9)
            {
                textBox25.Text = "";
                dataGridView15.DataSource = null;
            }
            else if (s == 10)
            {
                textBox27.Text = "";
                textBox28.Text = "";
                dataGridView17.DataSource = null;
                dataGridView18.DataSource = null;
                dataGridView19.DataSource = null;
            }
            else if (s == 11)
            {
                textBox26.Text = "";
                dataGridView16.DataSource = null;
            }
            else if (s == 12)
            {
                textBox29.Text = "";
                textBox30.Text = "";
                dataGridView20.DataSource = null;

            }
            else if (s == 13)
            {
                textBox32.Text = "";
                textBox31.Text = "";
                textBox33.Text = "";
                dataGridView24.DataSource = null;
                textBox35.Text = "";
                textBox36.Text = "";
                textBox37.Text = "";
                dataGridView22.DataSource = null;
                dataGridView21.DataSource = null;
            }


        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //bool handle;
            //if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.Equals(DBNull.Value))
            //{
            //    handle = true;
            //}
            //else
            //    handle = false;
            //e.Cancel = handle;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderpath = "";

            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择生成目标文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                folderpath = dialog.SelectedPath;

            }
            else
                return;


            AddSapList();


            //string path = AppDomain.CurrentDomain.BaseDirectory + "System\\IP.txt";
            string path = AppDomain.CurrentDomain.BaseDirectory + "System\\";

            for (int i = 0; i < crlist.Count; i++)
            {

                //File.Create(folderpath + "\\" + crlist[i]);
                //StreamWriter sw = new StreamWriter(folderpath + "\\" + crlist[i]);
                //sw.WriteLine("");
                //sw.Flush();
                //sw.Close();
                if (!File.Exists(folderpath + "\\" + crlist[i]))
                {
                    File.Create(folderpath + "\\" + crlist[i]).Close();

                }


            }
            MessageBox.Show(this, "创建完成!", "提示");

        }

        private void AddSapList()
        {
            crlist = new List<string>();

            crlist.Add("control.sap");
            crlist.Add("dummy_material.sap");
            crlist.Add("e_by_table.sap");
            crlist.Add("els_para.sap");
            crlist.Add("joint_mesh.sap");

            crlist.Add("mesh.sap");
            crlist.Add("placement_time_of_element.sap");
            crlist.Add("seepage_data.sap");
            crlist.Add("strength.sap");
            crlist.Add("strength_data.sap");
            crlist.Add("sup_step.sap");
            crlist.Add("temp_bdy_3.sap");
            crlist.Add("temp_para.sap");
            crlist.Add("temp_water.sap");
            crlist.Add("point_disp_output.sap");
            crlist.Add("grouting_step.sap");
            crlist.Add("temp_fix.sap");
            crlist.Add("parallel.sap");



        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string folderpath = "";

            //System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            //dialog.Description = "请选择sap所在文件夹";
            //if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    if (string.IsNullOrEmpty(dialog.SelectedPath))
            //    {
            //        MessageBox.Show(this, "文件夹路径不能为空", "提示");
            //        return;
            //    }
            //    folderpath = dialog.SelectedPath;

            //}
            //else
            //    return;

            var form = new frmImportpath();

            if (form.ShowDialog() == DialogResult.OK)
            {
                folderpath = form.folderpath;


            }
            if (folderpath != null && folderpath != "")
            {
                //   toolStripButton2_Click(null, EventArgs.Empty);

                openfile(folderpath);


                label107.Text = folderpath;
                label108.Text = Path.GetFileNameWithoutExtension(folderpath);

                var ex = Alist.Find(v => v.Contains("mesh.sap") && !v.Contains("joint_mesh.sap"));
                if (ex != null && ex != "")
                {

                    string[] fileText = File.ReadAllLines(ex);

                    string sp_txt = "";

                    string wtx = "";
                    if (fileText.Length > 0)
                    {
                        sp_txt = removeblank(sp_txt, fileText, 0);
                        //new 
                        sp_txt = removeblank_txt(sp_txt);

                        //单元数
                        string[] fileTextG = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                        if (fileTextG.Length <= 1)
                            fileTextG = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                        if (fileTextG.Length > 1)
                            label110.Text = fileTextG[0].Trim();
                        if (fileTextG.Length > 1)
                            label111.Text = fileTextG[1].Trim();
                    }

                }

                MessageBox.Show("读取完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void openfile(string folderpath)
        {
            clsAllnew BusinessHelp = new clsAllnew();
            Alist = new List<string>();
            cacheAlist = new List<string>();

            Alist = BusinessHelp.GetBy_CategoryReportFileName(folderpath);

            cacheAlist = BusinessHelp.GetBy_CategoryReportFileName(cache_path);

            //var filtered = Alist.FindAll(s => s[] == oids[j]);

            AddSapList();
            for (int i = 0; i < crlist.Count; i++)
            {
                var ex = Alist.Find(v => v.Contains(crlist[i]));
                if (ex == null || ex == "")
                {
                    // File.Create(folderpath + "\\" + crlist[i]);

                    if (!File.Exists(folderpath + "\\" + crlist[i]))
                    {
                        File.Create(folderpath + "\\" + crlist[i]).Close();

                    }

                }
            }


            #region   调用  转换程序.exe

            string DesktopPath = AppDomain.CurrentDomain.BaseDirectory + "";

            if (File.Exists(DesktopPath + "\\转换程序.exe"))
            {
                //File.Copy(DesktopPath + "\\转换程序.exe", folderpath + "\\转换程序.exe", true);//覆盖模式

                //System.Diagnostics.Process.Start(folderpath + "\\转换程序.exe").WaitForExit();
                //System.Threading.Thread.Sleep(1500);
                //File.Delete(folderpath + "\\转换程序.exe");
                //System.Diagnostics.Process.Start(folderpath + "\\转换程序.exe", folderpath);

                if (File.Exists(folderpath + "\\run.bat"))
                    File.Delete(folderpath + "\\run.bat");
                if (!File.Exists(folderpath + "\\run.bat"))
                {
                    //  File.Create(folderpath + "\\run.bat" ).Close();
                    FileStream fs = new FileStream(folderpath + "\\run.bat", FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                    sw.WriteLine("\""+DesktopPath + "转换程序.exe"+"\"");
                    sw.Flush();
                    sw.Close();

                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.WorkingDirectory = folderpath;
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.FileName = folderpath + "\\run.bat";
                    p.Start();
                    p.WaitForExit();


                    if (File.Exists(folderpath + "\\run.bat"))
                        File.Delete(folderpath + "\\run.bat");
                }
            }
            else
            {
                if (File.Exists(DesktopPath + "\\转换程序.exe.lnk"))
                    System.Diagnostics.Process.Start(DesktopPath + "\\转换程序.exe.lnk", folderpath);
                else
                    MessageBox.Show(DesktopPath + "路径为空 或 在系统根目录路径下没有找到 转换程序.exe", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion


            //  System.Threading.Thread.Sleep(1000);
            Gettab1();
            toolStripLabel1.Text = "已读取完成";

        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("control.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 1;


        }

        private void Gettab1()
        {
            for (int i = 0; i < Alist.Count; i++)
            {

                string sp_txt = "";

                #region 计算控制文件  control.sap

                if (Alist[i].Contains("control.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    // string[] fileText1 = System.Text.RegularExpressions.Regex.Split(UserResult[0].salse_code, " ");

                    string wtx = "";
                    if (fileText.Length > 0)
                    {

                        sp_txt = removeblank(sp_txt, fileText, 0);
                        //new 
                        sp_txt = removeblank_txt(sp_txt);

                        //工况数
                        string[] fileTextG = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                        if (fileTextG.Length < 1)
                            fileTextG = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                        textBox1.Text = fileTextG[0].Trim();

                        //计算量1
                        if (fileText.Length > 1)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 2);
                            //new 
                            sp_txt = removeblank_txt(sp_txt);
                            string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                            if (fileTextG.Length < 1)
                                fileTextG = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");


                            if (fileText1.Length > 0 && fileText1[0] == "1")
                                radioButton1.Checked = true;
                            else if (fileText1.Length > 1 && fileText1[0] == "0")
                                radioButton1.Checked = false;
                            //计算量2

                            if (fileText1.Length > 1 && fileText1[1] == "1")
                                radioButton3.Checked = true;
                            if (fileText1.Length > 1 && fileText1[1] == "2")
                                radioButton4.Checked = true;

                            //计算量3

                            if (fileText1.Length > 2 && fileText1[2] == "1")
                                radioButton6.Checked = true;
                            if (fileText1.Length > 2 && fileText1[2] == "2")
                                radioButton5.Checked = true;
                        }
                        //求解器
                        sp_txt = removeblank(sp_txt, fileText, 4);
                        //new 
                        sp_txt = removeblank_txt(sp_txt);

                        string[] fileTextQ = splittx0(sp_txt);
                        if (fileTextQ.Length < 1)
                            fileTextQ = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");

                        textBox2.Text = fileTextQ[0].Trim();


                        //温度梯度
                        if (fileText.Length > 6)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 6);

                            fileTextQ = splittx0(sp_txt);
                            textBox3.Text = fileTextQ[0].Trim();
                        }
                        //位移约束
                        if (fileText.Length > 8)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 8);

                            fileTextQ = splittx0(sp_txt);
                            textBox4.Text = fileTextQ[0].Trim();
                        }
                        //最大开闭次数:
                        if (fileText.Length > 10)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 10);

                            fileTextQ = splittx0(sp_txt);
                            textBox7.Text = fileTextQ[0].Trim();
                        }
                        //方程迭代误差
                        if (fileText.Length > 12)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 12);

                            fileTextQ = splittx0(sp_txt);
                            textBox6.Text = fileTextQ[0].Trim();
                        }
                        //初始条件读入
                        if (fileText.Length > 14)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 14);

                            fileTextQ = splittx0(sp_txt);
                            textBox5.Text = fileTextQ[0].Trim();
                        }
                        //非线性迭代误差
                        if (fileText.Length > 16)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 16);

                            fileTextQ = splittx0(sp_txt);
                            textBox10.Text = fileTextQ[0].Trim();
                        }
                        //最大非线性迭代次数
                        if (fileText.Length > 18)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 18);

                            fileTextQ = splittx0(sp_txt);
                            textBox9.Text = fileTextQ[0].Trim();
                        }

                        //位移清0步
                        if (fileText.Length > 20)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 20);

                            fileTextQ = splittx0(sp_txt);
                            textBox12.Text = fileTextQ[0].Trim();
                        }
                        //惯性阻尼系数
                        if (fileText.Length > 22)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 22);

                            fileTextQ = splittx0(sp_txt);
                            textBox11.Text = fileTextQ[0].Trim();
                        }
                        //接续计算
                        if (fileText.Length > 24)
                        {
                            sp_txt = removeblank(sp_txt, fileText, 24);

                            fileTextQ = splittx0(sp_txt);
                            textBox14.Text = fileTextQ[0].Trim();
                        }

                    }
                }
                #endregion


                #region 基本材料参数 els_para.sap
                else if (Alist[i].Contains("els_para.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    sp_txt = read_els_para(sp_txt, fileText);

                }
                #endregion

                #region  热学参数 temp_para.sap
                else if (Alist[i].Contains("temp_para.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    sp_txt = Read_temp_para(sp_txt, fileText);


                }
                #endregion

                #region 挖除与回填 placement_time_of_element.sap


                else if (Alist[i].Contains("placement_time_of_element.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    sp_txt = Read_placement_time_of_element(sp_txt, fileText);
                }

                #endregion


                #region 非线性参数 strength_data.sap
                else if (Alist[i].Contains("strength_data.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    sp_txt = Read_strength_data(sp_txt, fileText);
                }

                #endregion


                #region 浇筑次序 sup_step.sap
                else if (Alist[i].Contains("sup_step.sap") && !Alist[i].Contains("num_sup_step.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    sp_txt = Read_sup_step(sp_txt, fileText);
                }


                #endregion


                #region 时步条件定义 Temp_bdy_3.sap


                else if (Alist[i].Contains("temp_bdy_3.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    int ongo = 0;

                    if (fileText.Length > 1)
                    {

                        sp_txt = removeblank(sp_txt, fileText, 0);
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                        //挖除与回填单元总数::
                        if (fileText1.Length > 0)
                            textBox24.Text = fileText1[0].Trim();
                    }

                    qtyTable_dav12 = new DataTable();
                    int icount = 10;
                    for (int i11 = 1; i11 <= icount; i11++)
                    {
                        qtyTable_dav12.Columns.Add("T" + i11, System.Type.GetType("System.String"));//0

                    }
                    for (int i11 = 1; i11 <= icount; i11++)
                    {
                        qtyTable_dav12.Columns.Add("nβ" + i11, System.Type.GetType("System.String"));//0

                    }
                    qtyTable_dav12.Columns.Add("zu", System.Type.GetType("System.String"));//0
                    qtyTable_dav12.Columns.Add("zd", System.Type.GetType("System.String"));//1
                    qtyTable_dav12.Columns.Add("zm", System.Type.GetType("System.String"));//2

                    qtyTable_dav12.Columns.Add("ov1", System.Type.GetType("System.String"));//0
                    qtyTable_dav12.Columns.Add("ov2", System.Type.GetType("System.String"));//1
                    qtyTable_dav12.Columns.Add("dc1", System.Type.GetType("System.String"));//2
                    qtyTable_dav12.Columns.Add("dc2", System.Type.GetType("System.String"));//3


                    for (int ir = 0; ir < jisuancishu; ir++)
                        qtyTable_dav12.Rows.Add(qtyTable_dav12.NewRow());


                    int ongo1 = ongo + 1;
                    int rowindex = 0;
                    int isgo = 0;

                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;
                        sp_txt = removeblank(sp_txt, fileText, j);
                        sp_txt = tongyi_tempty(sp_txt);

                        if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                        {
                            isgo++;
                            if (isgo > 1 || rowindex > 0)
                                break;
                            else
                                continue;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;
                        if (qtyTable_dav12.Rows.Count <= rowindex)
                            qtyTable_dav12.Rows.Add(qtyTable_dav12.NewRow());


                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj < qtyTable_dav12.Columns.Count && rowindex < qtyTable_dav12.Rows.Count)

                                qtyTable_dav12.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }
                    this.bindingSource14.DataSource = qtyTable_dav12;
                    this.dataGridView14.DataSource = this.bindingSource14;
                }
                #endregion


                #region 输出位移点 point_disp_output.sap

                else if (Alist[i].Contains("point_disp_output.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    sp_txt = Read_point_disp_output(sp_txt, fileText);
                }
                #endregion


                #region  接缝单元数据 Joint_mesh.sap

                else if (Alist[i].Contains("joint_mesh.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    sp_txt = Read_joint_mesh(sp_txt, fileText);
                }
                #endregion


                #region 灌浆数据 grouting_step.sap
                else if (Alist[i].Contains("grouting_step.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    sp_txt = Read_grouting_step(sp_txt, fileText);
                }


                #endregion

                #region 给定节点温度 Temp_fix.sap

                else if (Alist[i].Contains("temp_fix.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    sp_txt = Read_temp_fix(sp_txt, fileText);
                }


                #endregion


                #region  库水河水温度 Temp_water.sap

                else if (Alist[i].Contains("temp_water.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    sp_txt = Read_temp_water(sp_txt, fileText);



                }
                #endregion


                #region parallel.sap
                else if (Alist[i].Contains("parallel.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);

                    if (fileText.Length > 1)
                    {
                        sp_txt = removeblank(sp_txt, fileText, 0);
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");

                        //初次蓄水日期

                        if (fileText1.Length > 0)
                            textBox8.Text = fileText1[0].Trim();

                        //蓄水结束日期

                        if (fileText1.Length > 1)
                            textBox34.Text = fileText1[1].Trim();

                        //库水温数据行（水深）数

                        if (fileText.Length > 1)
                            textBox38.Text = fileText1[2].Trim();


                    }



                }


                #endregion

                #region num_sup_step.sap
                else if (Alist[i].Contains("num_sup_step.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    sp_txt = Read_num_sup_step(sp_txt, fileText);


                }
                #endregion

                #region 渗流参数 seepage_data.sap

                else if (Alist[i].Contains("seepage_data.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    sp_txt = Read_seepage_data(sp_txt, fileText);
                }


                #endregion
            }
        }

        private string Read_strength_data(string sp_txt, string[] fileText)
        {
            int ongo = 0;

            if (fileText.Length > 1)
            {

                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                //分析类型::
                if (fileText1.Length > 0)
                    textBox21.Text = fileText1[0].Trim();
                //材料参数总数:::
                if (fileText1.Length > 1)
                    textBox22.Text = fileText1[1].Trim();

            }
            //强度参数
            qtyTable_dav9 = new DataTable();
            feixianxing_qiangduxishu(qtyTable_dav9);

            int ongo1 = ongo + 1;
            int rowindex = 0;
            int isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);

                sp_txt = removeblank_txt(sp_txt);
                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;

                    //break;
                }
                if (fileText[j] == "" && j == 1)
                    continue;

                qtyTable_dav9.Rows.Add(qtyTable_dav9.NewRow());


                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav9.Columns.Count - 1 && rowindex < qtyTable_dav9.Rows.Count)
                        qtyTable_dav9.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                qtyTable_dav9.Rows[rowindex][0] = rowindex + 1;

                rowindex++;

            }
            //损伤与软化系数
            qtyTable_dav10 = new DataTable();
            shunshangyuruanhuaxishu(qtyTable_dav10);
            isgo = 0;

            ongo1 = ongo + 1;
            rowindex = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;

                sp_txt = removeblank(sp_txt, fileText, j);

                sp_txt = removeblank_txt(sp_txt);


                //if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                //{
                //    break;
                //}
                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 2 || rowindex > 0)
                        break;
                    else
                        continue;

                    //break;
                }
                if (fileText[j] == "" && j == 1)
                    continue;

                qtyTable_dav10.Rows.Add(qtyTable_dav10.NewRow());
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav10.Columns.Count - 1 && rowindex < qtyTable_dav10.Rows.Count)
                        qtyTable_dav10.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                qtyTable_dav10.Rows[rowindex][0] = rowindex + 1;

                rowindex++;

            }


            this.bindingSource11.DataSource = qtyTable_dav9;
            this.dataGridView11.DataSource = this.bindingSource11;

            this.bindingSource12.DataSource = qtyTable_dav10;

            this.dataGridView12.DataSource = this.bindingSource12;
            return sp_txt;
        }

        private string Read_seepage_data(string sp_txt, string[] fileText)
        {
            try
            {
                if (fileText.Length > 1)
                {

                    sp_txt = removeblank(sp_txt, fileText, 0);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);

                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");

                    //渗流材料种数

                    if (fileText1.Length > 0)
                        textBox40.Text = fileText1[0].Trim();

                }

                qtyTable_dav23 = new DataTable();
                baoheshentouxishu(qtyTable_dav23);
                if (textBox40.Text.Length > 0)
                {
                    if (!textBox40.Text.Contains(" ") && !textBox40.Text.Contains("\t"))
                    {
                        int icount = Convert.ToInt32(textBox40.Text);
                        for (int iq = 1; iq <= icount; iq++)
                        {

                            qtyTable_dav23.Rows.Add(qtyTable_dav23.NewRow());

                        }
                    }
                }
                int ongo = 0;
                int rowindex = 0;
                for (int j = 1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);

                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j] == "" || sp_txt == "")
                    {
                        if (j > 2)
                            break;
                        else
                            continue;

                    }
                    if (fileText[j] == "" && j == 1)
                        continue;

                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                    for (int jj = 0; jj < fileText1.Length; jj++)
                    {
                        if (jj < qtyTable_dav23.Columns.Count && rowindex < qtyTable_dav23.Rows.Count)
                            qtyTable_dav23.Rows[rowindex][jj] = fileText1[jj];
                    }
                    //if (rowindex < qtyTable_dav23.Rows.Count)
                    //    qtyTable_dav23.Rows[rowindex][0] = rowindex + 1;
                    rowindex++;


                }

                int ongo1 = ongo + 1;
                // ongo1 = 3;
                rowindex = 0;
                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if ((fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                    {
                        break;
                    }
                    if (fileText[j] == "" && j == 1)
                        continue;

                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                    //地下水位:
                    if (fileText1.Length > 0)
                        textBox43.Text = fileText1[0].Trim();
                    //初始饱和度:
                    if (fileText1.Length > 1)
                        textBox42.Text = fileText1[1].Trim();
                    //初始负压水头:
                    if (fileText1.Length > 2)
                        textBox44.Text = fileText1[2].Trim();
                    break;

                    rowindex++;

                }
                ongo1 = ongo + 1;
                rowindex = 0;
                int blankindex = 0;
                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j] == "" || sp_txt == "")
                    {
                        blankindex++;
                        if (blankindex > 2)
                            break;
                        else
                            continue;
                    }
                    if (fileText[j] == "" && j == 1)
                        continue;

                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                    //非饱和数据组数::
                    if (fileText1.Length > 0)
                    {
                        textBox39.Text = fileText1[0].Trim();
                        break;
                    }
                }
                //每组数据行数
                qtyTable_dav24 = new DataTable();
                meizushujuhangshu(qtyTable_dav24);

                //
                ongo1 = ongo + 1;
                // ongo1 = 3;
                rowindex = 0;
                int isgo = 0;
                double dav25row = 0;
                List<int> dav25clo1 = new List<int>();

                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                    {
                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;
                        //break;
                    }

                    qtyTable_dav24.Rows.Add(qtyTable_dav24.NewRow());

                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                    for (int jj = 0; jj < fileText1.Length; jj++)
                    {
                        if (jj < qtyTable_dav24.Columns.Count - 1 && rowindex < qtyTable_dav24.Rows.Count)
                        {
                            qtyTable_dav24.Rows[rowindex][jj + 1] = fileText1[jj];
                            dav25row = Convert.ToDouble(fileText1[jj]);
                            dav25clo1.Add(Convert.ToInt32(fileText1[jj]));
                        }
                    }
                    qtyTable_dav24.Rows[rowindex][0] = rowindex + 1;
                    rowindex++;
                    break;

                }
                //只有一行数据
                if (rowindex == 0)
                {
                    qtyTable_dav24.Rows.Add(qtyTable_dav24.NewRow());
                    qtyTable_dav24.Rows[rowindex][0] = rowindex + 1;
                }
                //吸力-饱和度-相对渗透系数

                var qtyTable_dav25 = new DataTable();
                xilibaohedu(qtyTable_dav25);
                //ssss

                #region MyRegion
                if (textBox39.Text.Length > 0 && dav25clo1.Count > 0)
                {
                    int tx15 = Convert.ToInt32(textBox39.Text);

                    rowindex = 0;
                    for (int j = 0; j < tx15; j++)
                    {
                        if (j < dav25clo1.Count)
                        {
                            int tx16 = Convert.ToInt32(dav25clo1[j]);

                            for (int jj = 0; jj < tx16; jj++)
                            {

                                qtyTable_dav25.Rows.Add(qtyTable_dav25.NewRow());
                                //组号

                                qtyTable_dav25.Rows[rowindex][0] = j + 1;
                                //序号

                                qtyTable_dav25.Rows[rowindex][1] = jj + 1;

                                rowindex++;

                            }
                        }

                    }
                }
                #endregion
                //if (dav25row > 0)
                //{
                //    int icount = Convert.ToInt32(dav25row);
                //    for (int i3 = 1; i3 <= icount; i3++)
                //    {
                //        qtyTable_dav25.Rows.Add(qtyTable_dav25.NewRow());

                //    }
                //}
                //绑定数据
                ongo1 = ongo + 1;
                // ongo1 = 3;
                rowindex = 0;
                isgo = 0;
                int cloumn2 = 0;
                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (fileText[j].Contains("\t\t\t") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                    {
                        cloumn2 = 0;

                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;

                    }
                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                    bool isfinde = false;
                    string left = "";
                    string hangindex = "";

                    if (rowindex < qtyTable_dav25.Rows.Count)
                    {
                          left = Convert.ToString(qtyTable_dav25.Rows[rowindex][1]);//得到dav 的第二例的 数据
                          hangindex = Convert.ToString(cloumn2 + 1);//txt 读取堆的行数 序列
                        if (left != hangindex)
                        {


                            while (true)
                            {
                                rowindex++;
                                if (rowindex < qtyTable_dav25.Rows.Count)
                                {
                                    left = Convert.ToString(qtyTable_dav25.Rows[rowindex][1]);//得到dav 的第二例的 数据

                                    if (left == hangindex)
                                    {
                                        isfinde = true;

                                        break;

                                    }
                                }
                                else
                                    break;

                            }
                            if (isfinde == false)
                                continue;


                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (isfinde == false)
                            continue;

                    }
                    for (int jj = 0; jj < fileText1.Length; jj++)
                    {
                    

                        if (jj <= qtyTable_dav25.Columns.Count - 1 && rowindex < qtyTable_dav25.Rows.Count && left == hangindex)
                        {
                           
                            qtyTable_dav25.Rows[rowindex][jj + 1] = fileText1[jj];

                        }
                    }
                    //  qtyTable_dav25.Rows[rowindex][0] = rowindex + 1;
                    rowindex++;
                    cloumn2++;
                }
                ///不透水面个数
                ongo1 = ongo + 1;
                rowindex = 0;
                blankindex = 0;
                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j] == "" || sp_txt == "")
                    {
                        blankindex++;
                        if (blankindex > 2)
                            break;
                        else
                            continue;
                    }
                    if (fileText[j] == "" && j == 1)
                        continue;

                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                    //非饱和数据组数::
                    if (fileText1.Length > 0)
                    {
                        textBox45.Text = fileText1[0].Trim();
                        break;
                    }
                }
                //

                qtyTable_dav26 = new DataTable();
                butoushuimiangeshui(qtyTable_dav26);

                ongo1 = ongo + 1;
                rowindex = 0;
                isgo = 0;

                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (fileText[j].Contains("\t\t\t") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                    {
                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;

                    }
                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                    for (int jj = 0; jj < fileText1.Length; jj++)
                    {
                        if (jj <= qtyTable_dav26.Columns.Count - 1 && rowindex < qtyTable_dav26.Rows.Count)
                        {
                            qtyTable_dav26.Rows[rowindex][jj] = fileText1[jj];

                        }
                    }
                    //  qtyTable_dav25.Rows[rowindex][0] = rowindex + 1;
                    rowindex++;
                }
                //
                #region //已知水位点数
                ongo1 = ongo + 1;
                rowindex = 0;
                blankindex = 0;
                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j] == "" || sp_txt == "")
                    {
                        blankindex++;
                        if (blankindex > 2)
                            break;
                        else
                            continue;
                    }
                    if (fileText[j] == "" && j == 1)
                        continue;

                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                    //非饱和数据组数::
                    if (fileText1.Length > 0)
                    {
                        textBox48.Text = fileText1[0].Trim();
                        break;
                    }
                }

                qtyTable_dav27 = new DataTable();
                yizhishuiweidianshu(qtyTable_dav27);

                ongo1 = ongo + 1;
                rowindex = 0;
                isgo = 0;

                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (fileText[j].Contains("\t\t\t") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                    {
                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;

                    }
                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                    for (int jj = 0; jj < fileText1.Length; jj++)
                    {
                        if (jj <= qtyTable_dav27.Columns.Count - 1 && rowindex < qtyTable_dav27.Rows.Count)
                        {
                            qtyTable_dav27.Rows[rowindex][jj] = fileText1[jj];

                        }
                    }
                    //  qtyTable_dav25.Rows[rowindex][0] = rowindex + 1;
                    rowindex++;
                }
                #endregion

                #region 不透水点个数


                ongo1 = ongo + 1;
                rowindex = 0;
                blankindex = 0;
                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j] == "" || sp_txt == "")
                    {
                        blankindex++;
                        if (blankindex > 2)
                            break;
                        else
                            continue;
                    }
                    if (fileText[j] == "" && j == 1)
                        continue;

                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                    //非饱和数据组数::
                    if (fileText1.Length > 0)
                    {
                        textBox51.Text = fileText1[0].Trim();
                        break;
                    }
                }

                qtyTable_dav28 = new DataTable();
                butoushuidiangeshu(qtyTable_dav28);

                ongo1 = ongo + 1;
                rowindex = 0;
                isgo = 0;

                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (fileText[j].Contains("\t\t\t") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                    {
                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;

                    }
                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                    for (int jj = 0; jj < fileText1.Length; jj++)
                    {
                        if (jj <= qtyTable_dav28.Columns.Count - 1 && rowindex < qtyTable_dav28.Rows.Count)
                        {
                            qtyTable_dav28.Rows[rowindex][jj] = fileText1[jj];

                        }
                    }

                    rowindex++;
                }
                #endregion

                #region 可能溢出面个数:


                ongo1 = ongo + 1;
                rowindex = 0;
                blankindex = 0;
                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j] == "" || sp_txt == "")
                    {
                        blankindex++;
                        if (blankindex > 2)
                            break;
                        else
                            continue;
                    }
                    if (fileText[j] == "" && j == 1)
                        continue;

                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                    //非饱和数据组数::
                    if (fileText1.Length > 0)
                    {
                        textBox54.Text = fileText1[0].Trim();
                        break;
                    }
                }

                var qtyTable_dav29 = new DataTable();
                qtyTable_dav29.Columns.Add("单元号", System.Type.GetType("System.String"));//0
                qtyTable_dav29.Columns.Add("面号", System.Type.GetType("System.String"));//0

                if (textBox54.Text.Length > 0)
                {
                    if (!textBox54.Text.Contains(" ") && !textBox54.Text.Contains("\t"))
                    {
                        int icount = Convert.ToInt32(textBox54.Text);
                        for (int iq = 1; iq <= icount; iq++)
                        {

                            qtyTable_dav29.Rows.Add(qtyTable_dav29.NewRow());

                        }
                    }
                }

                ongo1 = ongo + 1;
                rowindex = 0;
                isgo = 0;

                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (fileText[j].Contains("\t\t\t") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                    {
                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;

                    }
                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                    for (int jj = 0; jj < fileText1.Length; jj++)
                    {
                        if (jj <= qtyTable_dav29.Columns.Count - 1 && rowindex < qtyTable_dav29.Rows.Count)
                        {
                            qtyTable_dav29.Rows[rowindex][jj] = fileText1[jj];

                        }
                    }

                    rowindex++;
                }
                #endregion

                this.bindingSource25.DataSource = qtyTable_dav23;
                this.dataGridView25.DataSource = this.bindingSource25;

                this.bindingSource26.DataSource = qtyTable_dav24;
                this.dataGridView23.DataSource = this.bindingSource26;

                this.bindingSource27.DataSource = qtyTable_dav25;
                this.dataGridView1.DataSource = this.bindingSource27;

                this.bindingSource28.DataSource = qtyTable_dav26;
                this.dataGridView26.DataSource = this.bindingSource28;


                this.bindingSource29.DataSource = qtyTable_dav27;
                this.dataGridView27.DataSource = this.bindingSource29;


                this.bindingSource30.DataSource = qtyTable_dav28;
                this.dataGridView28.DataSource = this.bindingSource30;

                this.bindingSource31.DataSource = qtyTable_dav29;
                this.dataGridView29.DataSource = this.bindingSource31;
                return sp_txt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取seepage文件有问题,请保证文件无误后再吃重启操作！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";

                throw;
            }
        }

        private void butoushuidiangeshu(DataTable qtyTable_dav28)
        {
            qtyTable_dav28.Columns.Add("点号", System.Type.GetType("System.String"));//0

            if (textBox51.Text.Length > 0)
            {
                if (!textBox51.Text.Contains(" ") && !textBox51.Text.Contains("\t"))
                {
                    int icount = Convert.ToInt32(textBox51.Text);
                    for (int iq = 1; iq <= icount; iq++)
                    {

                        qtyTable_dav28.Rows.Add(qtyTable_dav28.NewRow());

                    }
                }
            }
        }

        private void yizhishuiweidianshu(DataTable qtyTable_dav27)
        {
            qtyTable_dav27.Columns.Add("点号", System.Type.GetType("System.String"));//0

            qtyTable_dav27.Columns.Add("水位", System.Type.GetType("System.String"));//0

            if (textBox48.Text.Length > 0)
            {
                if (!textBox48.Text.Contains(" ") && !textBox48.Text.Contains("\t"))
                {
                    int icount = Convert.ToInt32(textBox48.Text);
                    for (int iq = 1; iq <= icount; iq++)
                    {

                        qtyTable_dav27.Rows.Add(qtyTable_dav27.NewRow());

                    }
                }
            }
        }

        private void butoushuimiangeshui(DataTable qtyTable_dav26)
        {
            qtyTable_dav26.Columns.Add("单元号", System.Type.GetType("System.String"));//0

            qtyTable_dav26.Columns.Add("面号", System.Type.GetType("System.String"));//0

            if (textBox45.Text.Length > 0)
            {
                if (!textBox45.Text.Contains(" ") && !textBox45.Text.Contains("\t"))
                {
                    int icount = Convert.ToInt32(textBox45.Text);
                    for (int iq = 1; iq <= icount; iq++)
                    {

                        qtyTable_dav26.Rows.Add(qtyTable_dav26.NewRow());

                    }
                }
            }
        }

        private void meizushujuhangshu(DataTable qtyTable_dav24)
        {
            qtyTable_dav24.Columns.Add("组号", System.Type.GetType("System.String"));//0
            if (textBox39.Text.Length > 0 && !textBox39.Text.Contains("\t") && !textBox39.Text.Contains("."))
            {
                int icount = Convert.ToInt32(textBox39.Text);
                for (int i3 = 1; i3 <= icount; i3++)
                {
                    qtyTable_dav24.Columns.Add("" + i3, System.Type.GetType("System.String"));//0

                }
            }
        }

        private string Read_temp_water(string sp_txt, string[] fileText)
        {
            if (fileText.Length > 1)
            {
                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");

                //初次蓄水日期

                if (fileText1.Length > 0)
                    textBox32.Text = fileText1[0].Trim();

                //蓄水结束日期

                if (fileText1.Length > 1)
                    textBox31.Text = fileText1[1].Trim();

                //库水温数据行（水深）数

                if (fileText[2].Length > 0)
                    textBox33.Text = fileText[2].Trim();


            }
            //库水温信息

            qtyTable_dav20 = new DataTable();
            kushuiwenxinxi(qtyTable_dav20);



            if (textBox33.Text.Length > 0)
            {
                if (!textBox33.Text.Contains(" ") && !textBox33.Text.Contains("\t"))
                {
                    int icount = Convert.ToInt32(textBox33.Text);
                    for (int iq = 1; iq <= icount; iq++)
                    {
                        //qtyTable_dav20.Rows.Add("" + iq, System.Type.GetType("System.String"));//0
                        qtyTable_dav20.Rows.Add(qtyTable_dav20.NewRow());

                    }
                }
            }

            int ongo = 0;
            int rowindex = 0;
            for (int j = 3; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);

                //new 
                sp_txt = removeblank_txt(sp_txt);
                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j] == "" || sp_txt == "")
                {
                    if (j > 4)
                        break;
                    else
                        continue;

                }
                if (fileText[j] == "" && j == 1)
                    continue;

                //  qtyTable_dav20.Rows.Add(qtyTable_dav20.NewRow());



                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav20.Columns.Count - 1 && rowindex < qtyTable_dav20.Rows.Count)
                        qtyTable_dav20.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                if (rowindex < qtyTable_dav20.Rows.Count)
                    qtyTable_dav20.Rows[rowindex][0] = rowindex + 1;
                rowindex++;


            }
            int ongo1 = ongo + 1;
            // ongo1 = 3;
            rowindex = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);
                if ((fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                {
                    break;
                }
                if (fileText[j] == "" && j == 1)
                    continue;

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                //下游水温类型:
                if (fileText1.Length > 0)
                    textBox36.Text = fileText1[0].Trim();
                //泄水孔高程:
                if (fileText1.Length > 0)
                    textBox37.Text = fileText1[1].Trim();
                //下游水温表数据行数:
                if (fileText.Length > j + 1)
                    textBox35.Text = fileText[j + 2].Trim();
                break;

                rowindex++;

            }


            qtyTable_dav21 = new DataTable();
            qtyTable_dav21.Columns.Add("序号", System.Type.GetType("System.String"));//0
            qtyTable_dav21.Columns.Add("水深", System.Type.GetType("System.String"));//1
            qtyTable_dav21.Columns.Add("T1", System.Type.GetType("System.String"));//2
            qtyTable_dav21.Columns.Add("T2", System.Type.GetType("System.String"));//3
            qtyTable_dav21.Columns.Add("T3", System.Type.GetType("System.String"));//4
            qtyTable_dav21.Columns.Add("T4", System.Type.GetType("System.String"));//5
            qtyTable_dav21.Columns.Add("T5", System.Type.GetType("System.String"));//6
            qtyTable_dav21.Columns.Add("T6", System.Type.GetType("System.String"));//7 
            qtyTable_dav21.Columns.Add("T7", System.Type.GetType("System.String"));//8 
            qtyTable_dav21.Columns.Add("T8", System.Type.GetType("System.String"));//8 
            qtyTable_dav21.Columns.Add("T9", System.Type.GetType("System.String"));//8 
            qtyTable_dav21.Columns.Add("T10", System.Type.GetType("System.String"));//8 
            qtyTable_dav21.Columns.Add("T11", System.Type.GetType("System.String"));//8 
            qtyTable_dav21.Columns.Add("T12", System.Type.GetType("System.String"));//8 

            if (textBox35.Text.Length > 0)
            {
                if (!textBox35.Text.Contains("\t"))
                {
                    int icount = Convert.ToInt32(textBox35.Text);
                    for (int iw = 1; iw <= icount; iw++)
                    {
                        //qtyTable_dav21.Rows.Add("" + iw, System.Type.GetType("System.String"));//0
                        qtyTable_dav21.Rows.Add(qtyTable_dav21.NewRow());
                        //qtyTable_dav21.Rows[iw-1][0] = iw + 1;
                        qtyTable_dav21.Rows[iw - 1][0] = iw + 1;

                    }
                }
            }


            ongo1 = ongo + 2;
            rowindex = 0;
            int blankindex = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);
                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "" || fileText[j].Length < 5 || sp_txt == "" || sp_txt.Length < 2)
                {
                    blankindex++;
                    if (blankindex > 2)
                        break;
                    else
                        continue;

                }
                if (fileText[j] == "" && j == 1)
                    continue;

                //  qtyTable_dav21.Rows.Add(qtyTable_dav21.NewRow());

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav21.Columns.Count - 1 && rowindex < qtyTable_dav21.Rows.Count)
                        qtyTable_dav21.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                if (rowindex < qtyTable_dav21.Rows.Count)
                    qtyTable_dav21.Rows[rowindex][0] = rowindex + 1;

                rowindex++;

            }

            //河水温度

            var qtyTable_dav22 = new DataTable();
            qtyTable_dav22.Columns.Add("月份", System.Type.GetType("System.String"));//0
            qtyTable_dav22.Columns.Add("1", System.Type.GetType("System.String"));//2
            qtyTable_dav22.Columns.Add("2", System.Type.GetType("System.String"));//3
            qtyTable_dav22.Columns.Add("3", System.Type.GetType("System.String"));//4
            qtyTable_dav22.Columns.Add("4", System.Type.GetType("System.String"));//5
            qtyTable_dav22.Columns.Add("5", System.Type.GetType("System.String"));//6
            qtyTable_dav22.Columns.Add("6", System.Type.GetType("System.String"));//7 
            qtyTable_dav22.Columns.Add("7", System.Type.GetType("System.String"));//8 
            qtyTable_dav22.Columns.Add("8", System.Type.GetType("System.String"));//8 
            qtyTable_dav22.Columns.Add("9", System.Type.GetType("System.String"));//8 
            qtyTable_dav22.Columns.Add("10", System.Type.GetType("System.String"));//8 
            qtyTable_dav22.Columns.Add("11", System.Type.GetType("System.String"));//8 
            qtyTable_dav22.Columns.Add("12", System.Type.GetType("System.String"));//8 

            qtyTable_dav22.Rows.Add(qtyTable_dav22.NewRow());

            ongo1 = ongo + 1;
            rowindex = 0;

            int cloumnindex = 1;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j] == "" || sp_txt == "")
                {
                    continue;
                }
                if (fileText[j] == "" && j == 1)
                    continue;


                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (fileText1.Length > 1 && cloumnindex < qtyTable_dav22.Columns.Count)
                        qtyTable_dav22.Rows[rowindex][cloumnindex] = fileText1[1];
                }
                qtyTable_dav22.Rows[rowindex][0] = "水温";

                cloumnindex++;

            }

            this.bindingSource22.DataSource = qtyTable_dav20;
            this.dataGridView24.DataSource = this.bindingSource22;

            this.bindingSource23.DataSource = qtyTable_dav21;
            this.dataGridView22.DataSource = this.bindingSource23;

            this.bindingSource24.DataSource = qtyTable_dav22;
            this.dataGridView21.DataSource = this.bindingSource24;
            return sp_txt;
        }

        private string Read_temp_fix(string sp_txt, string[] fileText)
        {
            int ongo = 0;

            if (fileText.Length > 1)
            {
                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                //挖除与回填单元总数::
                if (fileText1.Length > 0)
                    textBox29.Text = fileText1[0].Trim();
                if (fileText1.Length > 1)
                    textBox30.Text = fileText1[1].Trim();
            }
            qtyTable_dav19 = new DataTable();
            qtyTable_dav19.Columns.Add("序号", System.Type.GetType("System.String"));//0
            qtyTable_dav19.Columns.Add("节点号", System.Type.GetType("System.String"));//1


            if (textBox30.Text.Length > 0 && textBox29.Text.Length > 0)
            {

                int icount = Convert.ToInt32(textBox30.Text);
                for (int i1 = 1; i1 <= icount; i1++)
                {
                    qtyTable_dav19.Columns.Add("T" + i1, System.Type.GetType("System.String"));//0

                }
                int icount1 = Convert.ToInt32(textBox29.Text);
                for (int i2 = 1; i2 <= icount1; i2++)
                {
                    // qtyTable_dav19.Rows.Add("" + i2, System.Type.GetType("System.String"));//0
                    qtyTable_dav19.Rows.Add(qtyTable_dav19.NewRow());
                }
                int ongo1 = ongo + 1;
                int rowindex = 0;
                int isgo = 0;
                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                    {
                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;
                        //break;
                    }
                    if (fileText[j] == "" && j == 1)
                        continue;

                    // qtyTable_dav19.Rows.Add(qtyTable_dav19.NewRow());



                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                    for (int jj = 0; jj < fileText1.Length; jj++)
                    {
                        if (jj <= icount && rowindex < qtyTable_dav19.Rows.Count)
                            qtyTable_dav19.Rows[rowindex][jj + 1] = fileText1[jj];
                    }
                    if (rowindex < qtyTable_dav19.Rows.Count)
                        qtyTable_dav19.Rows[rowindex][0] = rowindex + 1;
                    rowindex++;

                }
            }
            this.bindingSource20.DataSource = qtyTable_dav19;
            this.dataGridView20.DataSource = this.bindingSource20;
            return sp_txt;
        }

        private string Read_grouting_step(string sp_txt, string[] fileText)
        {
            int ongo = 0;
            int rowindex = 0;
            int isgo = 0;
            if (fileText.Length > 1)
            {
                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");
                if (fileText1.Length < 1)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                rowindex = 0;
                isgo = 0;
                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    sp_txt = removeblank(sp_txt, fileText1, jj);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);

                    if (jj >= fileText1.Length || (fileText1[jj].Contains("\t\t\t\t") && fileText1[jj].Replace("\t", "").Trim() == "") || (fileText1[jj].Replace("  ", "").Trim() == "" && jj != 1) || sp_txt == "")
                    {
                        isgo++;
                        if (rowindex > 0)
                            break;
                        else
                            continue;
                    }

                    //挖除与回填单元总数::
                    if (fileText1.Length > 0)
                        textBox26.Text = fileText1[jj].Trim();
                }
            }
            qtyTable_dav18 = new DataTable();

            qtyTable_dav18.Columns.Add("单元号", System.Type.GetType("System.String"));//0
            qtyTable_dav18.Columns.Add("浇筑号", System.Type.GetType("System.String"));//1
            qtyTable_dav18.Columns.Add("计算步号", System.Type.GetType("System.String"));//1

            int ongo1 = ongo + 1;
            rowindex = 0;
            isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);
                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                    //break;
                }
                if (fileText[j] == "" && j == 1)
                    continue;

                qtyTable_dav18.Rows.Add(qtyTable_dav18.NewRow());



                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < 3)
                        qtyTable_dav18.Rows[rowindex][jj] = fileText1[jj];
                }
                rowindex++;

            }
            this.bindingSource19.DataSource = qtyTable_dav18;
            this.dataGridView16.DataSource = this.bindingSource19;
            return sp_txt;
        }

        private string Read_joint_mesh(string sp_txt, string[] fileText)
        {
            if (fileText.Length > 1)
            {
                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt.Replace("  ", "").Trim(), " ");

                //缝单元总数

                if (fileText1.Length > 0)
                    textBox28.Text = fileText1[0].Trim();

                //缝材料总数

                if (fileText1.Length > 1)
                    textBox27.Text = fileText1[1].Trim();

            }
            //刚度系数
            qtyTable_dav14 = new DataTable();
            gangduxishu(qtyTable_dav14);

            int ongo = 0;
            int isgo = 0;
            int rowindex = 0;

            for (int j = 1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);
                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                    //break;
                }
                if (fileText[j].Replace("  ", "").Trim() == "" && j == 1)
                    continue;

                qtyTable_dav14.Rows.Add(qtyTable_dav14.NewRow());

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                if (fileText1.Length < 2)
                {
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt.Replace("  ", "").Trim(), " ");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");

                }
                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < 5)
                        qtyTable_dav14.Rows[j - 2][jj + 1] = fileText1[jj];
                }
                qtyTable_dav14.Rows[j - 2][0] = j - 1;
                rowindex++;


            }
            //强度系数

            qtyTable_dav15 = new DataTable();
            qiangduxishu(qtyTable_dav15);

            //
            int ongo1 = ongo + 1;
            // ongo1 = 3;
            rowindex = 0;
            isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);
                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 6 || rowindex > 0)
                        break;
                    else
                        continue;
                    //break;
                }
                if (fileText[j] == "" && j == 1)
                    continue;

                qtyTable_dav15.Rows.Add(qtyTable_dav15.NewRow());

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                {

                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                }
                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav15.Columns.Count - 1)
                        qtyTable_dav15.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                qtyTable_dav15.Rows[rowindex][0] = rowindex + 1;
                rowindex++;

            }
            //缝单元节点编

            qtyTable_dav16 = new DataTable();
            fengdanyuanjiedainbian(qtyTable_dav16);

            ongo1 = ongo + 1;
            rowindex = 0;
            isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;

                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                {
                    isgo++;
                    if (isgo > 6 || rowindex > 0)
                        break;
                    else
                        continue;
                    //break;
                }
                if (fileText[j] == "" && j == 1)
                    continue;

                qtyTable_dav16.Rows.Add(qtyTable_dav16.NewRow());

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav16.Columns.Count - 1 && rowindex < qtyTable_dav16.Rows.Count)
                        qtyTable_dav16.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                qtyTable_dav16.Rows[rowindex][0] = rowindex + 1;
                rowindex++;

            }
            this.bindingSource16.DataSource = qtyTable_dav14;
            this.dataGridView19.DataSource = this.bindingSource16;

            this.bindingSource17.DataSource = qtyTable_dav15;
            this.dataGridView18.DataSource = this.bindingSource17;

            this.bindingSource18.DataSource = qtyTable_dav16;
            this.dataGridView17.DataSource = this.bindingSource18;
            return sp_txt;
        }

        private string Read_point_disp_output(string sp_txt, string[] fileText)
        {
            int ongo = 0;

            if (fileText.Length > 1)
            {
                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                //挖除与回填单元总数::
                if (fileText1.Length > 0)
                    textBox25.Text = fileText1[0].Trim();
            }

            qtyTable_dav13 = new DataTable();

            shuchuweiyidian(qtyTable_dav13);

            int ongo1 = ongo + 1;
            int rowindex = 0;
            int isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                    //break;
                }
                if (fileText[j] == "" && j == 1)
                    continue;
                if (fileText[j].Replace("\t", "") == "")
                    continue;
                qtyTable_dav13.Rows.Add(qtyTable_dav13.NewRow());

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav13.Columns.Count - 1 && rowindex < qtyTable_dav13.Rows.Count)
                        qtyTable_dav13.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                qtyTable_dav13.Rows[rowindex][0] = rowindex + 1;
                rowindex++;

            }
            this.bindingSource15.DataSource = qtyTable_dav13;
            this.dataGridView15.DataSource = this.bindingSource15;
            return sp_txt;
        }

        private string Read_num_sup_step(string sp_txt, string[] fileText)
        {
            if (fileText.Length >= 1)
            {
                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");

                //浇筑次数
                if (fileText1.Length > 0)
                    textBox23.Text = fileText1[0].Trim();


            }
            return sp_txt;
        }

        private string Read_sup_step(string sp_txt, string[] fileText)
        {
            int ongo = 0;

            if (fileText.Length > 1)
            {
                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                //挖除与回填单元总数::
                //if (fileText1.Length > 0)
                //    textBox23.Text = fileText1[0].Trim();
            }
            //浇筑及计算时步
            qtyTable_dav11 = new DataTable();
            qtyTable_dav11.Columns.Add("浇筑序号", System.Type.GetType("System.String"));//0
            qtyTable_dav11.Columns.Add("单元数", System.Type.GetType("System.String"));//1
            qtyTable_dav11.Columns.Add("节点数", System.Type.GetType("System.String"));//2
            qtyTable_dav11.Columns.Add("计算次数", System.Type.GetType("System.String"));//3


            if (textBox23.Text.Length >= 1)
            {
                int icount = Convert.ToInt32(textBox23.Text);
                icount = 20;

                Adddav11cloumn(icount, qtyTable_dav11);

                //int ongo1 = ongo + 1;
                int ongo1 = ongo;//新增需求 删除第一行
                int rowindex = 0;
                int isadd = 0;
                int cloindex = 0;
                string comtxt = "";
                int isgo = 0;
                for (int j = ongo1; j <= fileText.Length; j++)
                {
                    ongo = j;
                    sp_txt = removeblank(sp_txt, fileText, j);
                    //new 
                    sp_txt = removeblank_txt(sp_txt);
                    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                    {
                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;

                        //break;
                    }
                    if (fileText[j] == "" && j == 1)
                        continue;
                    if (isadd == 0)
                    {

                        qtyTable_dav11.Rows.Add(qtyTable_dav11.NewRow());

                        sp_txt = sp_txt.Replace("\t", " ").Trim();

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            cloindex = jj;
                            if (jj < qtyTable_dav11.Columns.Count - 1 && rowindex < qtyTable_dav11.Rows.Count)
                                qtyTable_dav11.Rows[rowindex][jj + 1] = fileText1[jj];
                        }
                        qtyTable_dav11.Rows[rowindex][0] = rowindex + 1;
                        isadd++;

                    }
                    else
                    {
                        sp_txt = removeblank(sp_txt, fileText, j);
                        //new 
                        sp_txt = removeblank_txt(sp_txt);


                        //   qtyTable_dav11.Rows.Add(qtyTable_dav11.NewRow());
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (qtyTable_dav11.Columns.Count >= cloindex + 1)
                            {
                                if (jj < qtyTable_dav11.Columns.Count - 2 && rowindex < qtyTable_dav11.Rows.Count)
                                {
                                    qtyTable_dav11.Rows[rowindex][cloindex + 2] = fileText1[jj];
                                    cloindex++;
                                }
                            }
                        }
                        cloindex = 0;
                        isadd = 0;
                        rowindex++;
                    }


                }
            }

            this.bindingSource13.DataSource = qtyTable_dav11;
            this.dataGridView13.DataSource = this.bindingSource13;
            return sp_txt;
        }

        private string Read_placement_time_of_element(string sp_txt, string[] fileText)
        {
            int ongo = 0;

            if (fileText.Length > 1)
            {
                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                //挖除与回填单元总数::
                if (fileText1.Length > 0)
                    textBox20.Text = fileText1[0].Trim();
            }

            qtyTable_dav8 = new DataTable();
            wajueyuhuitian(qtyTable_dav8);

            int ongo1 = ongo + 1;
            int rowindex = 0;
            int isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);
                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1) || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                    //break;
                }
                if (fileText[j] == "" && j == 1)
                    continue;

                qtyTable_dav8.Rows.Add(qtyTable_dav8.NewRow());

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav8.Columns.Count && rowindex < qtyTable_dav8.Rows.Count)
                        qtyTable_dav8.Rows[rowindex][jj] = fileText1[jj];
                }

                rowindex++;

            }


            this.bindingSource10.DataSource = qtyTable_dav8;
            this.dataGridView10.DataSource = this.bindingSource10;
            return sp_txt;
        }

        private string Read_temp_para(string sp_txt, string[] fileText)
        {
            if (fileText.Length > 1)
            {
                sp_txt = removeblank(sp_txt, fileText, 0);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");


                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");


                if (fileText[0].Contains("\t") && !fileText[0].Contains(" "))
                {
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");


                }
                //表面散热系数总数
                if (fileText1.Length > 0)
                    textBox13.Text = fileText1[0].Trim();

                //水管总数:
                if (fileText1.Length > 1)
                    textBox15.Text = fileText1[1].Trim();

                //冷却期数:
                if (fileText1.Length > 2)
                    textBox16.Text = fileText1[2].Trim();


            }
            //热学参数
            qtyTable_dav2 = new DataTable();
            rexuecanshu(qtyTable_dav2);
            int isgo = 0;
            int rowindex = 0;
            int ongo = 0;
            for (int j = 1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "") || sp_txt == "")
                {
                    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "")
                    {
                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;

                    }
                }

                qtyTable_dav2.Rows.Add(qtyTable_dav2.NewRow());

                sp_txt = sp_txt.Replace(" ", "\t").Replace("\t\t", "\t");
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav2.Columns.Count - 1 && rowindex < qtyTable_dav2.Rows.Count)
                        qtyTable_dav2.Rows[rowindex][jj + 1] = fileText1[jj];


                }
                qtyTable_dav2.Rows[rowindex][0] = rowindex + 1;
                rowindex++;
            }
            //表面散热系数
            qtyTable_dav3 = new DataTable();
            qtyTable_dav3.Columns.Add("βw", System.Type.GetType("System.String"));//0
            if (textBox13.Text.Length > 0 && !textBox13.Text.Contains("\t"))
            {
                int icount = Convert.ToInt32(textBox13.Text);
                for (int i3 = 1; i3 < icount + 1; i3++)
                {
                    qtyTable_dav3.Columns.Add("β" + i3, System.Type.GetType("System.String"));//0

                }
            }

            //
            int ongo1 = ongo + 1;
            // ongo1 = 3;
            rowindex = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);
                if (fileText[j].Contains("\t\t\t") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                    //break;
                }

                qtyTable_dav3.Rows.Add(qtyTable_dav3.NewRow());

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav3.Columns.Count - 1 && rowindex < qtyTable_dav3.Rows.Count)
                        qtyTable_dav3.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                qtyTable_dav3.Rows[rowindex][0] = rowindex + 1;
                rowindex++;

            }
            //只有一行数据
            if (rowindex == 0)
            {
                qtyTable_dav3.Rows.Add(qtyTable_dav3.NewRow());
                qtyTable_dav3.Rows[rowindex][0] = rowindex + 1;
            }

            //水管定义

            qtyTable_dav4 = new DataTable();
            shuiguandingyi(qtyTable_dav4);

            #region old

            //ongo1 = ongo + 1;
            //rowindex = 0;
            //for (int j = ongo1; j <= fileText.Length; j++)
            //{
            //    ongo = j;
            //    sp_txt = removeblank(sp_txt, fileText, j);
            //    //new 
            //    sp_txt = removeblank_txt(sp_txt);

            //    if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
            //    {
            //        isgo++;
            //        if (isgo > 1 || rowindex > 0)
            //            break;
            //        else
            //            continue;
            //        //break;
            //    }

            //    qtyTable_dav4.Rows.Add(qtyTable_dav4.NewRow());



            //    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
            //    if (fileText1.Length < 2)
            //        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

            //    for (int jj = 0; jj < fileText1.Length; jj++)
            //    {
            //        if (jj < qtyTable_dav4.Columns.Count - 1 && rowindex < qtyTable_dav4.Rows.Count)
            //            qtyTable_dav4.Rows[rowindex][jj + 1] = fileText1[jj];
            //    }
            //    qtyTable_dav4.Rows[rowindex][0] = rowindex + 1;
            //    rowindex++;

            //}

            #endregion


            //通水参数

            qtyTable_dav5 = new DataTable();
            tongshuocanshu(qtyTable_dav5);

            ongo1 = ongo + 1;
            rowindex = 0;
            int isupordown = 0;
            int uprowindex = 0;
            int cloumn2 = 0;
            #region old
            //for (int j = ongo1; j <= fileText.Length; j++)
            //{
            //    ongo = j;
            //    sp_txt = removeblank(sp_txt, fileText, j);
            //    //new 
            //    sp_txt = removeblank_txt(sp_txt);

            //    if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
            //    {
            //        isgo++;
            //        if (isgo > 1 || rowindex > 0)
            //            break;
            //        else
            //            continue;
            //        //break;
            //    }

            //    //  qtyTable_dav5.Rows.Add(qtyTable_dav5.NewRow());

            //    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
            //    if (fileText1.Length < 2)
            //        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

            //    for (int jj = 0; jj < fileText1.Length; jj++)
            //    {
            //        if (jj < qtyTable_dav5.Columns.Count - 2 && rowindex < qtyTable_dav5.Rows.Count)

            //            qtyTable_dav5.Rows[rowindex][jj + 2] = fileText1[jj];
            //    }
            //    //   qtyTable_dav5.Rows[rowindex][0] = rowindex + 1;
            //    rowindex++;

            //}  
            #endregion
            #region new
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                {
                    isgo++;
                    isupordown = 0;
                    cloumn2 = 0;

                    //if (isgo > 1 || rowindex > 0)
                    //    break;
                    //else
                    continue;
                    //break;
                }

                //水管定义 new
                if (isupordown == 0)
                {

                    qtyTable_dav4.Rows.Add(qtyTable_dav4.NewRow());



                    string[] fileText11 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText11.Length < 2)
                        fileText11 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                    for (int jj = 0; jj < fileText11.Length; jj++)
                    {
                        if (jj < qtyTable_dav4.Columns.Count - 1 && uprowindex < qtyTable_dav4.Rows.Count)
                            qtyTable_dav4.Rows[uprowindex][jj + 1] = fileText11[jj];
                    }
                    qtyTable_dav4.Rows[uprowindex][0] = uprowindex + 1;
                    uprowindex++;
                    isupordown = 1;
                    continue;

                }


                //  qtyTable_dav5.Rows.Add(qtyTable_dav5.NewRow());

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    //判断 是否留下此行为空行 比如手动 增加冷却期数 后 7 到8  应该在 在水管号8行留出来不应 占用
                    string left = Convert.ToString(qtyTable_dav5.Rows[rowindex][1]);//得到dav 的第二例的 数据
                    string hangindex = Convert.ToString(cloumn2 + 1);//txt 读取堆的行数 序列
                    if (left != hangindex)
                    {
                        while (true)
                        {
                            rowindex++;
                            left = Convert.ToString(qtyTable_dav5.Rows[rowindex][1]);//得到dav 的第二例的 数据

                            if (left == hangindex)
                            {

                                break;

                            }
                        }

                    }
                    //&& left == hangindex
                    if (jj < qtyTable_dav5.Columns.Count - 2 && rowindex < qtyTable_dav5.Rows.Count && left == hangindex)

                        qtyTable_dav5.Rows[rowindex][jj + 2] = fileText1[jj];
                }
                //   qtyTable_dav5.Rows[rowindex][0] = rowindex + 1;
                rowindex++;
                cloumn2++;


            }

            #endregion

            this.bindingSource2.DataSource = qtyTable_dav2;
            this.dataGridView2.DataSource = this.bindingSource2;

            this.bindingSource3.DataSource = qtyTable_dav3;
            this.dataGridView3.DataSource = this.bindingSource3;

            this.bindingSource4.DataSource = qtyTable_dav4;
            this.dataGridView4.DataSource = this.bindingSource4;

            this.bindingSource5.DataSource = qtyTable_dav5;
            this.dataGridView5.DataSource = this.bindingSource5;
            return sp_txt;
        }

        private string read_els_para(string sp_txt, string[] fileText)
        {
            int ongo = 0;

            if (fileText.Length > 1)
            {

                sp_txt = removeblank(sp_txt, fileText, 0);
                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                //材料种数:
                if (fileText1.Length > 0)
                    textBox17.Text = fileText1[0].Trim();
            }
            //基本力学参数
            qtyTable_dav5 = new DataTable();
            jibenlixuecanshu(qtyTable_dav5);

            int ongo1 = ongo + 1;
            int rowindex = 0;
            int isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                sp_txt = removeblank_txt(sp_txt);
                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;

                }
                qtyTable_dav5.Rows.Add(qtyTable_dav5.NewRow());


                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav5.Columns.Count - 1 && rowindex < qtyTable_dav5.Rows.Count)
                        qtyTable_dav5.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                qtyTable_dav5.Rows[rowindex][0] = rowindex + 1;
                rowindex++;

            }
            //徐变参数
            qtyTable_dav6 = new DataTable();
            xubiancanshu(qtyTable_dav6);

            ongo1 = ongo + 1;
            rowindex = 0;
            isgo = 0;

            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                    //isgo++;
                    //if (isgo > 1 || rowindex>0)
                    //break;
                    //else
                    //    continue;
                }
                if (fileText[j] == "" && j == 1)
                    continue;
                qtyTable_dav6.Rows.Add(qtyTable_dav6.NewRow());


                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length <= 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt.Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt.Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav6.Columns.Count - 1 && rowindex < qtyTable_dav6.Rows.Count)
                        qtyTable_dav6.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                qtyTable_dav6.Rows[rowindex][0] = rowindex + 1;
                rowindex++;

            }
            //荷载
            ongo1 = ongo + 1;
            rowindex = 0;
            isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j] == "" || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                    //isgo++;
                    //if (isgo >1)
                    //break;
                    //else
                    //    continue;
                }


                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                //if (fileText1.Length > 0 && fileText1[0] == "1")
                //    radioButton1.Checked = true;
                //else if (fileText1.Length > 1 && fileText1[0] == "0")
                //    radioButton1.Checked = false;
                //计算量2
                if (fileText1.Length >= 1 && fileText1[0] == "1")
                    radioButton8.Checked = true;
                else
                    radioButton8.Checked = false;
                if (fileText1.Length >= 2 && fileText1[1] == "1")
                    radioButton9.Checked = true;
                else
                    radioButton9.Checked = false;
                if (fileText1.Length >= 3 && fileText1[2] == "1")
                    radioButton10.Checked = true;
                else
                    radioButton10.Checked = false;
                if (fileText1.Length >= 4 && fileText1[3] == "1")
                    radioButton11.Checked = true;
                else
                    radioButton11.Checked = false;
                ////渗透力
                if (fileText1.Length >= 5)
                    this.textBox18.Text = fileText1[4].Trim();
                //自生体积变形定义点数
                if (fileText1.Length >= 6)
                    this.textBox19.Text = fileText1[5].Trim();
                break;

            }
            //自生体积变形定义点数
            ongo1 = ongo + 1;
            rowindex = 0;
            isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                }


                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                ////自生体积变形定义点数
                if (fileText1.Length >= 1)
                    this.textBox19.Text = fileText1[0].Trim();

            }
            //自生体积变形
            qtyTable8 = new DataTable();
            zishengtijibianxing(qtyTable8);
            ongo1 = ongo + 1;
            rowindex = 0;
            isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;

                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || (fileText[j].Contains("\t\t") || fileText[j].Replace("  ", "").Trim() == "") || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                    //isgo++;
                    //if (isgo > 1 || rowindex>0)
                    //break;
                    //else
                    //    continue;
                }
                //textbox17已经确认行数此处不加
                // qtyTable8.Rows.Add(qtyTable8.NewRow());

                if (fileText.Length > j)
                {



                    sp_txt = sp_txt.Replace(" ", "\t").Replace("\t\t", "\t");
                    string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                    if (fileText1.Length < 2)
                        fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                    for (int jj = 0; jj < fileText1.Length; jj++)
                    {
                        if (jj < qtyTable8.Columns.Count - 1 && rowindex < qtyTable8.Rows.Count)
                            qtyTable8.Rows[rowindex][jj + 1] = fileText1[jj];
                    }
                    rowindex++;
                }
            }
            ongo1 = ongo + 1;
            rowindex = 0;
            isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                {
                    isgo++;
                    if (isgo > 1 || rowindex > 0)
                        break;
                    else
                        continue;
                    //isgo++;
                    //if (isgo > 1 || rowindex>0)
                    //break;
                    //else
                    //    continue;
                }


                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");

                if (fileText1.Length > 0 && fileText1[0] == "1")
                    radioButton12.Checked = true;
                else if (fileText1.Length > 0 && fileText1[0] == "0")
                    radioButton12.Checked = false;

                rowindex++;

            }


            //氧化镁
            qtyTable_dav7 = new DataTable();
            yanghuamei(qtyTable_dav7);

            ongo1 = ongo + 1;
            rowindex = 0;
            isgo = 0;
            for (int j = ongo1; j <= fileText.Length; j++)
            {
                ongo = j;
                sp_txt = removeblank(sp_txt, fileText, j);
                //new 
                sp_txt = removeblank_txt(sp_txt);

                if (j >= fileText.Length || (fileText[j].Contains("\t\t\t\t") && fileText[j].Replace("\t", "").Trim() == "") || fileText[j].Replace("  ", "").Trim() == "" || sp_txt == "")
                {
                    int con = 0;
                    if (j < fileText.Length && fileText[j].Contains("\t\t\t\t"))
                    {
                        if (fileText[j].Replace("\t", "") == "")
                        {
                        }
                        else
                            con = 1;

                    }
                    if (con == 0)
                    {
                        isgo++;
                        if (isgo > 1 || rowindex > 0)
                            break;
                        else
                            continue;
                    }
                }

                qtyTable_dav7.Rows.Add(qtyTable_dav7.NewRow());

                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, "\t");
                if (fileText1.Length < 2)
                    fileText1 = System.Text.RegularExpressions.Regex.Split(sp_txt, " ");
                for (int jj = 0; jj < fileText1.Length; jj++)
                {
                    if (jj < qtyTable_dav7.Columns.Count - 1 && rowindex < qtyTable_dav7.Rows.Count)
                        qtyTable_dav7.Rows[rowindex][jj + 1] = fileText1[jj];
                }
                qtyTable_dav7.Rows[rowindex][0] = rowindex + 1;
                rowindex++;

            }
            this.bindingSource6.DataSource = qtyTable_dav5;
            this.dataGridView6.DataSource = this.bindingSource6;

            this.bindingSource8.DataSource = qtyTable_dav6;
            this.dataGridView7.DataSource = this.bindingSource8;

            this.bindingSource9.DataSource = qtyTable_dav7;
            this.dataGridView9.DataSource = this.bindingSource9;

            this.bindingSource7.DataSource = qtyTable8;
            this.dataGridView8.DataSource = this.bindingSource7;
            return sp_txt;
        }
        private static void xilibaohedu(DataTable qtyTable_dav20)
        {
            qtyTable_dav20.Columns.Add("组号", System.Type.GetType("System.String"));//0
            qtyTable_dav20.Columns.Add("序号", System.Type.GetType("System.String"));//1
            qtyTable_dav20.Columns.Add("吸力", System.Type.GetType("System.String"));//2
            qtyTable_dav20.Columns.Add("饱和度", System.Type.GetType("System.String"));//3
            qtyTable_dav20.Columns.Add("相对渗透系数", System.Type.GetType("System.String"));//4

        }
        private static void baoheshentouxishu(DataTable qtyTable_dav20)
        {
            qtyTable_dav20.Columns.Add("材料编号", System.Type.GetType("System.String"));//0
            qtyTable_dav20.Columns.Add("kxx", System.Type.GetType("System.String"));//1
            qtyTable_dav20.Columns.Add("kyy", System.Type.GetType("System.String"));//2
            qtyTable_dav20.Columns.Add("kzz", System.Type.GetType("System.String"));//3
            qtyTable_dav20.Columns.Add("kxy", System.Type.GetType("System.String"));//4
            qtyTable_dav20.Columns.Add("kyz", System.Type.GetType("System.String"));//5
            qtyTable_dav20.Columns.Add("kzx", System.Type.GetType("System.String"));//6
            qtyTable_dav20.Columns.Add("空隙率", System.Type.GetType("System.String"));//7 
            qtyTable_dav20.Columns.Add("骨架弹损", System.Type.GetType("System.String"));//8 
            qtyTable_dav20.Columns.Add("骨架泊松比", System.Type.GetType("System.String"));//8 

        }
        private static void kushuiwenxinxi(DataTable qtyTable_dav20)
        {
            qtyTable_dav20.Columns.Add("序号", System.Type.GetType("System.String"));//0
            qtyTable_dav20.Columns.Add("水深", System.Type.GetType("System.String"));//1
            qtyTable_dav20.Columns.Add("T1", System.Type.GetType("System.String"));//2
            qtyTable_dav20.Columns.Add("T2", System.Type.GetType("System.String"));//3
            qtyTable_dav20.Columns.Add("T3", System.Type.GetType("System.String"));//4
            qtyTable_dav20.Columns.Add("T4", System.Type.GetType("System.String"));//5
            qtyTable_dav20.Columns.Add("T5", System.Type.GetType("System.String"));//6
            qtyTable_dav20.Columns.Add("T6", System.Type.GetType("System.String"));//7 
            qtyTable_dav20.Columns.Add("T7", System.Type.GetType("System.String"));//8 
            qtyTable_dav20.Columns.Add("T8", System.Type.GetType("System.String"));//8 
            qtyTable_dav20.Columns.Add("T9", System.Type.GetType("System.String"));//8 
            qtyTable_dav20.Columns.Add("T10", System.Type.GetType("System.String"));//8 
            qtyTable_dav20.Columns.Add("T11", System.Type.GetType("System.String"));//8 
            qtyTable_dav20.Columns.Add("T12", System.Type.GetType("System.String"));//8 
        }

        private static string tongyi_tempty(string sp_txt)
        {
            sp_txt = sp_txt.Replace(" \t", "\t").Trim();
            sp_txt = sp_txt.Replace("\t ", "\t").Trim();
            sp_txt = sp_txt.Replace(" ", "\t").Trim();
            return sp_txt;
        }

        private static void shunshangyuruanhuaxishu(DataTable qtyTable_dav10)
        {
            qtyTable_dav10.Columns.Add("材料号", System.Type.GetType("System.String"));//0
            qtyTable_dav10.Columns.Add("α", System.Type.GetType("System.String"));//1
            qtyTable_dav10.Columns.Add("N", System.Type.GetType("System.String"));//2
            qtyTable_dav10.Columns.Add("拉极限应变", System.Type.GetType("System.String"));//3
            qtyTable_dav10.Columns.Add("剪极限应变", System.Type.GetType("System.String"));//3
            qtyTable_dav10.Columns.Add("刚度软化", System.Type.GetType("System.String"));//3
            qtyTable_dav10.Columns.Add("强度软化", System.Type.GetType("System.String"));//3
        }

        private static void feixianxing_qiangduxishu(DataTable qtyTable_dav9)
        {
            qtyTable_dav9.Columns.Add("材料号", System.Type.GetType("System.String"));//0
            qtyTable_dav9.Columns.Add("凝聚力", System.Type.GetType("System.String"));//1
            qtyTable_dav9.Columns.Add("摩擦角", System.Type.GetType("System.String"));//2
            qtyTable_dav9.Columns.Add("抗拉强度", System.Type.GetType("System.String"));//3
            // qtyTable_dav9.Columns.Add("抗压强度", System.Type.GetType("System.String"));//3新变更
            qtyTable_dav9.Columns.Add("单轴抗压强度", System.Type.GetType("System.String"));//3
            qtyTable_dav9.Columns.Add("双轴抗压强度", System.Type.GetType("System.String"));//3

            qtyTable_dav9.Columns.Add("准则号", System.Type.GetType("System.String"));//3
            qtyTable_dav9.Columns.Add("r1", System.Type.GetType("System.String"));//3
            qtyTable_dav9.Columns.Add("r2", System.Type.GetType("System.String"));//3
            qtyTable_dav9.Columns.Add("r3", System.Type.GetType("System.String"));//3
            qtyTable_dav9.Columns.Add("r4", System.Type.GetType("System.String"));//3
        }

        private static string removeblank(string sp_txt, string[] fileText, int j)
        {
            if (j >= fileText.Length)
                return "";

            sp_txt = fileText[j].Trim();

            while (true)
            {
                if (sp_txt.Contains("  "))
                {
                    sp_txt = sp_txt.Replace("  ", " ");

                }
                else
                    break;

            }
            while (true)
            {
                if (sp_txt.Contains("\t\t"))
                {
                    sp_txt = sp_txt.Replace("\t\t", "\t");

                }
                else
                    break;

            }
            return sp_txt;
        }
        private static string removeblank_txt(string sp_txt)
        {
            sp_txt = sp_txt.Trim().Replace(" ", "\t").Replace("\t\t", "\t");

            while (true)
            {
                if (sp_txt.Contains("  "))
                {
                    sp_txt = sp_txt.Replace("  ", " ");

                }
                else
                    break;

            }
            while (true)
            {
                if (sp_txt.Contains("\t\t"))
                {
                    sp_txt = sp_txt.Replace("\t\t", "\t");

                }
                else
                    break;

            }
            return sp_txt;
        }

        private static void fengdanyuanjiedainbian(DataTable qtyTable_dav16)
        {
            qtyTable_dav16.Columns.Add("单元号", System.Type.GetType("System.String"));//0
            qtyTable_dav16.Columns.Add("n1", System.Type.GetType("System.String"));//1
            qtyTable_dav16.Columns.Add("n2", System.Type.GetType("System.String"));//2
            qtyTable_dav16.Columns.Add("n3", System.Type.GetType("System.String"));//3
            qtyTable_dav16.Columns.Add("n4", System.Type.GetType("System.String"));//4
            qtyTable_dav16.Columns.Add("n5", System.Type.GetType("System.String"));//5
            qtyTable_dav16.Columns.Add("n6", System.Type.GetType("System.String"));//6
            qtyTable_dav16.Columns.Add("n7", System.Type.GetType("System.String"));//7 
            qtyTable_dav16.Columns.Add("n8", System.Type.GetType("System.String"));//8 
            qtyTable_dav16.Columns.Add("nm", System.Type.GetType("System.String"));//8 
        }

        private static void qiangduxishu(DataTable qtyTable_dav15)
        {
            qtyTable_dav15.Columns.Add("材料号", System.Type.GetType("System.String"));//0
            qtyTable_dav15.Columns.Add("Re", System.Type.GetType("System.String"));//1
            qtyTable_dav15.Columns.Add("c", System.Type.GetType("System.String"));//2
            qtyTable_dav15.Columns.Add("f", System.Type.GetType("System.String"));//3
            qtyTable_dav15.Columns.Add("fg", System.Type.GetType("System.String"));//4
            qtyTable_dav15.Columns.Add("cl", System.Type.GetType("System.String"));//5
            qtyTable_dav15.Columns.Add("cc", System.Type.GetType("System.String"));//4
            qtyTable_dav15.Columns.Add("cf", System.Type.GetType("System.String"));//5
            qtyTable_dav15.Columns.Add("pre", System.Type.GetType("System.String"));//4
        }

        private static void gangduxishu(DataTable qtyTable_dav14)
        {
            qtyTable_dav14.Columns.Add("材料号", System.Type.GetType("System.String"));//0
            qtyTable_dav14.Columns.Add("法向刚度", System.Type.GetType("System.String"));//1
            qtyTable_dav14.Columns.Add("切向刚度", System.Type.GetType("System.String"));//2
            qtyTable_dav14.Columns.Add("法向残余", System.Type.GetType("System.String"));//3
            qtyTable_dav14.Columns.Add("切向残余", System.Type.GetType("System.String"));//4
            qtyTable_dav14.Columns.Add("渗透系数", System.Type.GetType("System.String"));//5
        }

        private static void shuchuweiyidian(DataTable qtyTable_dav13)
        {
            qtyTable_dav13.Columns.Add("序号", System.Type.GetType("System.String"));//0
            qtyTable_dav13.Columns.Add("节点号", System.Type.GetType("System.String"));//1
        }

        private void Adddav11cloumn(int icount, DataTable qtyTable_dav11)
        {
            for (int i11 = 1; i11 <= icount; i11++)
            {
                qtyTable_dav11.Columns.Add("△t" + i11, System.Type.GetType("System.String"));//0

            }
        }

        private static void wajueyuhuitian(DataTable qtyTable_dav8)
        {
            qtyTable_dav8.Columns.Add("单元号", System.Type.GetType("System.String"));//0
            qtyTable_dav8.Columns.Add("挖除序号", System.Type.GetType("System.String"));//1
            qtyTable_dav8.Columns.Add("回填序号", System.Type.GetType("System.String"));//2
            qtyTable_dav8.Columns.Add("回填材料号", System.Type.GetType("System.String"));//3
        }

        private void tongshuocanshu(DataTable qtyTable_dav5)
        {
            qtyTable_dav5.Columns.Add("水管号", System.Type.GetType("System.String"));//0
            qtyTable_dav5.Columns.Add("通水期数", System.Type.GetType("System.String"));//1
            qtyTable_dav5.Columns.Add("t1", System.Type.GetType("System.String"));//2
            qtyTable_dav5.Columns.Add("t2", System.Type.GetType("System.String"));//3
            qtyTable_dav5.Columns.Add("Tw1", System.Type.GetType("System.String"));//4
            qtyTable_dav5.Columns.Add("Tw2", System.Type.GetType("System.String"));//5
            qtyTable_dav5.Columns.Add("Tend", System.Type.GetType("System.String"));//6
            qtyTable_dav5.Columns.Add("Kw", System.Type.GetType("System.String"));//7 
            qtyTable_dav5.Columns.Add("qn", System.Type.GetType("System.String"));//8 
            qtyTable_dav5.Columns.Add("D", System.Type.GetType("System.String"));//8 

            if (textBox16.Text.Length > 0 && textBox15.Text.Length > 0)
            {
                int tx16 = Convert.ToInt32(textBox16.Text);
                int tx15 = Convert.ToInt32(textBox15.Text);

                int rowindex = 0;
                for (int j = 0; j < tx15; j++)
                {

                    for (int jj = 0; jj < tx16; jj++)
                    {

                        qtyTable_dav5.Rows.Add(qtyTable_dav5.NewRow());
                        //水管号

                        qtyTable_dav5.Rows[rowindex][0] = j + 1;
                        //通水期数

                        qtyTable_dav5.Rows[rowindex][1] = jj + 1;

                        rowindex++;

                    }

                }
            }

        }

        private static void shuiguandingyi(DataTable qtyTable_dav4)
        {
            qtyTable_dav4.Columns.Add("水管号", System.Type.GetType("System.String"));//0
            qtyTable_dav4.Columns.Add("冷却直径", System.Type.GetType("System.String"));//1
            qtyTable_dav4.Columns.Add("管长", System.Type.GetType("System.String"));//2
            qtyTable_dav4.Columns.Add("qmax", System.Type.GetType("System.String"));//3
            qtyTable_dav4.Columns.Add("水热容量", System.Type.GetType("System.String"));//4
            qtyTable_dav4.Columns.Add("管材λ", System.Type.GetType("System.String"));//5
            qtyTable_dav4.Columns.Add("外径", System.Type.GetType("System.String"));//6
            qtyTable_dav4.Columns.Add("内径", System.Type.GetType("System.String"));//7 
            qtyTable_dav4.Columns.Add("材质", System.Type.GetType("System.String"));//8 
        }

        private static void rexuecanshu(DataTable qtyTable_dav2)
        {
            qtyTable_dav2.Columns.Add("材料编号", System.Type.GetType("System.String"));//0
            qtyTable_dav2.Columns.Add("λ", System.Type.GetType("System.String"));//1
            qtyTable_dav2.Columns.Add("C", System.Type.GetType("System.String"));//2
            qtyTable_dav2.Columns.Add("ρ", System.Type.GetType("System.String"));//3
            qtyTable_dav2.Columns.Add("θ1", System.Type.GetType("System.String"));//4
            qtyTable_dav2.Columns.Add("α1", System.Type.GetType("System.String"));//5
            qtyTable_dav2.Columns.Add("β1", System.Type.GetType("System.String"));//6
            qtyTable_dav2.Columns.Add("nfc", System.Type.GetType("System.String"));//7 
            qtyTable_dav2.Columns.Add("θ2", System.Type.GetType("System.String"));//8 
            qtyTable_dav2.Columns.Add("α2", System.Type.GetType("System.String"));//8 
            qtyTable_dav2.Columns.Add("β2", System.Type.GetType("System.String"));//8 
        }

        private static void yanghuamei(DataTable qtyTable_dav7)
        {
            qtyTable_dav7.Columns.Add("材料编号", System.Type.GetType("System.String"));//0
            qtyTable_dav7.Columns.Add("nf", System.Type.GetType("System.String"));//1
            qtyTable_dav7.Columns.Add("c0", System.Type.GetType("System.String"));//2
            qtyTable_dav7.Columns.Add("c1", System.Type.GetType("System.String"));//3
            qtyTable_dav7.Columns.Add("c2", System.Type.GetType("System.String"));//4
            qtyTable_dav7.Columns.Add("a1", System.Type.GetType("System.String"));//5
            qtyTable_dav7.Columns.Add("b1", System.Type.GetType("System.String"));//6
            qtyTable_dav7.Columns.Add("a2", System.Type.GetType("System.String"));//7 
            qtyTable_dav7.Columns.Add("b2", System.Type.GetType("System.String"));//8 
            qtyTable_dav7.Columns.Add("a3", System.Type.GetType("System.String"));//8 
            qtyTable_dav7.Columns.Add("b3", System.Type.GetType("System.String"));//8 
        }

        private void zishengtijibianxing(DataTable qtyTable8)
        {
            qtyTable8.Columns.Add("材料号\\龄期", System.Type.GetType("System.String"));//0

            if (textBox19.Text == "" || textBox19.Text.Contains("."))
                textBox19.Text = "0";
            if (!textBox19.Text.Contains("\t"))
            {
                int icount = Convert.ToInt32(textBox19.Text);
                for (int ip = 1; ip <= icount; ip++)
                {
                    qtyTable8.Columns.Add("" + ip, System.Type.GetType("System.String"));//0

                }
                if (textBox17.Text == "")
                    textBox17.Text = "0";

                int tx17 = Convert.ToInt32(textBox17.Text);
                for (int j = 0; j <= tx17; j++)
                {
                    qtyTable8.Rows.Add(qtyTable8.NewRow());
                    if (j > 0)
                        qtyTable8.Rows[j][0] = j;
                }
            }

        }

        private static void xubiancanshu(DataTable qtyTable_dav6)
        {
            qtyTable_dav6.Columns.Add("材料编号", System.Type.GetType("System.String"));//0
            qtyTable_dav6.Columns.Add("nfc", System.Type.GetType("System.String"));//1
            qtyTable_dav6.Columns.Add("k1", System.Type.GetType("System.String"));//2
            qtyTable_dav6.Columns.Add("k2", System.Type.GetType("System.String"));//3
            qtyTable_dav6.Columns.Add("k3", System.Type.GetType("System.String"));//4
            qtyTable_dav6.Columns.Add("A1", System.Type.GetType("System.String"));//5
            qtyTable_dav6.Columns.Add("A2", System.Type.GetType("System.String"));//6
            qtyTable_dav6.Columns.Add("A3", System.Type.GetType("System.String"));//7 
            qtyTable_dav6.Columns.Add("B1", System.Type.GetType("System.String"));//8 
            qtyTable_dav6.Columns.Add("B2", System.Type.GetType("System.String"));//8 
            qtyTable_dav6.Columns.Add("B3", System.Type.GetType("System.String"));//8 
            qtyTable_dav6.Columns.Add("D", System.Type.GetType("System.String"));//8 
        }

        private static void jibenlixuecanshu(DataTable qtyTable_dav5)
        {
            qtyTable_dav5.Columns.Add("材料编号", System.Type.GetType("System.String"));//0
            qtyTable_dav5.Columns.Add("平面应力", System.Type.GetType("System.String"));//1
            qtyTable_dav5.Columns.Add("E0", System.Type.GetType("System.String"));//2
            qtyTable_dav5.Columns.Add("E1", System.Type.GetType("System.String"));//3
            qtyTable_dav5.Columns.Add("μ", System.Type.GetType("System.String"));//4
            qtyTable_dav5.Columns.Add("β1", System.Type.GetType("System.String"));//5
            qtyTable_dav5.Columns.Add("β2", System.Type.GetType("System.String"));//6
            qtyTable_dav5.Columns.Add("α", System.Type.GetType("System.String"));//7 
            qtyTable_dav5.Columns.Add("Vx", System.Type.GetType("System.String"));//8 
            qtyTable_dav5.Columns.Add("Vy", System.Type.GetType("System.String"));//8 
            qtyTable_dav5.Columns.Add("Vz", System.Type.GetType("System.String"));//8 
            qtyTable_dav5.Columns.Add("ρ", System.Type.GetType("System.String"));//8 
            qtyTable_dav5.Columns.Add("nfe", System.Type.GetType("System.String"));//8 
            qtyTable_dav5.Columns.Add("τ0", System.Type.GetType("System.String"));//8 
        }

        private string[] splittx0(string fileText)
        {
            string[] fileTextQ = System.Text.RegularExpressions.Regex.Split(fileText, " ");
            return fileTextQ;

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int s = this.tabControl1.SelectedIndex;
            #region MyRegion
            //if (s == 1 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton2_Click(null, EventArgs.Empty);
            //}

            //if (s == 2 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton3_Click(null, EventArgs.Empty);
            //}
            //if (s == 3 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton4_Click(null, EventArgs.Empty);
            //}
            //if (s == 4 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton5_Click(null, EventArgs.Empty);
            //}
            //if (s == 5 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton6_Click(null, EventArgs.Empty);

            //}
            //if (s == 6 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton7_Click(null, EventArgs.Empty);
            //}

            //if (s == 7 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton8_Click(null, EventArgs.Empty);
            //}
            //if (s == 8 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton9_Click(null, EventArgs.Empty);
            //}
            //if (s == 9 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton10_Click(null, EventArgs.Empty);
            //}
            //if (s == 10 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton15_Click(null, EventArgs.Empty);
            //}
            //if (s == 11 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton12_Click(null, EventArgs.Empty);
            //}
            //if (s == 12 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton13_Click(null, EventArgs.Empty);
            //}
            //if (s == 13 && (nowfile == null || nowfile == ""))
            //{
            //    toolStripDropDownButton14_Click(null, EventArgs.Empty);

            //} 
            #endregion

            #region MyRegion
            if (s == 1)
            {

                toolStripDropDownButton2_Click(null, EventArgs.Empty);
            }

            if (s == 2)
            {
                toolStripDropDownButton3_Click(null, EventArgs.Empty);
            }
            if (s == 3)
            {
                toolStripDropDownButton4_Click(null, EventArgs.Empty);
            }
            if (s == 4)
            {
                toolStripDropDownButton5_Click(null, EventArgs.Empty);
            }
            if (s == 5)
            {
                //  toolStripDropDownButton6_Click(null, EventArgs.Empty);

            }
            if (s == 6)
            {
                toolStripDropDownButton7_Click(null, EventArgs.Empty);
            }

            if (s == 7)
            {
                toolStripDropDownButton8_Click(null, EventArgs.Empty);
            }
            if (s == 8)
            {
                //时步条件表格


                //时步条件表格——行数为《浇筑及计算时步》 浇筑次序tab 表格中，所有“计算次数”的总和，列数已知为27列。行数、列数一旦确定，请自动生成表格
                //比如浇筑次序tab 这里边就应该显示50行
                jisuancishu = 0;
                for (int i = 0; i < dataGridView13.RowCount; i++)
                {
                    if (dataGridView13.Rows[i].Cells["计算次数"].EditedFormattedValue != null && dataGridView13.Rows[i].Cells["计算次数"].EditedFormattedValue != "")
                        jisuancishu += Convert.ToInt32(dataGridView13.Rows[i].Cells["计算次数"].EditedFormattedValue.ToString());

                }
                if (dataGridView14.RowCount < jisuancishu)
                {
                    int dsds = jisuancishu - dataGridView14.RowCount;


                    for (int ir = 0; ir < dsds; ir++)
                        qtyTable_dav12.Rows.Add(qtyTable_dav12.NewRow());

                }
                this.bindingSource14.DataSource = qtyTable_dav12;
                this.dataGridView14.DataSource = this.bindingSource14;

                toolStripDropDownButton9_Click(null, EventArgs.Empty);
            }
            if (s == 9)
            {
                toolStripDropDownButton10_Click(null, EventArgs.Empty);
            }
            if (s == 10)
            {
                toolStripDropDownButton15_Click(null, EventArgs.Empty);
            }
            if (s == 11)
            {
                toolStripDropDownButton12_Click(null, EventArgs.Empty);
            }
            if (s == 12)
            {
                toolStripDropDownButton13_Click(null, EventArgs.Empty);
            }
            if (s == 13)
            {
                toolStripDropDownButton14_Click(null, EventArgs.Empty);

            }
            if (s == 14)
            {
                seepage_data_Click(null, EventArgs.Empty);

            }
            #endregion
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {
            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("els_para.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 2;

        }

        private void toolStripDropDownButton4_Click(object sender, EventArgs e)
        {
            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("temp_para.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 3;




        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {



        }

        private void textBox13_txchange()
        {
            if (textBox13.Text.Length < 1 || dataGridView3.RowCount <= 0)
                return;

            if (textBox13.Text != "" || dataGridView3.RowCount > 0)
            {

                var qtyTable = new DataTable();
                qtyTable.Columns.Add("βw", System.Type.GetType("System.String"));//0

                if (textBox13.Text.Contains("\t"))
                    return;
                int icount = Convert.ToInt32(textBox13.Text);
                for (int i = 1; i <= icount; i++)
                {
                    qtyTable.Columns.Add("β" + i, System.Type.GetType("System.String"));//0

                }
                #region 缓存处理
                iscache = true;//是缓存
                isallsave = 1;//批量保存标志
                allsave_index = 3;//第几个页
                toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

                string sp_txt = "";
                if (iscache == false)
                    nowfile = Alist.Find(v => v.Contains("temp_para.sap"));
                else
                    nowfile = cacheAlist.Find(v => v.Contains("temp_para.sap"));

                string[] fileText = File.ReadAllLines(nowfile);
                sp_txt = Read_temp_para(sp_txt, fileText);


                #endregion


                #region new

                //new
                if (dataGridView3.RowCount > 0)
                {
                    if (dataGridView3.ColumnCount > qtyTable.Columns.Count)
                    {
                        int rowcout = dataGridView3.ColumnCount;

                        for (int i = 0; i < rowcout - qtyTable.Columns.Count; i++)
                        {
                            dataGridView3.Columns.RemoveAt(dataGridView3.Columns.Count - 1);
                            qtyTable_dav3.Columns.RemoveAt(dataGridView3.Columns.Count);
                        }
                    }
                    else if (dataGridView3.ColumnCount < qtyTable.Columns.Count)
                    {
                        int davcount = dataGridView3.ColumnCount - 1;

                        for (int i = 0; i < qtyTable.Columns.Count - davcount; i++)
                        {
                            int nx = qtyTable_dav3.Columns.Count;

                            int clou = davcount + i;
                            bool ishave = false;

                            foreach (System.Data.DataColumn k in qtyTable_dav3.Columns)
                            {
                                string columnName = k.ColumnName;

                                if (clou.ToString() == columnName)
                                    ishave = true;

                            }
                            if (ishave == false)
                                qtyTable_dav3.Columns.Add("" + clou.ToString(), System.Type.GetType("System.String"));//0


                        }
                        this.bindingSource3.DataSource = qtyTable_dav3;
                        this.dataGridView3.DataSource = this.bindingSource3;
                    }
                }
                else
                {

                    this.bindingSource3.DataSource = qtyTable_dav3;
                    this.dataGridView3.DataSource = this.bindingSource3;

                }
                #endregion
                clearCache();
                //this.bindingSource3.DataSource = qtyTable;
                //this.dataGridView3.DataSource = this.bindingSource3;
            }
        }

        private void addDayButton_Click(object sender, EventArgs e)
        {
            //DataGridViewRow row = new DataGridViewRow();
            //DataGridViewTextBoxCell textboxcell = new DataGridViewTextBoxCell();
            //textboxcell.Value = "";
            //row.Cells.Add(textboxcell);
            //dataGridView3.Rows.Add(row);
            //clickdav.Rows.Add();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickdav = dataGridView2;
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            clickdav = dataGridView3;
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            //textBox19_txchange();
        }

        private void textBox19_txchange()
        {
            try
            {
                if (textBox19.Text != "" && isreopen == false)
                {
                    var qtyTable8_1 = new DataTable();
                    zishengtijibianxing(qtyTable8_1);
                    //var qtyTable = new DataTable();
                    //qtyTable.Columns.Add("材料号\\龄期", System.Type.GetType("System.String"));//0


                    //int icount = Convert.ToInt32(textBox19.Text);
                    //for (int i = 1; i <= icount; i++)
                    //{
                    //    qtyTable.Columns.Add("" + i, System.Type.GetType("System.String"));//0

                    //}
                    #region new
                    if (dataGridView8.RowCount > 0)
                    {
                        if (dataGridView8.ColumnCount > qtyTable8_1.Columns.Count)
                        {
                            int rowcout = dataGridView8.ColumnCount;

                            for (int i = 0; i < rowcout - qtyTable8_1.Columns.Count; i++)
                            {
                                dataGridView8.Columns.RemoveAt(dataGridView8.Columns.Count - 1);
                                qtyTable8.Columns.RemoveAt(dataGridView8.Columns.Count);
                            }
                        }
                        else if (dataGridView8.ColumnCount < qtyTable8_1.Columns.Count)
                        {
                            int davcount = dataGridView8.ColumnCount - 1;

                            for (int i = 0; i < qtyTable8_1.Columns.Count - davcount; i++)
                            {
                                int nx = qtyTable8.Columns.Count;

                                int clou = davcount + i;
                                bool ishave = false;

                                foreach (System.Data.DataColumn k in qtyTable8.Columns)
                                {
                                    string columnName = k.ColumnName;
                                    //columnType = k.DataType.ToString();
                                    if (clou.ToString() == columnName)
                                        ishave = true;

                                }
                                if (ishave == false)
                                    qtyTable8.Columns.Add("" + clou.ToString(), System.Type.GetType("System.String"));//0


                            }
                            this.bindingSource7.DataSource = qtyTable8;
                            this.dataGridView8.DataSource = this.bindingSource7;
                        }
                    }
                    else
                    {
                        this.bindingSource7.DataSource = qtyTable8_1;
                        this.dataGridView8.DataSource = this.bindingSource7;

                    }
                    #endregion
                    //this.bindingSource7.DataSource = qtyTable8;
                    //this.dataGridView8.DataSource = this.bindingSource7;
                    clearCache();

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void toolStripDropDownButton5_Click(object sender, EventArgs e)
        {
            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("placement_time_of_element.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 4;

        }

        private void toolStripDropDownButton6_Click(object sender, EventArgs e)
        {


            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("mesh.sap") && !Alist[i].Contains("joint"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 5;


            string DesktopPath = Convert.ToString(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop));


            if (File.Exists(DesktopPath + "\\ultreadit.exe"))
            {
                //  System.Diagnostics.Process.Start("ultraedit.exe", nowfile);
                System.Diagnostics.Process.Start(DesktopPath + "\\ultraedit.exe", nowfile);
            }
            else
            {
                if (File.Exists(DesktopPath + "\\ultreadit.exe.lnk"))
                    System.Diagnostics.Process.Start(DesktopPath + "\\ultreadit.exe.lnk", nowfile);
                else
                    MessageBox.Show(DesktopPath + "路径为空 或 在当前选择.sap文件路径下没有找到 ultraedit.exe", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void toolStripDropDownButton7_Click(object sender, EventArgs e)
        {
            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("strength_data.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 6;
        }

        private void toolStripDropDownButton8_Click(object sender, EventArgs e)
        {
            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("sup_step.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 7;
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            textBox23_txchange();
        }

        private void textBox23_txchange()
        {
            if (textBox23.Text.Length < 1 || dataGridView13.RowCount <= 0)
                return;


            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 7;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";

            ///num sup step
            ///
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("num_sup_step.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("num_sup_step.sap"));


            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_num_sup_step(sp_txt, fileText);


            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("sup_step.sap") && !v.Contains("num_sup_step.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("sup_step.sap") && !v.Contains("num_sup_step.sap"));

            fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_sup_step(sp_txt, fileText);

            #endregion


            var qtyTable_dav11_1 = new DataTable();
            jiaozhucixu(qtyTable_dav11_1);
            int icount1 = 20;

            Adddav11cloumn(icount1, qtyTable_dav11_1);

            if (textBox23.Text.Length >= 1)
            {
                int icount = Convert.ToInt32(textBox23.Text);
                for (int i11 = 1; i11 <= icount; i11++)
                {
                    //qtyTable_dav11.Columns.Add("△t" + i11, System.Type.GetType("System.String"));//0
                    qtyTable_dav11_1.Rows.Add(qtyTable_dav11_1.NewRow());
                    qtyTable_dav11_1.Rows[i11 - 1][0] = i11;
                }
            }
            //
            #region new
            if (dataGridView13.RowCount > 0)
            {
                if (dataGridView13.RowCount > qtyTable_dav11_1.Rows.Count)
                {
                    int rowcout = dataGridView13.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav11_1.Rows.Count; i++)
                        dataGridView13.Rows.RemoveAt(dataGridView13.Rows.Count - 1);
                }
                else if (dataGridView13.RowCount < qtyTable_dav11_1.Rows.Count)
                {
                    int davcount = dataGridView13.RowCount;
                    for (int i = 0; i < qtyTable_dav11_1.Rows.Count - davcount; i++)
                    {
                        qtyTable_dav11.Rows.Add(qtyTable_dav11.NewRow());
                        qtyTable_dav11.Rows[qtyTable_dav11.Rows.Count - 1][0] = davcount + 1 + i;

                    }
                    this.bindingSource13.DataSource = qtyTable_dav11;
                    this.dataGridView13.DataSource = this.bindingSource13;
                }
            }
            else
            {
                this.bindingSource13.DataSource = qtyTable_dav11_1;
                this.dataGridView13.DataSource = this.bindingSource13;

            }
            #endregion

            clearCache();
            //this.bindingSource13.DataSource = qtyTable_dav11;
            //this.dataGridView13.DataSource = this.bindingSource13;
        }

        private static void jiaozhucixu(DataTable qtyTable_dav11)
        {
            qtyTable_dav11.Columns.Add("浇筑序号", System.Type.GetType("System.String"));//0
            qtyTable_dav11.Columns.Add("单元数", System.Type.GetType("System.String"));//1
            qtyTable_dav11.Columns.Add("节点数", System.Type.GetType("System.String"));//2
            qtyTable_dav11.Columns.Add("计算次数", System.Type.GetType("System.String"));//3
        }

        private void toolStripDropDownButton9_Click(object sender, EventArgs e)
        {
            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("temp_bdy_3.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 8;
        }

        private void toolStripDropDownButton10_Click(object sender, EventArgs e)
        {
            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("point_disp_output.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 9;

        }

        private void toolStripDropDownButton15_Click(object sender, EventArgs e)
        {
            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("joint_mesh.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 10;

        }

        private void dataGridView18_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripDropDownButton12_Click(object sender, EventArgs e)
        {
            //灌浆数据 grouting_step.sap
            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("grouting_step.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 11;
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox26_txchange()
        {
            if (textBox26.Text.Length < 1 || dataGridView16.RowCount <= 0)
                return;

            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 11;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("grouting_step.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("grouting_step.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_grouting_step(sp_txt, fileText);


            #endregion

            if (textBox26.Text != "")
            {

                var qtyTable_dav18_1 = new DataTable();
                qtyTable_dav18_1.Columns.Add("单元号", System.Type.GetType("System.String"));//0
                qtyTable_dav18_1.Columns.Add("浇筑号", System.Type.GetType("System.String"));//1
                qtyTable_dav18_1.Columns.Add("计算步号", System.Type.GetType("System.String"));//1


                int icount = Convert.ToInt32(textBox26.Text);
                for (int i = 1; i <= icount; i++)
                {
                    //qtyTable_dav18.Rows.Add("" + i, System.Type.GetType("System.String"));//0
                    qtyTable_dav18_1.Rows.Add(qtyTable_dav18_1.NewRow());

                }
                #region new
                Datagridview_Addor_reduce(qtyTable_dav18_1, dataGridView16, qtyTable_dav18, bindingSource19);

                #endregion
                clearCache();

                //this.bindingSource19.DataSource = qtyTable_dav18;
                //this.dataGridView16.DataSource = this.bindingSource19;
            }
        }

        private void toolStripDropDownButton13_Click(object sender, EventArgs e)
        {
            //给定节点温度 Temp_fix.sap

            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("temp_fix.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 12;


        }

        private void textBox30_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox30_txchange()
        {
            if (textBox30.Text != "")
            {

                showdav20();
            }
        }

        private void showdav20()
        {
            var qtyTable_dav19_1 = new DataTable();
            qtyTable_dav19_1.Columns.Add("序号", System.Type.GetType("System.String"));//0
            qtyTable_dav19_1.Columns.Add("节点号", System.Type.GetType("System.String"));//1
            if (textBox30.Text.Length > 0 && textBox29.Text.Length > 0)
            {
                int icount = Convert.ToInt32(textBox30.Text);
                for (int i = 1; i <= icount; i++)
                {
                    qtyTable_dav19_1.Columns.Add("T" + i, System.Type.GetType("System.String"));//0

                }
                int icount1 = Convert.ToInt32(textBox29.Text);
                for (int i = 1; i <= icount1; i++)
                {
                    //qtyTable_dav19.Rows.Add("" + i, System.Type.GetType("System.String"));//0
                    qtyTable_dav19_1.Rows.Add(qtyTable_dav19_1.NewRow());
                    qtyTable_dav19_1.Rows[i - 1][0] = i;
                }
            }
            //new
            Datagridview_Addor_reduce(qtyTable_dav19_1, dataGridView20, qtyTable_dav19, bindingSource20);


            #region new
            datagridView_cloumnAddorRemove(qtyTable_dav19_1, dataGridView20, qtyTable_dav19, bindingSource20);
            #endregion


            //this.bindingSource20.DataSource = qtyTable_dav19;
            //this.dataGridView20.DataSource = this.bindingSource20;
        }

        private void datagridView_cloumnAddorRemove(DataTable qtyTable_dav19_1, DataGridView dataGridView20, DataTable qtyTable_dav19, BindingSource bindingSource7)
        {
            if (dataGridView20.RowCount > 0)
            {
                if (dataGridView20.ColumnCount > qtyTable_dav19_1.Columns.Count)
                {
                    int rowcout = dataGridView20.ColumnCount;

                    for (int i = 0; i < rowcout - qtyTable_dav19_1.Columns.Count; i++)
                    {
                        dataGridView20.Columns.RemoveAt(dataGridView20.Columns.Count - 1);
                        qtyTable_dav19.Columns.RemoveAt(dataGridView20.Columns.Count);
                    }
                }
                else if (dataGridView20.ColumnCount < qtyTable_dav19_1.Columns.Count)
                {
                    int davcount = dataGridView20.ColumnCount ;

                    for (int i = 0; i < qtyTable_dav19_1.Columns.Count - davcount; i++)
                    {
                        int nx = qtyTable_dav19.Columns.Count;

                        int clou = davcount-1 + i;
                        bool ishave = false;

                        foreach (System.Data.DataColumn k in qtyTable_dav19.Columns)
                        {
                            string columnName = k.ColumnName;
                            //columnType = k.DataType.ToString();
                            if ("T"+clou.ToString() == columnName)
                                ishave = true;

                        }
                        if (ishave == false)
                            qtyTable_dav19.Columns.Add("T" + clou.ToString(), System.Type.GetType("System.String"));//0


                    }
                    bindingSource7.DataSource = qtyTable_dav19;
                    dataGridView20.DataSource = bindingSource7;
                }
            }
            else
            {
                bindingSource7.DataSource = qtyTable_dav19_1;
                dataGridView20.DataSource = bindingSource7;

            }
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox29_txchange()
        {
            if (textBox29.Text.Length < 1 || dataGridView20.RowCount <= 0)
                return;

            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 12;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("temp_fix.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("temp_fix.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_temp_fix(sp_txt, fileText);


            #endregion

            if (textBox29.Text != "")
                showdav20();
        }

        private void toolStripDropDownButton14_Click(object sender, EventArgs e)
        {
            //库水河水温度 Temp_water.sap


            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("temp_water.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 13;
        }
        private void seepage_data_Click(object sender, EventArgs e)
        {
            //库水河水温度 Temp_water.sap


            if (Alist == null || Alist.Count < 1)
            {

                MessageBox.Show("请选择文件或者新建后再次尝试！", "Waring", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            nowfile = "";

            for (int i = 0; i < Alist.Count; i++)
            {

                if (Alist[i].Contains("seepage_data.sap"))
                {
                    nowfile = Alist[i];
                }
            }

            this.tabControl1.SelectedIndex = 14;
        }
        private void textBox35_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox35_txchange()
        {

            if (textBox35.Text.Length < 1 || dataGridView22.RowCount <= 0)
                return;

            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 13;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("temp_water.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("temp_water.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_temp_water(sp_txt, fileText);


            #endregion

            var qtyTable_dav21_1 = new DataTable();
            qtyTable_dav21_1.Columns.Add("序号", System.Type.GetType("System.String"));//0
            qtyTable_dav21_1.Columns.Add("水深", System.Type.GetType("System.String"));//1
            qtyTable_dav21_1.Columns.Add("T1", System.Type.GetType("System.String"));//2
            qtyTable_dav21_1.Columns.Add("T2", System.Type.GetType("System.String"));//3
            qtyTable_dav21_1.Columns.Add("T3", System.Type.GetType("System.String"));//4
            qtyTable_dav21_1.Columns.Add("T4", System.Type.GetType("System.String"));//5
            qtyTable_dav21_1.Columns.Add("T5", System.Type.GetType("System.String"));//6
            qtyTable_dav21_1.Columns.Add("T6", System.Type.GetType("System.String"));//7 
            qtyTable_dav21_1.Columns.Add("T7", System.Type.GetType("System.String"));//8 
            qtyTable_dav21_1.Columns.Add("T8", System.Type.GetType("System.String"));//8 
            qtyTable_dav21_1.Columns.Add("T9", System.Type.GetType("System.String"));//8 
            qtyTable_dav21_1.Columns.Add("T10", System.Type.GetType("System.String"));//8 
            qtyTable_dav21_1.Columns.Add("T11", System.Type.GetType("System.String"));//8 
            qtyTable_dav21_1.Columns.Add("T12", System.Type.GetType("System.String"));//8 
            if (textBox35.Text.Length > 0)
            {
                if (!textBox35.Text.Contains("\t"))
                {
                    int icount = Convert.ToInt32(textBox35.Text);
                    for (int i = 1; i <= icount; i++)
                    {
                        //qtyTable_dav21.Rows.Add("" + i, System.Type.GetType("System.String"));//0
                        qtyTable_dav21_1.Rows.Add(qtyTable_dav21_1.NewRow());
                        qtyTable_dav21_1.Rows[i - 1][0] = i;
                    }
                }
            }

            //new
            Datagridview_Addor_reduce(qtyTable_dav21_1, dataGridView22, qtyTable_dav21, bindingSource23);
            clearCache();


            //this.bindingSource23.DataSource = qtyTable_dav21;
            //this.dataGridView22.DataSource = this.bindingSource23;
        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {




        }

        private void textBox33_txchange()
        {
            if (textBox33.Text.Length < 1 || dataGridView24.RowCount <= 0)
                return;

            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 13;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("temp_water.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("temp_water.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_temp_water(sp_txt, fileText);


            #endregion


            var qtyTable_dav20_1 = new DataTable();
            qtyTable_dav20_1.Columns.Add("序号", System.Type.GetType("System.String"));//0
            qtyTable_dav20_1.Columns.Add("水深", System.Type.GetType("System.String"));//1
            qtyTable_dav20_1.Columns.Add("T1", System.Type.GetType("System.String"));//2
            qtyTable_dav20_1.Columns.Add("T2", System.Type.GetType("System.String"));//3
            qtyTable_dav20_1.Columns.Add("T3", System.Type.GetType("System.String"));//4
            qtyTable_dav20_1.Columns.Add("T4", System.Type.GetType("System.String"));//5
            qtyTable_dav20_1.Columns.Add("T5", System.Type.GetType("System.String"));//6
            qtyTable_dav20_1.Columns.Add("T6", System.Type.GetType("System.String"));//7 
            qtyTable_dav20_1.Columns.Add("T7", System.Type.GetType("System.String"));//8 
            qtyTable_dav20_1.Columns.Add("T8", System.Type.GetType("System.String"));//8 
            qtyTable_dav20_1.Columns.Add("T9", System.Type.GetType("System.String"));//8 
            qtyTable_dav20_1.Columns.Add("T10", System.Type.GetType("System.String"));//8 
            qtyTable_dav20_1.Columns.Add("T11", System.Type.GetType("System.String"));//8 
            qtyTable_dav20_1.Columns.Add("T12", System.Type.GetType("System.String"));//8 

            if (textBox33.Text.Length > 0)
            {
                if (!textBox33.Text.Contains("\t"))
                {
                    int icount = Convert.ToInt32(textBox33.Text);
                    for (int i = 1; i <= icount; i++)
                    {
                        //qtyTable_dav20.Rows.Add("" + i, System.Type.GetType("System.String"));//0
                        qtyTable_dav20_1.Rows.Add(qtyTable_dav20_1.NewRow());
                        qtyTable_dav20_1.Rows[i - 1][0] = i;
                    }
                }
            }


            //new
            Datagridview_Addor_reduce(qtyTable_dav20_1, dataGridView24, qtyTable_dav20, bindingSource22);

            clearCache();

            //this.bindingSource22.DataSource = qtyTable_dav20;
            //this.dataGridView24.DataSource = this.bindingSource22;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            if (File.Exists(folderpath + "\\saptis.exe"))
            {
                //System.Diagnostics.Process.Start(folderpath + "\\saptis.exe");

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.WorkingDirectory = folderpath;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = folderpath + "\\saptis.bat";
                p.Start();

                //proc.StartInfo.CreateNoWindow = true;
                //proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行

                //MessageBox.Show(" 已执行完毕!", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                p.WaitForExit();
            }
            else
                MessageBox.Show(folderpath + "路径为空 或 在当前选择.sap文件路径下没有找到 saptis.exe", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void delDayButton_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {


        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void radioButton1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
                radioButton4.Checked = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true)
                radioButton3.Checked = false;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
                radioButton5.Checked = false;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
                radioButton6.Checked = false;
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            if (textBox17.Text.Length < 1 || dataGridView6.RowCount <= 0)
                return;


            //
            //textBox17_txchange();


        }

        private void textBox17_txchange()
        {
            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 2;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("els_para.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("els_para.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = read_els_para(sp_txt, fileText);


            #endregion

            #region 方法
            int tx17 = Convert.ToInt32(textBox17.Text);
            //基本力学参数
            var qtyTable_dav5_1 = new DataTable();
            jibenlixuecanshu(qtyTable_dav5_1);


            for (int j = 0; j < tx17; j++)
            {
                qtyTable_dav5_1.Rows.Add(qtyTable_dav5_1.NewRow());
                qtyTable_dav5_1.Rows[j][0] = j + 1;

            }

            //new
            if (dataGridView6.RowCount > 0)
            {
                if (dataGridView6.RowCount > qtyTable_dav5_1.Rows.Count)
                {
                    int rowcout = dataGridView6.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav5_1.Rows.Count; i++)
                        dataGridView6.Rows.RemoveAt(dataGridView6.Rows.Count - 1);
                }
                else if (dataGridView6.RowCount < qtyTable_dav5_1.Rows.Count)
                {
                    //for (int i = 0; i < qtyTable_dav5.Rows.Count - dataGridView6.RowCount; i++)
                    {    // dataGridView6.Rows.Add();
                        int davcount = dataGridView6.RowCount;
                        for (int i = 0; i < qtyTable_dav5_1.Rows.Count - davcount; i++)
                        {
                            qtyTable_dav5.Rows.Add(qtyTable_dav5.NewRow());
                            qtyTable_dav5.Rows[qtyTable_dav5.Rows.Count - 1][0] = davcount + 1 + i;

                        }
                        this.bindingSource6.DataSource = qtyTable_dav5;
                        this.dataGridView6.DataSource = this.bindingSource6;

                    }
                }
            }
            else
            {

                this.bindingSource6.DataSource = qtyTable_dav5;
                this.dataGridView6.DataSource = this.bindingSource6;

            }
            //徐变参数
            var qtyTable_dav6_1 = new DataTable();
            xubiancanshu(qtyTable_dav6_1);
            for (int j = 0; j < tx17; j++)
            {
                qtyTable_dav6_1.Rows.Add(qtyTable_dav6_1.NewRow());
                qtyTable_dav6_1.Rows[j][0] = j + 1;
            }
            //new
            if (dataGridView7.RowCount > 0)
            {
                if (dataGridView7.RowCount > qtyTable_dav6_1.Rows.Count)
                {
                    int rowcout = dataGridView7.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav6_1.Rows.Count; i++)
                        dataGridView7.Rows.RemoveAt(dataGridView7.Rows.Count - 1);
                }
                else if (dataGridView7.RowCount < qtyTable_dav6_1.Rows.Count)
                {
                    int davcount = dataGridView7.RowCount;
                    for (int i = 0; i < qtyTable_dav6_1.Rows.Count - davcount; i++)
                    {
                        qtyTable_dav6.Rows.Add(qtyTable_dav6.NewRow());
                        qtyTable_dav6.Rows[qtyTable_dav6.Rows.Count - 1][0] = davcount + 1 + i;

                    }
                    this.bindingSource8.DataSource = qtyTable_dav6;
                    this.dataGridView7.DataSource = this.bindingSource8;
                }
            }
            else
            {
                this.bindingSource8.DataSource = qtyTable_dav6_1;
                this.dataGridView7.DataSource = this.bindingSource8;

            }
            //自生体积变形
            var qtyTable8_1 = new DataTable();
            zishengtijibianxing(qtyTable8_1);

            //new
            if (dataGridView8.RowCount > 0)
            {
                if (dataGridView8.RowCount > qtyTable8_1.Rows.Count)
                {
                    int rowcout = dataGridView8.RowCount;

                    for (int i = 0; i < rowcout - qtyTable8_1.Rows.Count; i++)
                        dataGridView8.Rows.RemoveAt(dataGridView8.Rows.Count - 1);
                }
                else if (dataGridView8.RowCount < qtyTable8_1.Rows.Count)
                {
                    int davcount = dataGridView8.RowCount;
                    for (int i = 0; i < qtyTable8_1.Rows.Count - davcount; i++)
                    {
                        qtyTable8.Rows.Add(qtyTable8.NewRow());
                        qtyTable8.Rows[qtyTable8.Rows.Count - 1][0] = davcount + 1 + i;

                    }
                    this.bindingSource7.DataSource = qtyTable8;
                    this.dataGridView8.DataSource = this.bindingSource7;

                }
            }
            else
            {
                this.bindingSource7.DataSource = qtyTable8_1;
                this.dataGridView8.DataSource = this.bindingSource7;
            }
            //氧化镁
            var qtyTable_dav7_1 = new DataTable();
            yanghuamei(qtyTable_dav7_1);
            for (int j = 0; j < tx17; j++)
            {
                qtyTable_dav7_1.Rows.Add(qtyTable_dav7_1.NewRow());
                qtyTable_dav7_1.Rows[j][0] = j + 1;

            }
            //new
            if (dataGridView9.RowCount > 0)
            {
                if (dataGridView9.RowCount > qtyTable_dav7_1.Rows.Count)
                {
                    int rowcout = dataGridView9.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav7_1.Rows.Count; i++)
                        dataGridView9.Rows.RemoveAt(dataGridView9.Rows.Count - 1);
                }
                else if (dataGridView9.RowCount < qtyTable_dav7_1.Rows.Count)
                {
                    int davcount = dataGridView9.RowCount;
                    for (int i = 0; i < qtyTable_dav7_1.Rows.Count - davcount; i++)
                    {
                        qtyTable_dav7.Rows.Add(qtyTable_dav7.NewRow());
                        qtyTable_dav7.Rows[qtyTable_dav7.Rows.Count - 1][0] = davcount + 1 + i;

                    }
                    this.bindingSource9.DataSource = qtyTable_dav7;
                    this.dataGridView9.DataSource = this.bindingSource9;

                }
            }
            else
            {
                this.bindingSource9.DataSource = qtyTable_dav7_1;
                this.dataGridView9.DataSource = this.bindingSource9;

            }
            //热学参数表格——行数由 “基本材料参数”选项卡中的材料种数确定，列数已知为11列。行数、列数一旦确定，请自动生成表格，并对表格第一列的材料编号自动生成。

            var qtyTable_dav2_1 = new DataTable();
            rexuecanshu(qtyTable_dav2_1);

            for (int j = 0; j < tx17; j++)
            {
                qtyTable_dav2_1.Rows.Add(qtyTable_dav2_1.NewRow());
                qtyTable_dav2_1.Rows[j][0] = j + 1;

            }
            //new
            if (dataGridView2.RowCount > 0)
            {
                if (dataGridView2.RowCount > qtyTable_dav2_1.Rows.Count)
                {
                    int rowcout = dataGridView2.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav2_1.Rows.Count; i++)
                        dataGridView2.Rows.RemoveAt(dataGridView2.Rows.Count - 1);
                }
                else if (dataGridView2.RowCount < qtyTable_dav2_1.Rows.Count)
                {
                    int davcount = dataGridView2.RowCount;

                    for (int i = 0; i < qtyTable_dav2_1.Rows.Count - davcount; i++)
                    {
                        qtyTable_dav2.Rows.Add(qtyTable_dav2.NewRow());
                        qtyTable_dav2.Rows[qtyTable_dav2.Rows.Count - 1][0] = davcount + 1 + i;

                    }
                    this.bindingSource2.DataSource = qtyTable_dav2;
                    this.dataGridView2.DataSource = this.bindingSource2;

                }
            }
            else
            {

                this.bindingSource2.DataSource = qtyTable_dav2_1;
                this.dataGridView2.DataSource = this.bindingSource2;
            }

            clearCache();
            #endregion
        }

        private void clearCache()
        {
            iscache = false;//是缓存
            isallsave = 0;//批量保存标志
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_txchange()
        {
            if (textBox15.Text.Length < 1 || dataGridView4.RowCount <= 0)
                return;


            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 3;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("temp_para.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("temp_para.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_temp_para(sp_txt, fileText);


            #endregion

            if (textBox15.Text.Length < 1)
                return;
            //int tx17 = Convert.ToInt32(textBox17.Text);
            int tx17 = Convert.ToInt32(textBox15.Text);

            var qtyTable_dav4_1 = new DataTable();
            shuiguandingyi(qtyTable_dav4_1);

            for (int j = 0; j < tx17; j++)
            {
                qtyTable_dav4_1.Rows.Add(qtyTable_dav4_1.NewRow());
                qtyTable_dav4_1.Rows[j][0] = j + 1;

            }

            #region new 缓存
            if (dataGridView4.RowCount > 0)
            {
                if (dataGridView4.RowCount > qtyTable_dav4_1.Rows.Count)
                {
                    int rowcout = dataGridView4.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav4_1.Rows.Count; i++)
                        dataGridView4.Rows.RemoveAt(dataGridView4.Rows.Count - 1);
                }
                else if (dataGridView4.RowCount < qtyTable_dav4_1.Rows.Count)
                {

                    {
                        int davcount = dataGridView4.RowCount;
                        for (int i = 0; i < qtyTable_dav4_1.Rows.Count - davcount; i++)
                        {
                            qtyTable_dav4.Rows.Add(qtyTable_dav4.NewRow());
                            qtyTable_dav4.Rows[qtyTable_dav4.Rows.Count - 1][0] = davcount + 1 + i;

                        }
                        this.bindingSource4.DataSource = qtyTable_dav4;
                        this.dataGridView4.DataSource = this.bindingSource4;

                    }
                }
            }
            else
            {

                this.bindingSource4.DataSource = qtyTable_dav4_1;
                this.dataGridView4.DataSource = this.bindingSource4;

            }
            #endregion

            #region MyRegion

            #endregion
            clearCache();
            //this.bindingSource4.DataSource = qtyTable_dav4;
            //this.dataGridView4.DataSource = this.bindingSource4;
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox16_txchange()
        {
            if (textBox16.Text.Length < 1 || textBox15.Text.Length < 1 || dataGridView5.RowCount <= 0)
                return;


            var qtyTable_dav5_1 = new DataTable();
            tongshuocanshu(qtyTable_dav5_1);
            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 3;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("temp_para.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("temp_para.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_temp_para(sp_txt, fileText);


            #endregion

            #region new
            if (dataGridView5.RowCount > 0)
            {
                if (dataGridView5.RowCount > qtyTable_dav5_1.Rows.Count)
                {
                    int rowcout = dataGridView5.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav5_1.Rows.Count; i++)
                        dataGridView5.Rows.RemoveAt(dataGridView5.Rows.Count - 1);
                }
                else if (dataGridView5.RowCount < qtyTable_dav5_1.Rows.Count)
                {

                    {
                        int davcount = dataGridView5.RowCount;
                        for (int i = 0; i < qtyTable_dav5_1.Rows.Count - davcount; i++)
                        {
                            qtyTable_dav4.Rows.Add(qtyTable_dav4.NewRow());
                            qtyTable_dav4.Rows[qtyTable_dav4.Rows.Count - 1][0] = davcount + 1 + i;

                        }
                        this.bindingSource4.DataSource = qtyTable_dav4;
                        this.dataGridView5.DataSource = this.bindingSource4;

                    }
                }
            }
            else
            {
                this.bindingSource5.DataSource = qtyTable_dav5_1;
                this.dataGridView5.DataSource = this.bindingSource5;

            }
            #endregion

            //this.bindingSource5.DataSource = qtyTable_dav5;
            //this.dataGridView5.DataSource = this.bindingSource5;
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox20_txchange()
        {
            if (textBox20.Text.Length < 1 || dataGridView10.RowCount <= 0)
                return;
            int tx20 = Convert.ToInt32(textBox20.Text);

            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 4;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("placement_time_of_element.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("placement_time_of_element.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_placement_time_of_element(sp_txt, fileText);


            #endregion



            var qtyTable_dav8_1 = new DataTable();
            wajueyuhuitian(qtyTable_dav8_1);
            for (int j = 0; j < tx20; j++)
            {
                qtyTable_dav8_1.Rows.Add(qtyTable_dav8_1.NewRow());
                qtyTable_dav8_1.Rows[j][0] = j + 1;
            }

            #region new
            if (dataGridView10.RowCount > 0)
            {
                if (dataGridView10.RowCount > qtyTable_dav8_1.Rows.Count)
                {
                    int rowcout = dataGridView10.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav8_1.Rows.Count; i++)
                        dataGridView10.Rows.RemoveAt(dataGridView10.Rows.Count - 1);
                }
                else if (dataGridView10.RowCount < qtyTable_dav8_1.Rows.Count)
                {

                    {
                        int davcount = dataGridView10.RowCount;
                        for (int i = 0; i < qtyTable_dav8_1.Rows.Count - davcount; i++)
                        {
                            qtyTable_dav8.Rows.Add(qtyTable_dav8.NewRow());
                            qtyTable_dav8.Rows[qtyTable_dav8.Rows.Count - 1][0] = davcount + 1 + i;

                        }
                        this.bindingSource10.DataSource = qtyTable_dav8;
                        this.dataGridView10.DataSource = this.bindingSource10;

                    }
                }
            }
            else
            {
                this.bindingSource10.DataSource = qtyTable_dav8_1;
                this.dataGridView10.DataSource = this.bindingSource10;

            }
            #endregion



            clearCache();

            //this.bindingSource10.DataSource = qtyTable_dav8;
            //this.dataGridView10.DataSource = this.bindingSource10;
        }

        private void dataGridView13_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {



            }
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox25_txchange()
        {
            if (textBox25.Text.Length < 1 || dataGridView15.RowCount <= 0)
                return;

            if (textBox25.Text.Length < 1)
                return;

            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 9;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";

            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("point_disp_output.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("point_disp_output.sap"));


            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_point_disp_output(sp_txt, fileText);


            #endregion

            int tx25 = Convert.ToInt32(textBox25.Text);
            var qtyTable_dav13_1 = new DataTable();

            shuchuweiyidian(qtyTable_dav13_1);


            for (int j = 0; j < tx25; j++)
            {
                qtyTable_dav13_1.Rows.Add(qtyTable_dav13_1.NewRow());
                qtyTable_dav13_1.Rows[j][0] = j + 1;
            }
            #region new
            if (dataGridView15.RowCount > 0)
            {
                if (dataGridView15.RowCount > qtyTable_dav13_1.Rows.Count)
                {
                    int rowcout = dataGridView15.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav13_1.Rows.Count; i++)
                        dataGridView15.Rows.RemoveAt(dataGridView15.Rows.Count - 1);
                }
                else if (dataGridView15.RowCount < qtyTable_dav13_1.Rows.Count)
                {
                    int davcount = dataGridView15.RowCount;
                    for (int i = 0; i < qtyTable_dav13_1.Rows.Count - davcount; i++)
                    {
                        qtyTable_dav13.Rows.Add(qtyTable_dav13.NewRow());
                        qtyTable_dav13.Rows[qtyTable_dav13.Rows.Count - 1][0] = davcount + 1 + i;

                    }
                    this.bindingSource15.DataSource = qtyTable_dav13;
                    this.dataGridView15.DataSource = this.bindingSource15;

                }
            }
            else
            {
                this.bindingSource15.DataSource = qtyTable_dav13_1;
                this.dataGridView15.DataSource = this.bindingSource15;

            }
            #endregion

            //this.bindingSource15.DataSource = qtyTable_dav13;
            //this.dataGridView15.DataSource = this.bindingSource15;
        }

        private void textBox27_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox27_txchange()
        {

            if (textBox27.Text.Length < 1 || dataGridView19.RowCount <= 0)
                return;


            if (textBox27.Text.Length < 1)
                return;

            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 10;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("joint_mesh.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("joint_mesh.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_joint_mesh(sp_txt, fileText);


            #endregion
            initialization_tx27_tx28();
            return;

            int tx27 = Convert.ToInt32(textBox27.Text);

            var qtyTable_dav14_1 = new DataTable();
            gangduxishu(qtyTable_dav14_1);

            for (int j = 0; j < tx27; j++)
            {
                qtyTable_dav14_1.Rows.Add(qtyTable_dav14_1.NewRow());
                qtyTable_dav14_1.Rows[j][0] = j + 1;
            }

            #region new
            Datagridview_Addor_reduce(qtyTable_dav14_1, dataGridView19, qtyTable_dav14, bindingSource16);
            #endregion
            clearCache();


            //this.bindingSource16.DataSource = qtyTable_dav14;
            //this.dataGridView19.DataSource = this.bindingSource16;


            var qtyTable_dav15_1 = new DataTable();
            qiangduxishu(qtyTable_dav15_1);
            for (int j = 0; j < tx27; j++)
            {
                qtyTable_dav15_1.Rows.Add(qtyTable_dav15_1.NewRow());
                qtyTable_dav15_1.Rows[j][0] = j + 1;
            }

            #region new
            Datagridview_Addor_reduce(qtyTable_dav15_1, dataGridView18, qtyTable_dav15, bindingSource17);
            #endregion
            clearCache();



            //this.bindingSource17.DataSource = qtyTable_dav15;
            //this.dataGridView18.DataSource = this.bindingSource17;
        }

        private void textBox28_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox28_txchange()
        {
            if (textBox28.Text.Length < 1 || dataGridView17.RowCount <= 0)
                return;


            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 10;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("joint_mesh.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("joint_mesh.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_joint_mesh(sp_txt, fileText);


            #endregion


            initialization_tx27_tx28();



            //this.bindingSource18.DataSource = qtyTable_dav16;
            //this.dataGridView17.DataSource = this.bindingSource18;
        }

        private void initialization_tx27_tx28()
        {
            int tx28 = Convert.ToInt32(textBox28.Text);

            var qtyTable_dav16_1 = new DataTable();
            fengdanyuanjiedainbian(qtyTable_dav16_1);

            for (int j = 0; j < tx28; j++)
            {
                qtyTable_dav16_1.Rows.Add(qtyTable_dav16_1.NewRow());
                qtyTable_dav16_1.Rows[j][0] = j + 1;
            }

            #region new
            Datagridview_Addor_reduce(qtyTable_dav16_1, dataGridView17, qtyTable_dav16, bindingSource18);
            #endregion
            clearCache();


            int tx27 = Convert.ToInt32(textBox27.Text);

            var qtyTable_dav14_1 = new DataTable();
            gangduxishu(qtyTable_dav14_1);

            for (int j = 0; j < tx27; j++)
            {
                qtyTable_dav14_1.Rows.Add(qtyTable_dav14_1.NewRow());
                qtyTable_dav14_1.Rows[j][0] = j + 1;
            }

            #region new
            Datagridview_Addor_reduce(qtyTable_dav14_1, dataGridView19, qtyTable_dav14, bindingSource16);
            #endregion
            clearCache();


            //this.bindingSource16.DataSource = qtyTable_dav14;
            //this.dataGridView19.DataSource = this.bindingSource16;


            var qtyTable_dav15_1 = new DataTable();
            qiangduxishu(qtyTable_dav15_1);
            for (int j = 0; j < tx27; j++)
            {
                qtyTable_dav15_1.Rows.Add(qtyTable_dav15_1.NewRow());
                qtyTable_dav15_1.Rows[j][0] = j + 1;
            }

            #region new
            Datagridview_Addor_reduce(qtyTable_dav15_1, dataGridView18, qtyTable_dav15, bindingSource17);
            #endregion
            clearCache();
        }

        private void Datagridview_Addor_reduce(DataTable qtyTable_dav16_1, DataGridView dataGridView17, DataTable qtyTable_dav16, BindingSource bindingSource18)
        {
            if (dataGridView17.RowCount > 0)
            {
                int davcount = dataGridView17.RowCount;

                if (dataGridView17.RowCount > qtyTable_dav16_1.Rows.Count)
                {
                    int rowcout = dataGridView17.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav16_1.Rows.Count; i++)
                        dataGridView17.Rows.RemoveAt(dataGridView17.Rows.Count - 1);
                }
                else if (dataGridView17.RowCount < qtyTable_dav16_1.Rows.Count)
                {

                    {
                        for (int i = 0; i < qtyTable_dav16_1.Rows.Count - davcount; i++)
                        {
                            qtyTable_dav16.Rows.Add(qtyTable_dav16.NewRow());
                            qtyTable_dav16.Rows[qtyTable_dav16.Rows.Count - 1][0] = davcount + 1 + i;

                        }
                        bindingSource18.DataSource = qtyTable_dav16;
                        dataGridView17.DataSource = bindingSource18;

                    }
                }
                for (int i = 0; i < qtyTable_dav16.Rows.Count; i++)
                {
                    // qtyTable_dav16.Rows.Add(qtyTable_dav16.NewRow());
                    qtyTable_dav16.Rows[i][0] = i + 1;
 
                }
                bindingSource18.DataSource = qtyTable_dav16;
                dataGridView17.DataSource = bindingSource18;

            }
            else
            {
                bindingSource18.DataSource = qtyTable_dav16_1;
                dataGridView17.DataSource = bindingSource18;

            }
        }


        public void DataGirdViewCellPaste(DataGridView DBGrid)
        {

            this.pbStatus.Value = 0;
            //changeindex = new List<int>();
            try
            {
                string firsttext = "";

                // 获取剪切板的内容，并按行分割  
                string pasteText = "";
                pasteText = Clipboard.GetText();

                if (string.IsNullOrEmpty(pasteText))
                    return;
                if (pasteText == "pasteText")
                {
                    return;
                }
                int tnum = 0;
                int nnum = 0;
                //获得当前剪贴板内容的行、列数
                for (int i = 0; i < pasteText.Length; i++)
                {
                    if (pasteText.Substring(i, 1) == "\t")
                    {
                        tnum++;
                    }
                    if (pasteText.Substring(i, 1) == "\n")
                    {
                        nnum++;
                    }
                }
                Object[,] data;
                //粘贴板上的数据来自于EXCEL时，每行末都有\n，在DATAGRIDVIEW内复制时，最后一行末没有\n
                if (pasteText.Substring(pasteText.Length - 1, 1) == "\n")
                {
                    nnum = nnum - 1;
                }
                tnum = tnum / (nnum + 1);
                data = new object[nnum + 1, tnum + 1];//定义一个二维数组

                String rowstr;
                rowstr = "";
                //赋值粘贴的row 
                Pasterow = nnum;


                //MessageBox.Show(pasteText.IndexOf("B").ToString());
                //对数组赋值
                for (int i = 0; i < (nnum + 1); i++)
                {
                    for (int colIndex = 0; colIndex < (tnum + 1); colIndex++)
                    {
                        //一行中的最后一列
                        if (colIndex == tnum && pasteText.IndexOf("\r") != -1)
                        {
                            rowstr = pasteText.Substring(0, pasteText.IndexOf("\r"));
                            if (firsttext == "")
                                firsttext = rowstr;

                        }
                        //最后一行的最后一列
                        if (colIndex == tnum && pasteText.IndexOf("\r") == -1)
                        {
                            rowstr = pasteText.Substring(0);
                        }
                        //其他行列
                        if (colIndex != tnum)
                        {
                            rowstr = pasteText.Substring(0, pasteText.IndexOf("\t"));
                            pasteText = pasteText.Substring(pasteText.IndexOf("\t") + 1);
                        }
                        data[i, colIndex] = rowstr;
                    }
                    //截取下一行数据
                    pasteText = pasteText.Substring(pasteText.IndexOf("\n") + 1);
                }
                //获取当前选中单元格所在的列序号
                int curntindex = DBGrid.CurrentRow.Cells.IndexOf(DBGrid.CurrentCell);
                //获取获取当前选中单元格所在的行序号
                int rowindex = DBGrid.CurrentRow.Index;
                //MessageBox.Show(curntindex.ToString ());
                for (int j = 0; j < (nnum + 1); j++)
                {
                    for (int colIndex = 0; colIndex < (tnum + 1); colIndex++)
                    {
                        if (!DBGrid.Columns[colIndex + curntindex].Visible)
                        {
                            continue;
                        }
                        if (!DBGrid.Rows[j + rowindex].Cells[colIndex + curntindex].ReadOnly)
                        {
                            double newconver = 99999.111111;
                            #region     //处理" 2,129.80 "  格式字样的数据
                            try
                            {
                                bool ischina = HasChineseTest(data[j, colIndex].ToString());
                                if (ischina == false && data[j, colIndex] != "")
                                {
                                    if (Regex.Matches(data[j, colIndex].ToString(), "[a-zA-Z]").Count <= 0 && !data[j, colIndex].ToString().Contains("/"))
                                    {
                                        string a = data[j, colIndex].ToString().Trim();
                                        newconver = Convert.ToDouble(a);
                                    }
                                }

                            }
                            catch
                            {


                            }
                            #endregion
                            if (newconver != 99999.111111)
                                DBGrid.Rows[j + rowindex].Cells[colIndex + curntindex].Value = newconver;
                            else
                                DBGrid.Rows[j + rowindex].Cells[colIndex + curntindex].Value = data[j, colIndex];

                        }

                    }
                    int inll = j + rowindex;
                    changeindex.Add(inll);
                }
                Clipboard.Clear();
                //  DBGrid.Rows[0 + rowindex].Cells[0 + curntindex].Value = firsttext;


            }
            catch
            {
                Clipboard.Clear();
                //MessageBox.Show("粘贴区域大小不一致");
                return;
            }
        }
        //判断是否为汉字
        public bool HasChineseTest(string text)
        {
            //string text = "是不是汉字，ABC,keleyi.com";
            char[] c = text.ToCharArray();
            bool ischina = false;

            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] >= 0x4e00 && c[i] <= 0x9fbb)
                {
                    ischina = true;

                }
                else
                {
                    //  ischina = false;
                }
            }
            return ischina;

        }

        private void dataGridView6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                DataGirdViewCellPaste(dataGridView6);
            }
        }

        private void dataGridView7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                DataGirdViewCellPaste(dataGridView7);
            }
        }

        private void dataGridView8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                DataGirdViewCellPaste(dataGridView8);
            }

        }

        private void dataGridView9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                DataGirdViewCellPaste(dataGridView9);
            }

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Update all Data , continue ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

            }
            else
                return;


            int i = this.tabControl1.TabCount;

            for (int j = 0; j < i; j++)
            {
                iscache = false;//全部保存时候 改成正式保存的类型
                isallsave = 1;
                allsave_index = j + 1;
                toolStripButton1_Click(null, EventArgs.Empty);
                if (isallsave == 1)
                    return;

            }
            openfile(folderpath);
            this.toolStripLabel1.Text = "刷新完成";
            MessageBox.Show("全部更新完成，请查看！");

        }

        private void textBox40_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox40_txchange()
        {
            if (textBox40.Text.Length < 1 || dataGridView25.RowCount <= 0)
                return;

            #region 缓存处理
            cache_seepage_data();

            #endregion

            int tx28 = Convert.ToInt32(textBox40.Text);

            var qtyTable_dav23_1 = new DataTable();
            baoheshentouxishu(qtyTable_dav23_1);

            for (int j = 0; j < tx28; j++)
            {
                qtyTable_dav23_1.Rows.Add(qtyTable_dav23_1.NewRow());
                qtyTable_dav23_1.Rows[j][0] = j + 1;
            }

            #region new
            Datagridview_Addor_reduce(qtyTable_dav23_1, dataGridView25, qtyTable_dav23, bindingSource25);
            #endregion


        }

        private void cache_seepage_data()
        {
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 14;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("seepage_data.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("seepage_data.sap"));

            string[] fileText = File.ReadAllLines(nowfile);
            sp_txt = Read_seepage_data(sp_txt, fileText);

        }

        private void textBox39_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox39_txchange()
        {
            if (textBox39.Text.Length < 1 || dataGridView23.RowCount <= 0)
                return;

            #region 缓存处理
            cache_seepage_data();

            #endregion

            int tx28 = Convert.ToInt32(textBox39.Text);

            var qtyTable_dav24_1 = new DataTable();
            meizushujuhangshu(qtyTable_dav24_1);

            for (int j = 0; j < tx28; j++)
            {
                qtyTable_dav24_1.Rows.Add(qtyTable_dav24_1.NewRow());
                qtyTable_dav24_1.Rows[j][0] = j + 1;
            }

            #region new
            //  Datagridview_Addor_reduce(qtyTable_dav24_1, dataGridView23, qtyTable_dav24, bindingSource26);
            #endregion
        }

        private void textBox45_TextChanged(object sender, EventArgs e)
        {



        }

        private void textBox45_txchange()
        {
            if (textBox45.Text.Length < 1 || dataGridView26.RowCount <= 0)
                return;
            #region 缓存处理
            cache_seepage_data();

            #endregion
            int tx28 = Convert.ToInt32(textBox45.Text);

            var qtyTable_dav26_1 = new DataTable();
            butoushuimiangeshui(qtyTable_dav26_1);

            for (int j = 0; j < tx28; j++)
            {
                // qtyTable_dav26_1.Rows.Add(qtyTable_dav26_1.NewRow());
                qtyTable_dav26_1.Rows[j][0] = j + 1;
            }
            #region new
            Datagridview_Addor_reduce(qtyTable_dav26_1, dataGridView26, qtyTable_dav26, bindingSource28);
            #endregion
        }

        private void textBox48_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox48_txchange()
        {
            if (textBox48.Text.Length < 1 || dataGridView26.RowCount <= 0)
                return;
            #region 缓存处理
            cache_seepage_data();

            #endregion
            int tx28 = Convert.ToInt32(textBox48.Text);

            var qtyTable_dav27_1 = new DataTable();
            yizhishuiweidianshu(qtyTable_dav27_1);

            for (int j = 0; j < tx28; j++)
            {
                // qtyTable_dav26_1.Rows.Add(qtyTable_dav26_1.NewRow());
                qtyTable_dav27_1.Rows[j][0] = j + 1;
            }
            #region new
            Datagridview_Addor_reduce(qtyTable_dav27_1, dataGridView27, qtyTable_dav27, bindingSource29);
            #endregion
        }

        private void textBox51_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox51_txchange()
        {
            if (textBox51.Text.Length < 1 || dataGridView28.RowCount <= 0)
                return;
            #region 缓存处理
            cache_seepage_data();

            #endregion
            int tx28 = Convert.ToInt32(textBox51.Text);

            var qtyTable_dav28_1 = new DataTable();
            butoushuidiangeshu(qtyTable_dav28_1);

            for (int j = 0; j < tx28; j++)
            {
                // qtyTable_dav26_1.Rows.Add(qtyTable_dav26_1.NewRow());
                //  qtyTable_dav28_1.Rows[j][0] = j + 1;
            }
            #region new
            //   Datagridview_Addor_reduce(qtyTable_dav28_1, dataGridView28, qtyTable_dav28, bindingSource30);
            #endregion
        }

        private void textBox54_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox54_txchange()
        {
            if (textBox54.Text.Length < 1 || dataGridView29.RowCount <= 0)
                return;
            #region 缓存处理
            cache_seepage_data();

            #endregion
        }

        private void NewfrmProductMain_Resize(object sender, EventArgs e)
        {
            //AdjustSubformSize1();

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Control _Control = (Control)sender;
            ShowScrollBar(_Control.Handle, 4, 0);
        }

        private void panel5_Scroll(object sender, ScrollEventArgs e)
        {
            this.panel5.VerticalScroll.Value = e.NewValue;
            panel5.Invalidate();//刷新panel
        }

        private void panel5_ControlAdded(object sender, ControlEventArgs e)
        {
            this.panel5.VerticalScroll.Enabled = true;
            this.panel5.VerticalScroll.Visible = true;
            this.panel5.Scroll += panel5_Scroll;

        }

        private void textBox17_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
            {
                textBox17_txchange();

            }
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
            {
                textBox19_txchange();
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
            {
                textBox13_txchange();
            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox15_txchange();
        }

        private void textBox16_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox16_txchange();
        }

        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox20_txchange();
        }

        private void textBox25_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox25_txchange();
        }

        private void textBox28_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
            {
                textBox28_txchange();
                //textBox27_KeyPress(null, e);

            }
        }

        private void textBox27_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
            {
           
                textBox27_txchange();
                //textBox28_txchange();
                int tx28 = Convert.ToInt32(textBox28.Text);

                var qtyTable_dav16_1 = new DataTable();
                fengdanyuanjiedainbian(qtyTable_dav16_1);

                for (int j = 0; j < tx28; j++)
                {
                    qtyTable_dav16_1.Rows.Add(qtyTable_dav16_1.NewRow());
                    qtyTable_dav16_1.Rows[j][0] = j + 1;
                }

                #region new
                Datagridview_Addor_reduce(qtyTable_dav16_1, dataGridView17, qtyTable_dav16, bindingSource18);
                #endregion
            }
        }

        private void textBox26_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox26_txchange();
        }

        private void textBox29_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox29_txchange();
        }

        private void textBox30_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox30_txchange();
        }

        private void textBox33_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox33_txchange();
        }

        private void textBox35_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox35_txchange();
        }

        private void textBox40_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox40_txchange();

        }

        private void textBox39_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox39_txchange();
        }

        private void textBox45_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox45_txchange();
        }

        private void textBox48_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox48_txchange();

        }

        private void textBox51_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox51_txchange();
        }

        private void textBox54_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox54_txchange();
        }

        private void textBox22_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
                textBox22_txchange();
        }
        private void textBox22_txchange()
        {

            //this.bindingSource11.DataSource = qtyTable_dav9;
            //this.dataGridView11.DataSource = this.bindingSource11;

            //this.bindingSource12.DataSource = qtyTable_dav10;
            //this.dataGridView12.DataSource = this.bindingSource12;


            if (textBox22.Text.Length < 1 || dataGridView11.RowCount <= 0)
                return;
            int tx20 = Convert.ToInt32(textBox22.Text);

            #region 缓存处理
            iscache = true;//是缓存
            isallsave = 1;//批量保存标志
            allsave_index = 6;//第几个页
            toolStripButton1_Click(null, EventArgs.Empty);//保存到缓存文件

            string sp_txt = "";
            if (iscache == false)
                nowfile = Alist.Find(v => v.Contains("strength_data.sap"));
            else
                nowfile = cacheAlist.Find(v => v.Contains("strength_data.sap"));

            string[] fileText = File.ReadAllLines(nowfile);

            sp_txt = Read_strength_data(sp_txt, fileText);

            #endregion

            var qtyTable_dav9_1 = new DataTable();
            feixianxing_qiangduxishu(qtyTable_dav9_1);
            for (int j = 0; j < tx20; j++)
            {
                qtyTable_dav9_1.Rows.Add(qtyTable_dav9_1.NewRow());
                qtyTable_dav9_1.Rows[j][0] = j + 1;
            }

            #region 强度参数
            if (dataGridView11.RowCount > 0)
            {
                if (dataGridView11.RowCount > qtyTable_dav9_1.Rows.Count)
                {
                    int rowcout = dataGridView11.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav9_1.Rows.Count; i++)
                        dataGridView11.Rows.RemoveAt(dataGridView11.Rows.Count - 1);
                }
                else if (dataGridView11.RowCount < qtyTable_dav9_1.Rows.Count)
                {
                    int davcount = dataGridView11.RowCount;
                    for (int i = 0; i < qtyTable_dav9_1.Rows.Count - davcount; i++)
                    {
                        qtyTable_dav9.Rows.Add(qtyTable_dav9.NewRow());
                        qtyTable_dav9.Rows[qtyTable_dav9.Rows.Count - 1][0] = davcount + 1 + i;

                    }
                    this.bindingSource11.DataSource = qtyTable_dav9;
                    this.dataGridView11.DataSource = this.bindingSource11;

                }
            }
            else
            {
                this.bindingSource11.DataSource = qtyTable_dav9_1;
                this.dataGridView11.DataSource = this.bindingSource11;

            }
            #endregion
            #region 损伤与软化系数
            var qtyTable_dav10_1 = new DataTable();
            shunshangyuruanhuaxishu(qtyTable_dav10_1);
            for (int j = 0; j < tx20; j++)
            {
                qtyTable_dav10_1.Rows.Add(qtyTable_dav10_1.NewRow());
                qtyTable_dav10_1.Rows[j][0] = j + 1;
            }
            if (dataGridView12.RowCount > 0)
            {
                if (dataGridView12.RowCount > qtyTable_dav10_1.Rows.Count)
                {
                    int rowcout = dataGridView12.RowCount;

                    for (int i = 0; i < rowcout - qtyTable_dav10_1.Rows.Count; i++)
                        dataGridView12.Rows.RemoveAt(dataGridView12.Rows.Count - 1);
                }
                else if (dataGridView12.RowCount < qtyTable_dav10_1.Rows.Count)
                {
                    int davcount = dataGridView12.RowCount;
                    for (int i = 0; i < qtyTable_dav10_1.Rows.Count - davcount; i++)
                    {
                        qtyTable_dav10.Rows.Add(qtyTable_dav10.NewRow());
                        qtyTable_dav10.Rows[qtyTable_dav10.Rows.Count - 1][0] = davcount + 1 + i;

                    }
                    this.bindingSource12.DataSource = qtyTable_dav10;
                    this.dataGridView12.DataSource = this.bindingSource12;

                }
            }
            else
            {
                this.bindingSource12.DataSource = qtyTable_dav10_1;
                this.dataGridView12.DataSource = this.bindingSource12;

            }
            #endregion

            clearCache();

        }

        private void textBox23_KeyPress(object sender, KeyPressEventArgs e)
        {


            if (e.KeyChar == 13)//按下回车
                textBox23_txchange();


        }

        private void dataGridView13_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)//按下回车
            {


            }
        }

        private void dataGridView13_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {

                int dataGridView13_cloumncount = 0;
                for (int i = 0; i < dataGridView13.RowCount; i++)
                {
                    if (dataGridView13.Rows[i].Cells["计算次数"].EditedFormattedValue != null && dataGridView13.Rows[i].Cells["计算次数"].EditedFormattedValue != "")
                    {
                        int newjisuancishu = Convert.ToInt32(dataGridView13.Rows[i].Cells["计算次数"].EditedFormattedValue.ToString());
                        if (newjisuancishu > dataGridView13_cloumncount)
                            dataGridView13_cloumncount = Convert.ToInt32(dataGridView13.Rows[i].Cells["计算次数"].EditedFormattedValue.ToString());
                    }
                }



                #region 强度参数
                var qtyTable_dav11_1 = new DataTable();
                jiaozhucixu(qtyTable_dav11_1);
                int icount1 = dataGridView13_cloumncount;

                Adddav11cloumn(icount1, qtyTable_dav11_1);

       
                if (dataGridView13.RowCount > 0)
                {
                    if (dataGridView13.ColumnCount > qtyTable_dav11_1.Columns.Count)
                    {
                        int rowcout = dataGridView13.ColumnCount;

                        for (int i = 0; i < rowcout - qtyTable_dav11_1.Columns.Count; i++)
                        {
                            //dataGridView13.Columns.RemoveAt(dataGridView13.Columns.Count - 1);
                            qtyTable_dav11.Columns.RemoveAt(qtyTable_dav11.Columns.Count- 1);
                        }
                    }
                    else if (dataGridView13.ColumnCount < qtyTable_dav11_1.Columns.Count)
                    {
                        int davcount = dataGridView13.ColumnCount-3;

                        for (int i = 0; i < qtyTable_dav11_1.Columns.Count - davcount-3; i++)
                        {
                            int nx = qtyTable_dav11.Columns.Count;

                            int clou = davcount + i;
                            bool ishave = false;

                            foreach (System.Data.DataColumn k in qtyTable_dav11.Columns)
                            {
                                string columnName = k.ColumnName;

                                if (clou.ToString() == columnName)
                                    ishave = true;

                            }
                            if (ishave == false)
                                qtyTable_dav11.Columns.Add("△t" + clou.ToString(), System.Type.GetType("System.String"));//0


                        }
                        this.bindingSource13.DataSource = qtyTable_dav11;
                        this.dataGridView13.DataSource = this.bindingSource13;
                    }
                }
                else
                {

                    this.bindingSource13.DataSource = qtyTable_dav11;
                    this.dataGridView13.DataSource = this.bindingSource13;

                }
                this.bindingSource13.DataSource = qtyTable_dav11;
                this.dataGridView13.DataSource = this.bindingSource13;
                #endregion
            }
        }

        private void dataGridView23_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >=  1)
            {


                textBox39_txchange();
            
            
            }
        }


    }
}
