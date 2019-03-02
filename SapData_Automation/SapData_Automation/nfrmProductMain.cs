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

namespace SapData_Automation
{
    public partial class nfrmProductMain : DockContent
    {
        DateTime startAt;
        DateTime endAt;
        List<clsProductinfo> Productinfolist_Server;
        int rowcount;
        string txfind;
        private SortableBindingList<clsProductinfo> sortableOrderList;
        List<int> changeindex;
        private List<string> Alist = new List<string>();
        private Hashtable dataGridChanges = null;
        private string nowfile;
        DataGridView clickdav;
        string folderpath;
        List<string> crlist;
        public nfrmProductMain(string user)
        {
            InitializeComponent();
            this.dataGridChanges = new Hashtable();
            changeindex = new List<int>();
            this.WindowState = FormWindowState.Maximized;
            //foreach (Control gbox in groupBox7.Controls)
            //{
            //    if (gbox is VScrollBar) continue;
            //    gbox.Tag = panel2.Location.Y;
            //}

            //dataGridView8.Height = 1162;
      
            //tableLayoutPanel4.Height = 1600;
            //panel1.Height = 1600;
            dataGridView6.Height = 800;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            新建ToolStripMenuItem_Click(null, EventArgs.Empty);

            //var form = new frmaddProcuct("");

            //if (form.ShowDialog() == DialogResult.OK)
            //{

            //}
            toolStripButton2_Click(null, EventArgs.Empty);

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

                #region control.sap

                if (s == 1)
                {

                    //工况数

                    wtx = textBox1.Text;
                    //计算量1
                    if (radioButton1.Checked == true)
                        wtx += "\r\n" + "1";
                    else
                        wtx += "\r\n" + "0";
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
                    wtx += "\r\n" + textBox2.Text;
                    //温度梯度
                    wtx += "\r\n" + textBox3.Text;
                    //位移约束
                    wtx += "\r\n" + textBox4.Text;
                    //最大开闭次数:
                    wtx += "\r\n" + textBox7.Text;
                    //方程迭代误差
                    wtx += "\r\n" + textBox6.Text;
                    //初始条件读入
                    wtx += "\r\n" + textBox5.Text;
                    //非线性迭代误差
                    wtx += "\r\n" + textBox10.Text;
                    //最大非线性迭代次数
                    wtx += "\r\n" + textBox9.Text;

                    //位移清0步
                    wtx += "\r\n" + textBox12.Text;
                    //惯性阻尼系数
                    wtx += "\r\n" + textBox11.Text;
                    //接续计算
                    wtx += "\r\n" + textBox14.Text;

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw.Flush();
                    sw.Close();

                    MessageBox.Show("更新完成，请查看！");

                }
                #endregion


                #region  temp_para.sap
                else if (s == 3)
                {
                    //表面散热系数总数

                    wtx = textBox13.Text;

                    //水管总数:
                    wtx += " " + textBox15.Text;

                    //冷却期数:
                    wtx += " " + textBox16.Text;

                    //热学参数
                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView2, sw);
                    sw = wxdav(nowfile, this.dataGridView3, sw);
                    sw = wxdav(nowfile, this.dataGridView4, sw);
                    sw = wxdav(nowfile, this.dataGridView5, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");
                }
                #endregion

                #region Els_para.sap

                else if (s == 2)
                {
                    wtx = textBox17.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView6, sw);
                    sw = wxdav(nowfile, this.dataGridView7, sw);
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

                    //氧化镁
                    if (radioButton12.Checked == true)
                        wtx = "" + "0";
                    else if (radioButton12.Checked == true)
                        wtx = "" + "1";
                    wtx += "\r\n";
                    sw.WriteLine(wtx);


                    sw = wxdav(nowfile, this.dataGridView9, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");

                }

                #endregion

                #region 填 placement_time_of_element.sap


                else if (s == 4)
                {
                    wtx = textBox20.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView10, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");

                }

                #endregion
                #region 非线性参数 strength_data.sap

                else if (s == 6)
                {
                    wtx = textBox21.Text;
                    wtx += " " + textBox22.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView11, sw);
                    sw = wxdav(nowfile, this.dataGridView12, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");

                }

                #endregion


                #region 浇筑次序 sup_step.sap

                else if (s == 7)
                {
                    wtx = textBox23.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_sup_step(nowfile, this.dataGridView13, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");

                }

                #endregion


                #region 时步条件定义 Temp_bdy_3.sap

                else if (s == 8)
                {
                    wtx = textBox24.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView14, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");

                }

                #endregion

                #region 输出位移点 point_disp_output.sap


                else if (s == 9)
                {
                    wtx = textBox25.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView15, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");

                }

                #endregion


                #region  接缝单元数据 Joint_mesh.sap

                else if (s == 10)
                {
                    //缝单元总数


                    wtx = textBox28.Text;

                    //缝材料总数

                    wtx += " " + textBox27.Text + "\r\n";

                    //刚度系数
                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView19, sw);
                    //强度系数
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView18, sw);
                    //缝单元节点编
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView17, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");
                }
                #endregion

                #region 灌浆数据 grouting_step.sap

                else if (s == 11)
                {
                    wtx = textBox26.Text;
                    wtx += "\r\n";

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav(nowfile, this.dataGridView16, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");

                }

                #endregion

                #region  给定节点温度 Temp_fix.sap


                else if (s == 12)
                {
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
                    MessageBox.Show("更新完成，请查看！");
                }
                #endregion

                #region  库水河水温度 Temp_water.sap

                else if (s == 13)
                {
                    //初次蓄水日期


                    wtx = textBox32.Text;

                    //蓄水结束日期

                    wtx += " " + textBox31.Text;

                    //库水温数据行（水深）数

                    wtx += "\r\n" + textBox33.Text + "\r\n";

                    //库水温信息

                    StreamWriter sw = new StreamWriter(nowfile);
                    sw.WriteLine(wtx);
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView24, sw);


                    //下游水温类型:
                    wtx = textBox36.Text;

                    //泄水孔高程:

                    wtx += " " + textBox37.Text;

                    //下游水温表数据行数:

                    wtx += "\r\n" + textBox35.Text + "\r\n";

                    sw.WriteLine(wtx);
                    sw = wxdav_Joint_mesh(nowfile, this.dataGridView22, sw);
                    sw = wxdav_Temp_water(nowfile, this.dataGridView21, sw);

                    sw.Flush();
                    sw.Close();
                    MessageBox.Show("更新完成，请查看！");
                }
                #endregion


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

                for (int k = 0; k < dav.Columns.Count; k++)
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

                File.Create(folderpath + "\\" + crlist[i]);
                //StreamWriter sw = new StreamWriter(folderpath + "\\" + crlist[i]);
                //sw.WriteLine("");
                //sw.Flush();
                //sw.Close();

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
                toolStripButton2_Click(null, EventArgs.Empty);

                openfile(folderpath);
                MessageBox.Show("读取完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void openfile(string folderpath)
        {
            clsAllnew BusinessHelp = new clsAllnew();
            Alist = new List<string>();

            Alist = BusinessHelp.GetBy_CategoryReportFileName(folderpath);

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

            Gettab1();
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

                #region 计算控制文件  control.sap

                if (Alist[i].Contains("control.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    // string[] fileText1 = System.Text.RegularExpressions.Regex.Split(UserResult[0].salse_code, " ");

                    string wtx = "";
                    if (fileText.Length > 0)
                    {
                        //工况数
                        string[] fileTextG = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        textBox1.Text = fileTextG[0];

                        //计算量1
                        if (fileText.Length > 1)
                        {
                            string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[1], " ");

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
                        string[] fileTextQ = splittx0(fileText[2]);

                        textBox2.Text = fileTextQ[0];


                        //温度梯度
                        fileTextQ = splittx0(fileText[3]);
                        textBox3.Text = fileTextQ[0];

                        //位移约束
                        fileTextQ = splittx0(fileText[4]);
                        textBox4.Text = fileTextQ[0];

                        //最大开闭次数:
                        fileTextQ = splittx0(fileText[5]);
                        textBox7.Text = fileTextQ[0];

                        //方程迭代误差
                        fileTextQ = splittx0(fileText[6]);
                        textBox6.Text = fileTextQ[0];

                        //初始条件读入
                        fileTextQ = splittx0(fileText[7]);
                        textBox5.Text = fileTextQ[0];

                        //非线性迭代误差
                        fileTextQ = splittx0(fileText[8]);
                        textBox10.Text = fileTextQ[0];

                        //最大非线性迭代次数
                        fileTextQ = splittx0(fileText[9]);
                        textBox9.Text = fileTextQ[0];


                        //位移清0步
                        fileTextQ = splittx0(fileText[10]);
                        textBox12.Text = fileTextQ[0];

                        //惯性阻尼系数
                        if (fileText.Length > 11)
                        {
                            fileTextQ = splittx0(fileText[11]);
                            textBox11.Text = fileTextQ[0];
                        }
                        //接续计算
                        if (fileText.Length > 12)
                        {
                            fileTextQ = splittx0(fileText[12]);
                            textBox14.Text = fileTextQ[0];
                        }

                    }
                }
                #endregion

                #region  热学参数 temp_para.sap
                else if (Alist[i].Contains("temp_para.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //表面散热系数总数
                        if (fileText1.Length > 0)
                            textBox13.Text = fileText1[0];

                        //水管总数:
                        if (fileText1.Length > 1)
                            textBox15.Text = fileText1[1];

                        //冷却期数:
                        if (fileText1.Length > 2)
                            textBox16.Text = fileText1[2];


                    }
                    //热学参数
                    var qtyTable_dav2 = new DataTable();
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

                    int ongo = 0;
                    for (int j = 1; j <= fileText.Length; j++)
                    {
                        ongo = j;
                        if (fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            break;
                        }

                        qtyTable_dav2.Rows.Add(qtyTable_dav2.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            qtyTable_dav2.Rows[j - 1][jj] = fileText1[jj];


                        }


                    }
                    //表面散热系数
                    var qtyTable_dav3 = new DataTable();
                    qtyTable_dav3.Columns.Add("βw", System.Type.GetType("System.String"));//0
                    if (textBox13.Text.Length > 0)
                    {
                        int icount = Convert.ToInt32(textBox13.Text);
                        for (int i3 = 1; i3 <= icount; i3++)
                        {
                            qtyTable_dav3.Columns.Add("β" + i3, System.Type.GetType("System.String"));//0

                        }
                    }
                    //
                    int ongo1 = ongo + 1;
                    // ongo1 = 3;
                    int rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (fileText[j].Contains("\t\t\t") || fileText[j] == "")
                        {
                            break;
                        }

                        qtyTable_dav3.Rows.Add(qtyTable_dav3.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            if (jj < qtyTable_dav3.Columns.Count)
                                qtyTable_dav3.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }
                    //水管定义

                    var qtyTable_dav4 = new DataTable();
                    qtyTable_dav4.Columns.Add("水管号", System.Type.GetType("System.String"));//0
                    qtyTable_dav4.Columns.Add("冷却直径", System.Type.GetType("System.String"));//1
                    qtyTable_dav4.Columns.Add("管长", System.Type.GetType("System.String"));//2
                    qtyTable_dav4.Columns.Add("qmax", System.Type.GetType("System.String"));//3
                    qtyTable_dav4.Columns.Add("水热容量", System.Type.GetType("System.String"));//4
                    qtyTable_dav4.Columns.Add("管材λ", System.Type.GetType("System.String"));//5
                    qtyTable_dav4.Columns.Add("外径", System.Type.GetType("System.String"));//6
                    qtyTable_dav4.Columns.Add("内径", System.Type.GetType("System.String"));//7 
                    qtyTable_dav4.Columns.Add("材质", System.Type.GetType("System.String"));//8 
                    ongo1 = ongo + 1;
                    rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            break;
                        }

                        qtyTable_dav4.Rows.Add(qtyTable_dav4.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            if (jj < qtyTable_dav4.Columns.Count)
                                qtyTable_dav4.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }

                    //通水参数

                    var qtyTable_dav5 = new DataTable();
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

                    ongo1 = ongo + 1;
                    rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            break;
                        }

                        qtyTable_dav5.Rows.Add(qtyTable_dav5.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            qtyTable_dav5.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }

                    this.bindingSource2.DataSource = qtyTable_dav2;
                    this.dataGridView2.DataSource = this.bindingSource2;

                    this.bindingSource3.DataSource = qtyTable_dav3;
                    this.dataGridView3.DataSource = this.bindingSource3;

                    this.bindingSource4.DataSource = qtyTable_dav4;
                    this.dataGridView4.DataSource = this.bindingSource4;

                    this.bindingSource5.DataSource = qtyTable_dav5;
                    this.dataGridView5.DataSource = this.bindingSource5;


                }
                #endregion

                #region 基本材料参数 els_para.sap
                else if (Alist[i].Contains("els_para.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    int ongo = 0;

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //材料种数:
                        if (fileText1.Length > 0)
                            textBox17.Text = fileText1[0];
                    }
                    //基本力学参数
                    var qtyTable_dav5 = new DataTable();
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

                    int ongo1 = ongo + 1;
                    int rowindex = 0;
                    int isgo = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            isgo++;
                            if (isgo > 1)
                                break;
                            else
                                continue;

                        }
                        qtyTable_dav5.Rows.Add(qtyTable_dav5.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            qtyTable_dav5.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }
                    //徐变参数
                    var qtyTable_dav6 = new DataTable();
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

                    ongo1 = ongo + 1;
                    rowindex = 0;
                    isgo = 0;

                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            //isgo++;
                            //if (isgo > 1)
                            break;
                            //else
                            //    continue;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;
                        qtyTable_dav6.Rows.Add(qtyTable_dav6.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            if (jj < 12)
                                qtyTable_dav6.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }
                    //荷载
                    ongo1 = ongo + 1;
                    rowindex = 0;
                    isgo = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            //isgo++;
                            //if (isgo >1)
                            break;
                            //else
                            //    continue;
                        }
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        //if (fileText1.Length > 0 && fileText1[0] == "1")
                        //    radioButton1.Checked = true;
                        //else if (fileText1.Length > 1 && fileText1[0] == "0")
                        //    radioButton1.Checked = false;
                        //计算量2
                        if (fileText1.Length >= 1 && fileText1[0] == "0")
                            radioButton8.Checked = true;
                        if (fileText1.Length >= 2 && fileText1[1] == "1")
                            radioButton9.Checked = true;
                        if (fileText1.Length >= 3 && fileText1[2] == "2")
                            radioButton10.Checked = true;
                        if (fileText1.Length >= 4 && fileText1[3] == "4")
                            radioButton11.Checked = true;
                        ////渗透力
                        if (fileText1.Length > 5)
                            this.textBox18.Text = fileText1[4];
                        //自生体积变形定义点数
                        if (fileText1.Length >= 6)
                            this.textBox19.Text = fileText1[5];
                        break;

                    }
                    //自生体积变形定义点数
                    ongo1 = ongo + 1;
                    rowindex = 0;
                    isgo = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            isgo++;
                            if (isgo > 1)
                                break;
                            else
                                continue;
                        }
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        ////自生体积变形定义点数
                        if (fileText1.Length >= 1)
                            this.textBox19.Text = fileText1[0];

                    }
                    //自生体积变形
                    var qtyTable8 = new DataTable();
                    qtyTable8.Columns.Add("材料号\\龄期", System.Type.GetType("System.String"));//0

                    if (textBox19.Text == "")
                        textBox19.Text = "0";
                    int icount = Convert.ToInt32(textBox19.Text);
                    for (int ip = 1; ip <= icount; ip++)
                    {
                        qtyTable8.Columns.Add("" + ip, System.Type.GetType("System.String"));//0

                    }
                    ongo1 = ongo + 1;
                    rowindex = 0;
                    isgo = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || (fileText[j].Contains("\t\t") || fileText[j] == ""))
                        {
                            //isgo++;
                            //if (isgo > 1)
                            break;
                            //else
                            //    continue;
                        }

                        qtyTable8.Rows.Add(qtyTable8.NewRow());

                        if (fileText.Length > j)
                        {
                            string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), "\t");

                            for (int jj = 0; jj < fileText1.Length - 1; jj++)
                            {
                                if (jj < qtyTable8.Columns.Count)
                                    qtyTable8.Rows[rowindex][jj] = fileText1[jj];
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

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            //isgo++;
                            //if (isgo > 1)
                            break;
                            //else
                            //    continue;
                        }
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        if (fileText1.Length > 0 && fileText1[0] == "1")
                            radioButton12.Checked = true;
                        else if (fileText1.Length > 0 && fileText1[0] == "0")
                            radioButton12.Checked = false;
                    }


                    //氧化镁
                    var qtyTable_dav7 = new DataTable();
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

                    ongo1 = ongo + 1;
                    rowindex = 0;
                    isgo = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
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
                                if (isgo > 1)
                                    break;
                                else
                                    continue;
                            }
                        }

                        qtyTable_dav7.Rows.Add(qtyTable_dav7.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            qtyTable_dav7.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }
                    this.bindingSource6.DataSource = qtyTable_dav5;
                    this.dataGridView6.DataSource = this.bindingSource6;

                    this.bindingSource8.DataSource = qtyTable_dav6;
                    this.dataGridView7.DataSource = this.bindingSource8;

                    this.bindingSource9.DataSource = qtyTable_dav7;
                    this.dataGridView9.DataSource = this.bindingSource9;
                }
                #endregion

                #region 挖除与回填 placement_time_of_element.sap


                else if (Alist[i].Contains("placement_time_of_element.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    int ongo = 0;

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //挖除与回填单元总数::
                        if (fileText1.Length > 0)
                            textBox20.Text = fileText1[0];
                    }

                    var qtyTable_dav8 = new DataTable();
                    qtyTable_dav8.Columns.Add("单元号", System.Type.GetType("System.String"));//0
                    qtyTable_dav8.Columns.Add("挖除序号", System.Type.GetType("System.String"));//1
                    qtyTable_dav8.Columns.Add("回填序号", System.Type.GetType("System.String"));//2
                    qtyTable_dav8.Columns.Add("回填材料号", System.Type.GetType("System.String"));//3

                    int ongo1 = ongo + 1;
                    int rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav8.Rows.Add(qtyTable_dav8.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj < 4)
                                qtyTable_dav8.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }


                    this.bindingSource10.DataSource = qtyTable_dav8;
                    this.dataGridView10.DataSource = this.bindingSource10;
                }

                #endregion


                #region 非线性参数 strength_data.sap
                else if (Alist[i].Contains("strength_data.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    int ongo = 0;

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //分析类型::
                        if (fileText1.Length > 0)
                            textBox21.Text = fileText1[0];
                        //材料参数总数:::
                        if (fileText1.Length > 1)
                            textBox22.Text = fileText1[1];

                    }

                    var qtyTable_dav9 = new DataTable();
                    qtyTable_dav9.Columns.Add("材料号", System.Type.GetType("System.String"));//0
                    qtyTable_dav9.Columns.Add("凝聚力", System.Type.GetType("System.String"));//1
                    qtyTable_dav9.Columns.Add("摩擦角", System.Type.GetType("System.String"));//2
                    qtyTable_dav9.Columns.Add("抗拉强度", System.Type.GetType("System.String"));//3
                    qtyTable_dav9.Columns.Add("抗压强度", System.Type.GetType("System.String"));//3

                    qtyTable_dav9.Columns.Add("准则号", System.Type.GetType("System.String"));//3
                    qtyTable_dav9.Columns.Add("r1", System.Type.GetType("System.String"));//3
                    qtyTable_dav9.Columns.Add("r2", System.Type.GetType("System.String"));//3
                    qtyTable_dav9.Columns.Add("r3", System.Type.GetType("System.String"));//3
                    qtyTable_dav9.Columns.Add("r4", System.Type.GetType("System.String"));//3

                    int ongo1 = ongo + 1;
                    int rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav9.Rows.Add(qtyTable_dav9.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj < 10)
                                qtyTable_dav9.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }

                    var qtyTable_dav10 = new DataTable();
                    qtyTable_dav10.Columns.Add("材料号", System.Type.GetType("System.String"));//0
                    qtyTable_dav10.Columns.Add("α", System.Type.GetType("System.String"));//1
                    qtyTable_dav10.Columns.Add("N", System.Type.GetType("System.String"));//2
                    qtyTable_dav10.Columns.Add("拉极限应变", System.Type.GetType("System.String"));//3
                    qtyTable_dav10.Columns.Add("剪极限应变", System.Type.GetType("System.String"));//3
                    qtyTable_dav10.Columns.Add("刚度软化", System.Type.GetType("System.String"));//3
                    qtyTable_dav10.Columns.Add("强度软化", System.Type.GetType("System.String"));//3

                    ongo1 = ongo + 1;
                    rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav10.Rows.Add(qtyTable_dav10.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj < 7)
                                qtyTable_dav10.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }


                    this.bindingSource11.DataSource = qtyTable_dav9;
                    this.dataGridView11.DataSource = this.bindingSource11;

                    this.bindingSource12.DataSource = qtyTable_dav10;

                    this.dataGridView12.DataSource = this.bindingSource12;
                }

                #endregion


                #region 浇筑次序 sup_step.sap
                else if (Alist[i].Contains("sup_step.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    int ongo = 0;

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //挖除与回填单元总数::
                        if (fileText1.Length > 0)
                            textBox23.Text = fileText1[0];
                    }

                    var qtyTable_dav11 = new DataTable();
                    qtyTable_dav11.Columns.Add("浇筑序号", System.Type.GetType("System.String"));//0
                    qtyTable_dav11.Columns.Add("单元数", System.Type.GetType("System.String"));//1
                    qtyTable_dav11.Columns.Add("节点数", System.Type.GetType("System.String"));//2
                    qtyTable_dav11.Columns.Add("计算次数", System.Type.GetType("System.String"));//3


                    if (textBox23.Text.Length >= 1)
                    {
                        int icount = Convert.ToInt32(textBox23.Text);
                        for (int i11 = 1; i11 <= icount; i11++)
                        {
                            qtyTable_dav11.Columns.Add("△t" + i11, System.Type.GetType("System.String"));//0

                        }

                        int ongo1 = ongo + 1;
                        int rowindex = 0;
                        int isadd = 0;
                        int cloindex = 0;
                        string comtxt = "";

                        for (int j = ongo1; j <= fileText.Length; j++)
                        {
                            ongo = j;

                            if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                            {
                                break;
                            }
                            if (fileText[j] == "" && j == 1)
                                continue;
                            if (isadd == 0)
                            {
                                qtyTable_dav11.Rows.Add(qtyTable_dav11.NewRow());

                                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                                if (fileText1.Length < 2)
                                    fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Replace("   ", " ").Replace("  ", " "), " ");

                                for (int jj = 0; jj < fileText1.Length; jj++)
                                {
                                    cloindex = jj;
                                    if (rowindex < qtyTable_dav11.Rows.Count)
                                        qtyTable_dav11.Rows[rowindex][jj] = fileText1[jj];
                                }
                                isadd++;
                            }
                            else
                            {
                                string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                                if (fileText1.Length < 2)
                                    fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Replace("   ", " ").Replace("  ", " ").Trim(), " ");

                                for (int jj = 0; jj < fileText1.Length; jj++)
                                {
                                    if (qtyTable_dav11.Columns.Count >= cloindex + 1)
                                    {
                                        if (rowindex - 1 < qtyTable_dav11.Rows.Count)
                                        {
                                            qtyTable_dav11.Rows[rowindex - 1][cloindex] = fileText1[jj];
                                            cloindex++;
                                        }
                                    }
                                }
                                cloindex = 0;
                                isadd = 0;

                            }
                            rowindex++;

                        }
                    }

                    this.bindingSource13.DataSource = qtyTable_dav11;
                    this.dataGridView13.DataSource = this.bindingSource13;
                }


                #endregion


                #region 时步条件定义 Temp_bdy_3.sap


                else if (Alist[i].Contains("temp_bdy_3.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    int ongo = 0;

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //挖除与回填单元总数::
                        if (fileText1.Length > 0)
                            textBox24.Text = fileText1[0];
                    }

                    var qtyTable_dav12 = new DataTable();
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


                    int ongo1 = ongo + 1;
                    int rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav12.Rows.Add(qtyTable_dav12.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj < 27)
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
                    int ongo = 0;

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //挖除与回填单元总数::
                        if (fileText1.Length > 0)
                            textBox25.Text = fileText1[0];
                    }

                    var qtyTable_dav13 = new DataTable();

                    qtyTable_dav13.Columns.Add("序号", System.Type.GetType("System.String"));//0
                    qtyTable_dav13.Columns.Add("节点号", System.Type.GetType("System.String"));//1

                    int ongo1 = ongo + 1;
                    int rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav13.Rows.Add(qtyTable_dav13.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj < 2)
                                qtyTable_dav13.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }
                    this.bindingSource15.DataSource = qtyTable_dav13;
                    this.dataGridView15.DataSource = this.bindingSource15;
                }
                #endregion


                #region  接缝单元数据 Joint_mesh.sap

                else if (Alist[i].Contains("joint_mesh.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //缝单元总数

                        if (fileText1.Length > 0)
                            textBox28.Text = fileText1[0];

                        //缝材料总数

                        if (fileText1.Length > 1)
                            textBox27.Text = fileText1[1];

                    }
                    //刚度系数
                    var qtyTable_dav14 = new DataTable();
                    qtyTable_dav14.Columns.Add("材料号", System.Type.GetType("System.String"));//0
                    qtyTable_dav14.Columns.Add("法向刚度", System.Type.GetType("System.String"));//1
                    qtyTable_dav14.Columns.Add("切向刚度", System.Type.GetType("System.String"));//2
                    qtyTable_dav14.Columns.Add("法向残余", System.Type.GetType("System.String"));//3
                    qtyTable_dav14.Columns.Add("切向残余", System.Type.GetType("System.String"));//4
                    qtyTable_dav14.Columns.Add("渗透系数", System.Type.GetType("System.String"));//5

                    int ongo = 0;
                    for (int j = 1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav14.Rows.Add(qtyTable_dav14.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length <= 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            if (jj < 5)
                                qtyTable_dav14.Rows[j - 2][jj + 1] = fileText1[jj];
                        }
                        qtyTable_dav14.Rows[j - 2][0] = j - 1;


                    }
                    //强度系数

                    var qtyTable_dav15 = new DataTable();
                    qtyTable_dav15.Columns.Add("材料号", System.Type.GetType("System.String"));//0
                    qtyTable_dav15.Columns.Add("Re", System.Type.GetType("System.String"));//1
                    qtyTable_dav15.Columns.Add("c", System.Type.GetType("System.String"));//2
                    qtyTable_dav15.Columns.Add("f", System.Type.GetType("System.String"));//3
                    qtyTable_dav15.Columns.Add("fg", System.Type.GetType("System.String"));//4
                    qtyTable_dav15.Columns.Add("cl", System.Type.GetType("System.String"));//5
                    qtyTable_dav15.Columns.Add("cc", System.Type.GetType("System.String"));//4
                    qtyTable_dav15.Columns.Add("cf", System.Type.GetType("System.String"));//5
                    qtyTable_dav15.Columns.Add("pre", System.Type.GetType("System.String"));//4

                    //
                    int ongo1 = ongo + 1;
                    // ongo1 = 3;
                    int rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav15.Rows.Add(qtyTable_dav15.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length <= 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj < qtyTable_dav15.Columns.Count - 1)
                                qtyTable_dav15.Rows[rowindex][jj + 1] = fileText1[jj];
                        }
                        qtyTable_dav15.Rows[rowindex][0] = rowindex + 1;
                        rowindex++;

                    }
                    //缝单元节点编

                    var qtyTable_dav16 = new DataTable();
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

                    ongo1 = ongo + 1;
                    rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav16.Rows.Add(qtyTable_dav16.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length <= 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            if (jj < 10)
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
                }
                #endregion


                #region 灌浆数据 grouting_step.sap
                else if (Alist[i].Contains("grouting_step.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    int ongo = 0;

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //挖除与回填单元总数::
                        if (fileText1.Length > 0)
                            textBox26.Text = fileText1[0];
                    }

                    var qtyTable_dav18 = new DataTable();

                    qtyTable_dav18.Columns.Add("单元号", System.Type.GetType("System.String"));//0
                    qtyTable_dav18.Columns.Add("浇筑号", System.Type.GetType("System.String"));//1
                    qtyTable_dav18.Columns.Add("计算步号", System.Type.GetType("System.String"));//1

                    int ongo1 = ongo + 1;
                    int rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav18.Rows.Add(qtyTable_dav18.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj < 3)
                                qtyTable_dav18.Rows[rowindex][jj] = fileText1[jj];
                        }
                        rowindex++;

                    }
                    this.bindingSource19.DataSource = qtyTable_dav18;
                    this.dataGridView16.DataSource = this.bindingSource19;
                }


                #endregion

                #region 给定节点温度 Temp_fix.sap

                else if (Alist[i].Contains("temp_fix.sap"))
                {
                    string[] fileText = File.ReadAllLines(Alist[i]);
                    int ongo = 0;

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        //挖除与回填单元总数::
                        if (fileText1.Length > 0)
                            textBox29.Text = fileText1[0];
                        if (fileText1.Length > 1)
                            textBox30.Text = fileText1[1];
                    }
                    var qtyTable_dav19 = new DataTable();
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
                        for (int j = ongo1; j <= fileText.Length; j++)
                        {
                            ongo = j;

                            if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || (fileText[j].Replace("  ", "").Trim() == "" && j != 1))
                            {
                                break;
                            }
                            if (fileText[j] == "" && j == 1)
                                continue;

                            // qtyTable_dav19.Rows.Add(qtyTable_dav19.NewRow());

                            string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                            if (fileText1.Length < 2)
                                fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("   ", " ").Replace("  ", " "), " ");

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
                }


                #endregion


                #region  库水河水温度 Temp_water.sap

                else if (Alist[i].Contains("temp_water.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), "\t");

                        //初次蓄水日期

                        if (fileText1.Length > 0)
                            textBox32.Text = fileText1[0];

                        //蓄水结束日期

                        if (fileText1.Length > 1)
                            textBox31.Text = fileText1[1];

                        //库水温数据行（水深）数

                        if (fileText[1].Length > 0)
                            textBox33.Text = fileText[1];


                    }
                    //库水温信息

                    var qtyTable_dav20 = new DataTable();
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



                    if (textBox33.Text.Length > 0)
                    {
                        if (!textBox33.Text.Contains(" "))
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
                    for (int j = 2; j <= fileText.Length; j++)
                    {
                        ongo = j;
                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            if (j > 4)
                                break;
                            else
                                continue;

                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        //  qtyTable_dav20.Rows.Add(qtyTable_dav20.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj + 1 < qtyTable_dav20.Columns.Count && rowindex < qtyTable_dav20.Rows.Count)
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

                        if (fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            break;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");
                        //下游水温类型:
                        if (fileText1.Length > 0)
                            textBox36.Text = fileText1[0];
                        //泄水孔高程:
                        if (fileText1.Length > 0)
                            textBox37.Text = fileText1[1];
                        //下游水温表数据行数:
                        if (fileText.Length > j + 1)
                            textBox35.Text = fileText[j + 1];
                        break;

                        rowindex++;

                    }


                    var qtyTable_dav21 = new DataTable();
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
                        int icount = Convert.ToInt32(textBox35.Text);
                        for (int iw = 1; iw <= icount; iw++)
                        {
                            //qtyTable_dav21.Rows.Add("" + iw, System.Type.GetType("System.String"));//0
                            qtyTable_dav21.Rows.Add(qtyTable_dav21.NewRow());
                        }
                    }


                    ongo1 = ongo + 2;
                    rowindex = 0;
                    int blankindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            blankindex++;
                            if (blankindex > 1)
                                break;
                            else
                                continue;

                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        //  qtyTable_dav21.Rows.Add(qtyTable_dav21.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length; jj++)
                        {
                            if (jj + 1 < qtyTable_dav21.Columns.Count && rowindex < qtyTable_dav21.Rows.Count)
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


                    ongo1 = ongo + 1;
                    rowindex = 0;

                    int cloumnindex = 1;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (j >= fileText.Length || fileText[j].Contains("\t\t\t\t") || fileText[j] == "")
                        {
                            continue;
                        }
                        if (fileText[j] == "" && j == 1)
                            continue;

                        qtyTable_dav22.Rows.Add(qtyTable_dav22.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");
                        if (fileText1.Length < 2)
                            fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j].Trim().Replace("    ", " ").Replace("   ", " ").Replace("  ", " "), " ");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
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



                }
                #endregion


            }
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
                toolStripDropDownButton6_Click(null, EventArgs.Empty);

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

            if (textBox13.Text != "")
            {

                var qtyTable = new DataTable();
                qtyTable.Columns.Add("βw", System.Type.GetType("System.String"));//0


                int icount = Convert.ToInt32(textBox13.Text);
                for (int i = 1; i <= icount; i++)
                {
                    qtyTable.Columns.Add("β" + i, System.Type.GetType("System.String"));//0

                }

                this.bindingSource3.DataSource = qtyTable;
                this.dataGridView3.DataSource = this.bindingSource3;
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

            if (File.Exists(DesktopPath + "\\ultraedit.exe"))

                System.Diagnostics.Process.Start("ultraedit.exe", nowfile);





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
            var qtyTable_dav11 = new DataTable();
            qtyTable_dav11.Columns.Add("浇筑序号", System.Type.GetType("System.String"));//0
            qtyTable_dav11.Columns.Add("单元数", System.Type.GetType("System.String"));//1
            qtyTable_dav11.Columns.Add("节点数", System.Type.GetType("System.String"));//2
            qtyTable_dav11.Columns.Add("计算次数", System.Type.GetType("System.String"));//3


            if (textBox23.Text.Length >= 1)
            {
                int icount = Convert.ToInt32(textBox23.Text);
                for (int i11 = 1; i11 <= icount; i11++)
                {
                    qtyTable_dav11.Columns.Add("△t" + i11, System.Type.GetType("System.String"));//0

                }
            }
            this.bindingSource13.DataSource = qtyTable_dav11;
            this.dataGridView13.DataSource = this.bindingSource13;
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
            if (textBox26.Text != "")
            {

                var qtyTable_dav18 = new DataTable();
                qtyTable_dav18.Columns.Add("单元号", System.Type.GetType("System.String"));//0
                qtyTable_dav18.Columns.Add("浇筑号", System.Type.GetType("System.String"));//1
                qtyTable_dav18.Columns.Add("计算步号", System.Type.GetType("System.String"));//1


                int icount = Convert.ToInt32(textBox26.Text);
                for (int i = 1; i <= icount; i++)
                {
                    //qtyTable_dav18.Rows.Add("" + i, System.Type.GetType("System.String"));//0
                    qtyTable_dav18.Rows.Add(qtyTable_dav18.NewRow());

                }

                this.bindingSource19.DataSource = qtyTable_dav18;
                this.dataGridView16.DataSource = this.bindingSource19;
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
            if (textBox30.Text != "")
            {

                showdav20();
            }
        }

        private void showdav20()
        {
            var qtyTable_dav19 = new DataTable();
            qtyTable_dav19.Columns.Add("序号", System.Type.GetType("System.String"));//0
            qtyTable_dav19.Columns.Add("节点号", System.Type.GetType("System.String"));//1
            if (textBox30.Text.Length > 0 && textBox29.Text.Length > 0)
            {
                int icount = Convert.ToInt32(textBox30.Text);
                for (int i = 1; i <= icount; i++)
                {
                    qtyTable_dav19.Columns.Add("T" + i, System.Type.GetType("System.String"));//0

                }
                int icount1 = Convert.ToInt32(textBox29.Text);
                for (int i = 1; i <= icount1; i++)
                {
                    //qtyTable_dav19.Rows.Add("" + i, System.Type.GetType("System.String"));//0
                    qtyTable_dav19.Rows.Add(qtyTable_dav19.NewRow());
                }
            }
            this.bindingSource20.DataSource = qtyTable_dav19;
            this.dataGridView20.DataSource = this.bindingSource20;
        }

        private void textBox29_TextChanged(object sender, EventArgs e)
        {
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

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            var qtyTable_dav21 = new DataTable();
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
                int icount = Convert.ToInt32(textBox35.Text);
                for (int i = 1; i <= icount; i++)
                {
                    //qtyTable_dav21.Rows.Add("" + i, System.Type.GetType("System.String"));//0
                    qtyTable_dav21.Rows.Add(qtyTable_dav21.NewRow());
                }
            }
            this.bindingSource23.DataSource = qtyTable_dav21;
            this.dataGridView22.DataSource = this.bindingSource23;

        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {

            var qtyTable_dav20 = new DataTable();
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

            if (textBox33.Text.Length > 0)
            {
                int icount = Convert.ToInt32(textBox33.Text);
                for (int i = 1; i <= icount; i++)
                {
                    //qtyTable_dav20.Rows.Add("" + i, System.Type.GetType("System.String"));//0
                    qtyTable_dav20.Rows.Add(qtyTable_dav20.NewRow());
                }
            }
            this.bindingSource22.DataSource = qtyTable_dav20;
            this.dataGridView24.DataSource = this.bindingSource22;


        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

            if (File.Exists(folderpath + "\\saptis.exe"))

                System.Diagnostics.Process.Start("saptis.exe", folderpath);




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

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            //foreach (Control gbox in groupBox7.Controls)
            //{
            //    if (gbox is VScrollBar) continue;
            //    gbox.Location = new Point(gbox.Location.X, (int)gbox.Tag - e.NewValue);
            //}
        }
    }
}
