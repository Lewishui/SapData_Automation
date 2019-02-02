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
    public partial class frmProductMain : DockContent
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


        public frmProductMain(string user)
        {
            InitializeComponent();
            this.dataGridChanges = new Hashtable();
            changeindex = new List<int>();
            this.WindowState = FormWindowState.Maximized;

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

            //var form = new frmaddProcuct("");

            //if (form.ShowDialog() == DialogResult.OK)
            //{

            //}

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(" 确认删除这条信息 , 继续 ?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

            }
            else
                return;

            var oids = GetOrderIdsBySelectedGridCell();
            for (int j = 0; j < oids.Count; j++)
            {
                var filtered = Productinfolist_Server.FindAll(s => s.Product_id == oids[j]);
                clsAllnew BusinessHelp = new clsAllnew();
                //批量删 
                int istu = BusinessHelp.deleteProduct(filtered[0].Product_id.ToString());

                for (int i = 0; i < filtered.Count; i++)
                {
                    //单个删除

                    Productinfolist_Server.Remove(Productinfolist_Server.Where(o => o.Product_id == filtered[i].Product_id).Single());
                    if (istu != 1)
                    {
                        MessageBox.Show("删除失败，请查看" + filtered[i].Product_address + filtered[i].Product_name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            BindDataGridView();

        }
        private List<long> GetOrderIdsBySelectedGridCell()
        {

            List<long> order_ids = new List<long>();
            var rows = GetSelectedRowsBySelectedCells(dataGridView1);
            foreach (DataGridViewRow row in rows)
            {
                var Diningorder = row.DataBoundItem as clsProductinfo;
                order_ids.Add((long)Diningorder.Product_id);
            }

            return order_ids;
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
            this.pbStatus.Value = 0;
            this.toolStripLabel1.Text = "";

            startAt = this.stockOutDateTimePicker.Value.AddDays(0).Date;
            endAt = this.stockInDateTimePicker1.Value.AddDays(0).Date;
            txfind = this.textBox8.Text;

            string strSelect = "select * from JNOrder_product where Input_Date>='" + startAt.ToString("yyyy/MM/dd") + "'" + "and " + "Input_Date<='" + endAt.ToString("yyyy/MM/dd") + "'";
            // strSelect = "select * from JNOrder_customer where Input_Date BETWEEN #" + startAt + "# AND #" + endAt + "#";//成功


            if (txfind.Length > 0)
            {
                strSelect += " And Product_name like '%" + txfind + "%'";
                if (txfind == "所有")
                    strSelect = "select * from JNOrder_product";

            }

            strSelect += " order by Product_id desc";

            clsAllnew BusinessHelp = new clsAllnew();
            Productinfolist_Server = new List<clsProductinfo>();
            Productinfolist_Server = BusinessHelp.findProductr(strSelect);
            this.BindDataGridView();
        }

        private void BindDataGridView()
        {
            if (Productinfolist_Server != null)
            {
                sortableOrderList = new SortableBindingList<clsProductinfo>(Productinfolist_Server);
                dataGridView1.AutoGenerateColumns = false;

                dataGridView1.DataSource = sortableOrderList;
                this.toolStripLabel1.Text = "条数：" + sortableOrderList.Count.ToString();
            }
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
                    if (radioButton2.Checked == true)
                        wtx += " " + "0";
                    if (radioButton3.Checked == true)
                        wtx += " " + "1";
                    if (radioButton4.Checked == true)
                        wtx += " " + "2";
                    //计算量3
                    if (radioButton7.Checked == true)
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


        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridViewRow dgrSingle = dataGridView1.Rows[e.RowIndex];
            string cell_key = e.RowIndex.ToString() + "_" + e.ColumnIndex.ToString();

            if (!dataGridChanges.ContainsKey(cell_key))
            {
                dataGridChanges[cell_key] = dgrSingle.Cells[e.ColumnIndex].Value;
            }
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            string cell_key = e.RowIndex.ToString() + "_" + e.ColumnIndex.ToString();
            var new_cell_value = row.Cells[e.ColumnIndex].Value;
            var original_cell_value = dataGridChanges[cell_key];
            if (new_cell_value == null && original_cell_value == null)
            {
                dataGridChanges.Remove(cell_key + "_changed");
            }
            else if ((new_cell_value == null && original_cell_value != null) || (new_cell_value != null && original_cell_value == null) || !new_cell_value.Equals(original_cell_value))
            {
                dataGridChanges[cell_key + "_changed"] = new_cell_value;
            }
            else
            {
                dataGridChanges.Remove(cell_key + "_changed");
            }

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            bool success = dailysaveList(worker, e);
        }
        private bool dailysaveList(BackgroundWorker worker, DoWorkEventArgs e)
        {
            WorkerArgument arg = e.Argument as WorkerArgument;
            clsAllnew BusinessHelp = new clsAllnew();
            bool success = true;
            try
            {

                int rowCount = changeindex.Count;
                arg.OrderCount = rowCount;
                int j = 1;
                int progress = 0;
                #region MyRegion
                for (int ik = 0; ik < changeindex.Count; ik++)
                {
                    j = ik;

                    arg.CurrentIndex = j + 1;
                    progress = Convert.ToInt16(((j + 1) * 1.0 / rowCount) * 100);

                    int i = changeindex[ik];
                    var row = dataGridView1.Rows[i];

                    var model = row.DataBoundItem as clsProductinfo;

                    clsProductinfo item = new clsProductinfo();

                    item.Product_no = Convert.ToString(dataGridView1.Rows[i].Cells["Product_no"].EditedFormattedValue.ToString());

                    item.Product_name = Convert.ToString(dataGridView1.Rows[i].Cells["Product_name"].EditedFormattedValue.ToString());

                    item.Product_salse = Convert.ToString(dataGridView1.Rows[i].Cells["Product_salse"].EditedFormattedValue.ToString());

                    item.Product_address = Convert.ToString(dataGridView1.Rows[i].Cells["Product_address"].EditedFormattedValue.ToString());


                    item.Input_Date = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd"));
                    item.Product_id = model.Product_id;

                #endregion

                    #region MyRegion
                    var startAt = this.stockOutDateTimePicker.Value.AddDays(0).Date;
                    string conditions = "";

                    #region  构造查询条件
                    if (item.Product_no != null)
                    {
                        conditions += " Product_no ='" + item.Product_no + "'";
                    }
                    if (item.Product_name != null)
                    {
                        conditions += " ,Product_name ='" + item.Product_name + "'";
                    }
                    if (item.Product_salse != null)
                    {
                        conditions += " ,Product_salse ='" + item.Product_salse + "'";
                    }
                    if (item.Product_address != null)
                    {
                        conditions += " ,Product_address ='" + item.Product_address + "'";
                    }

                    if (item.Input_Date != null)
                    {
                        conditions += " ,Input_Date ='" + item.Input_Date.ToString("yyyy/MM/dd") + "'";
                    }
                    conditions = "update JNOrder_product set  " + conditions + " where Product_id = " + item.Product_id + " ";

                    // conditions += " order by Id desc";
                    #endregion
                    #endregion

                    int isrun = BusinessHelp.updateProduct_Server(conditions);


                    if (arg.CurrentIndex % 5 == 0)
                    {
                        backgroundWorker2.ReportProgress(progress, arg);
                    }
                }
                backgroundWorker2.ReportProgress(100, arg);
                e.Result = string.Format("{0} 已保存 ！", changeindex.Count);

            }
            catch (Exception ex)
            {
                if (!e.Cancel)
                {

                    e.Result = ex.Message + "";
                }
                success = false;
            }

            return success;
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show(string.Format("It is cancelled!"));
            }
            else
            {
                toolStripLabel1.Text = "" + "(" + e.Result + ")" + "--数据已成功保存 可以继续编辑无需刷新";
                changeindex = new List<int>();

                dataGridView1.Enabled = true;
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
            if (this.dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Sorry , No Data Output !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".csv";
            saveFileDialog.Filter = "csv|*.csv";
            string strFileName = "商品 信息" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.FileName = strFileName;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                strFileName = saveFileDialog.FileName.ToString();
            }
            else
            {
                return;
            }
            FileStream fa = new FileStream(strFileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fa, Encoding.Unicode);
            string delimiter = "\t";
            string strHeader = "";
            for (int i = 0; i < this.dataGridView1.Columns.Count; i++)
            {
                strHeader += this.dataGridView1.Columns[i].HeaderText + delimiter;
            }
            sw.WriteLine(strHeader);

            //output rows data
            for (int j = 0; j < this.dataGridView1.Rows.Count; j++)
            {
                string strRowValue = "";

                for (int k = 0; k < this.dataGridView1.Columns.Count; k++)
                {
                    if (this.dataGridView1.Rows[j].Cells[k].Value != null)
                    {
                        strRowValue += this.dataGridView1.Rows[j].Cells[k].Value.ToString().Replace("\r\n", " ").Replace("\n", "") + delimiter;
                        if (this.dataGridView1.Rows[j].Cells[k].Value.ToString() == "LIP201507-35")
                        {

                        }

                    }
                    else
                    {
                        strRowValue += this.dataGridView1.Rows[j].Cells[k].Value + delimiter;
                    }
                }
                sw.WriteLine(strRowValue);
            }
            sw.Close();
            fa.Close();
            MessageBox.Show("Dear User, Down File  Successful ！", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            string folderpath = "";

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

            List<string> crlist = new List<string>();

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

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderpath = "";

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

            }
            else
                return;
            clsAllnew BusinessHelp = new clsAllnew();
            Alist = new List<string>();

            Alist = BusinessHelp.GetBy_CategoryReportFileName(folderpath);


            Gettab1();
            MessageBox.Show("读取完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                #region control.sap
                if (Alist[i].Contains("control.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);

                    // string[] fileText1 = System.Text.RegularExpressions.Regex.Split(UserResult[0].salse_code, " ");

                    string wtx = "";

                    //工况数
                    textBox1.Text = fileText[0];

                    //计算量1
                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[1], " ");

                        if (fileText1.Length > 0 && fileText1[0] == "1")
                            radioButton1.Checked = true;
                        else if (fileText1.Length > 1 && fileText1[0] == "0")
                            radioButton1.Checked = false;
                        //计算量2
                        if (fileText1.Length > 1 && fileText1[1] == "0")
                            radioButton2.Checked = true;
                        if (fileText1.Length > 1 && fileText1[1] == "1")
                            radioButton3.Checked = true;
                        if (fileText1.Length > 1 && fileText1[1] == "2")
                            radioButton4.Checked = true;

                        //计算量3
                        if (fileText1.Length > 2 && fileText1[2] == "0")
                            radioButton7.Checked = true;
                        if (fileText1.Length > 2 && fileText1[2] == "1")
                            radioButton6.Checked = true;
                        if (fileText1.Length > 2 && fileText1[2] == "2")
                            radioButton5.Checked = true;
                    }
                    //求解器
                    textBox2.Text = fileText[2];


                    //温度梯度
                    textBox3.Text = fileText[3];

                    //位移约束
                    textBox4.Text = fileText[4];

                    //最大开闭次数:
                    textBox7.Text = fileText[5];

                    //方程迭代误差
                    textBox6.Text = fileText[6];

                    //初始条件读入
                    textBox5.Text = fileText[7];

                    //非线性迭代误差
                    textBox10.Text = fileText[8];

                    //最大非线性迭代次数
                    textBox9.Text = fileText[9];


                    //位移清0步
                    textBox12.Text = fileText[10];

                    //惯性阻尼系数
                    textBox11.Text = fileText[11];

                    //接续计算
                    textBox14.Text = fileText[12];

                }
                #endregion

                #region temp_para.sap
                else if (Alist[i].Contains("temp_para.sap"))
                {

                    string[] fileText = File.ReadAllLines(Alist[i]);



                    if (fileText.Length > 1)
                    {
                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[0], " ");

                        //表面散热系数总数
                        if (fileText1.Length > 0)
                            textBox13.Text = fileText[0];

                        //水管总数:
                        if (fileText1.Length > 1)
                            textBox15.Text = fileText[1];

                        //冷却期数:
                        if (fileText1.Length > 2)
                            textBox15.Text = fileText[2];


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
                        if (fileText[j].Contains("\t\t\t\t"))
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
                    int icount = Convert.ToInt32(textBox13.Text);
                    for (int i3 = 1; i3 <= icount; i3++)
                    {
                        qtyTable_dav3.Columns.Add("β" + i3, System.Type.GetType("System.String"));//0

                    }

                    //
                    int ongo1 = ongo + 1;
                    // ongo1 = 3;
                    int rowindex = 0;
                    for (int j = ongo1; j <= fileText.Length; j++)
                    {
                        ongo = j;

                        if (fileText[j].Contains("\t\t\t\t"))
                        {
                            break;
                        }

                        qtyTable_dav3.Rows.Add(qtyTable_dav3.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
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

                        if (fileText[j].Contains("\t\t\t\t"))
                        {
                            break;
                        }

                        qtyTable_dav4.Rows.Add(qtyTable_dav4.NewRow());

                        string[] fileText1 = System.Text.RegularExpressions.Regex.Split(fileText[j], "\t");

                        for (int jj = 0; jj < fileText1.Length - 1; jj++)
                        {
                            qtyTable_dav3.Rows[rowindex][jj] = fileText1[jj];
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

                        if (fileText[j].Contains("\t\t\t\t"))
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
                    this.dataGridView3.DataSource = this.bindingSource2;

                    this.bindingSource4.DataSource = qtyTable_dav4;
                    this.dataGridView4.DataSource = this.bindingSource2;

                    this.bindingSource5.DataSource = qtyTable_dav5;
                    this.dataGridView5.DataSource = this.bindingSource2;


                }
                #endregion
                else if (Alist[i].Contains("els_para.sap"))
                {


                }



            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int s = this.tabControl1.SelectedIndex;
            if (s == 1 && (nowfile == null || nowfile == ""))
            {
                toolStripDropDownButton2_Click(null, EventArgs.Empty);
            }
            if (s == 3 && (nowfile == null || nowfile == ""))
            {
                toolStripDropDownButton4_Click(null, EventArgs.Empty);
            }

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {

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

                this.bindingSource2.DataSource = qtyTable;
                this.dataGridView3.DataSource = this.bindingSource2;


            }



        }
    }
}
