using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Order.Common;
using Order.DB;
using System.Collections;

namespace Order.Buiness
{
    public class clsAllnew
    {
        string connectionString = "mongodb://127.0.0.1";
        string DB_NAME = "FA_shop_PT";
        #region print
        private List<Stream> m_streams;
        private int m_currentPageIndex;
        List<clsOrderinfo> FilterTIPResults;
        string orderprint;
        #endregion


        public clsAllnew()
        {
            

        }
        public void createUser_Server(List<clsuserinfo> AddMAPResult)
        {
            string sql = "insert into JNOrder_User(name,password,Createdate,Btype,denglushijian,jigoudaima,AdminIS) values ('" + AddMAPResult[0].name + "','" + AddMAPResult[0].password + "','" + AddMAPResult[0].Createdate + "','" + AddMAPResult[0].Btype + "','" + AddMAPResult[0].denglushijian + "','" + AddMAPResult[0].jigoudaima + "','" + AddMAPResult[0].AdminIS + "')";
            int isrun = MySqlHelper.ExecuteSql(sql);

            return;


            #region mongo

            //MongoServer server = MongoServer.Create(connectionString);
            //MongoDatabase db1 = server.GetDatabase(DB_NAME);
            //MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            //MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_User");

            ////  collection1.RemoveAll();
            //if (AddMAPResult == null)
            //{
            //    MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //foreach (clsuserinfo item in AddMAPResult)
            //{

            //    QueryDocument query = new QueryDocument("name", item.name);
            //    collection1.Remove(query);
            //    BsonDocument fruit_1 = new BsonDocument
            //{ 
            //{ "name", item.name },
            //{ "password", item.password },
            //{ "Createdate", DateTime.Now.ToString("yyyy/MM/dd/HH")}, 
            //{ "Btype", item.Btype} ,
            // { "denglushijian", item.denglushijian} ,
            //  { "jigoudaima", item.jigoudaima} ,
            //{ "AdminIS", item.AdminIS} 
            //};
            //    collection1.Insert(fruit_1);
            //} 
            #endregion
        }
        public void lock_Userpassword_Server(List<clsuserinfo> AddMAPResult)
        {
            string sql = "update JNOrder_User set Btype ='" + AddMAPResult[0].Btype.Trim() + "' where name ='" + AddMAPResult[0].name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql);

            return;
            #region monodb

            //MongoServer server = MongoServer.Create(connectionString);
            //MongoDatabase db1 = server.GetDatabase(DB_NAME);
            //MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            //MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_User");

            //if (AddMAPResult == null)
            //{
            //    MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //foreach (clsuserinfo item in AddMAPResult)
            //{
            //    QueryDocument query = new QueryDocument("name", item.name);
            //    var update = Update.Set("Btype", item.Btype.Trim());
            //    collection1.Update(query, update);
            //} 
            #endregion
        }
        public List<clsuserinfo> ReadUserlistfromServer()
        {
            string conditions = "select * from JNOrder_User";//成功
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(conditions);
            List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();

            while (reader.Read())
            {
                clsuserinfo item = new clsuserinfo();

                item.Order_id = reader.GetString(0);
                item.name = reader.GetString(1);
                item.password = reader.GetString(2);
                item.Createdate = reader.GetString(3);
                item.Btype = reader.GetString(4);
                item.denglushijian = reader.GetString(5);
                item.jigoudaima = reader.GetString(6);
                item.AdminIS = reader.GetString(7);



                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
            #region  mongodb  Read  database info server
            //try
            //{
            //    List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();

            //    MongoServer server = MongoServer.Create(connectionString);
            //    MongoDatabase db1 = server.GetDatabase(DB_NAME);
            //    MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            //    MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_User");

            //    foreach (BsonDocument emp in employees.FindAll())
            //    {
            //        clsuserinfo item = new clsuserinfo();

            //        #region 数据
            //        if (emp.Contains("_id"))
            //            item.Order_id = (emp["_id"].ToString());
            //        if (emp.Contains("name"))
            //            item.name = (emp["name"].AsString);
            //        if (emp.Contains("password"))
            //            item.password = (emp["password"].ToString());
            //        if (emp.Contains("Btype"))
            //            item.Btype = (emp["Btype"].AsString);
            //        if (emp.Contains("denglushijian"))
            //            item.denglushijian = (emp["denglushijian"].AsString);
            //        if (emp.Contains("Createdate"))
            //            item.Createdate = (emp["Createdate"].AsString);
            //        if (emp.Contains("AdminIS"))
            //            item.AdminIS = (emp["AdminIS"].AsString);

            //        if (emp.Contains("jigoudaima"))
            //            item.jigoudaima = (emp["jigoudaima"].AsString);

            //        #endregion
            //        ClaimReport_Server.Add(item);
            //    }
            //    return ClaimReport_Server;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("" + ex);
            //    return null;
            //    throw ex;
            //}
            #endregion
        }

        public List<clsuserinfo> findUser(string findtext)
        {
            string strSelect = "select * from JNOrder_User where name='" + findtext + "'";
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(strSelect);
            List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();

            while (reader.Read())
            {
                clsuserinfo item = new clsuserinfo();

                item.Order_id = reader.GetString(0);
                item.name = reader.GetString(1);
                item.password = reader.GetString(2);
                item.Createdate = reader.GetString(3);
                item.Btype = reader.GetString(4);
                item.denglushijian = reader.GetString(5);
                item.jigoudaima = reader.GetString(6);
                item.AdminIS = reader.GetString(7);



                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
            #region Read  database info server
            //try
            //{
            //    List<clsuserinfo> ClaimReport_Server = new List<clsuserinfo>();

            //    MongoServer server = MongoServer.Create(connectionString);
            //    MongoDatabase db1 = server.GetDatabase(DB_NAME);
            //    MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            //    MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_User");

            //    var query = new QueryDocument("name", findtext);

            //    foreach (BsonDocument emp in employees.Find(query))
            //    {
            //        clsuserinfo item = new clsuserinfo();

            //        #region 数据
            //        if (emp.Contains("_id"))
            //            item.Order_id = (emp["_id"].ToString());
            //        if (emp.Contains("name"))
            //            item.name = (emp["name"].AsString);
            //        if (emp.Contains("password"))
            //            item.password = (emp["password"].ToString());
            //        if (emp.Contains("Btype"))
            //            item.Btype = (emp["Btype"].AsString);
            //        if (emp.Contains("denglushijian"))
            //            item.denglushijian = (emp["denglushijian"].AsString);
            //        if (emp.Contains("Createdate"))
            //            item.Createdate = (emp["Createdate"].AsString);
            //        if (emp.Contains("AdminIS"))
            //            item.AdminIS = (emp["AdminIS"].AsString);

            //        if (emp.Contains("jigoudaima"))
            //            item.jigoudaima = (emp["jigoudaima"].AsString);
            //        #endregion
            //        ClaimReport_Server.Add(item);
            //    }
            //    return ClaimReport_Server;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("" + ex);
            //    return null;

            //    throw ex;
            //}
            #endregion
        }
        public void deleteUSER(string name)
        {
            string sql2 = "delete from JNOrder_User where  name='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return;
            #region  monodb
            //MongoServer server = MongoServer.Create(connectionString);
            //MongoDatabase db1 = server.GetDatabase(DB_NAME);
            //MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            //MongoCollection<BsonDocument> employees = db1.GetCollection<BsonDocument>("FA_shop_User");

            //if (name == null)
            //{
            //    MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //QueryDocument query = new QueryDocument("name", name);

            //collection1.Remove(query); 
            #endregion
        }
        public void changeUserpassword_Server(List<clsuserinfo> AddMAPResult)
        {
            string sql = "update JNOrder_User set password ='" + AddMAPResult[0].password.Trim() + "' where name ='" + AddMAPResult[0].name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql);

            return;
            #region mongodb

            //MongoServer server = MongoServer.Create(connectionString);
            //MongoDatabase db1 = server.GetDatabase(DB_NAME);
            //MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            //MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_User");

            //if (AddMAPResult == null)
            //{
            //    MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //foreach (clsuserinfo item in AddMAPResult)
            //{
            //    QueryDocument query = new QueryDocument("name", item.name);
            //    var update = Update.Set("password", item.password.Trim());
            //    collection1.Update(query, update);
            //} 
            #endregion
        }
        public void updateLoginTime_Server(List<clsuserinfo> AddMAPResult)
        {
            string sql = "update JNOrder_User set denglushijian ='" + AddMAPResult[0].denglushijian.Trim() + "' where name ='" + AddMAPResult[0].name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql);

            return;

            #region mongodb
            //MongoServer server = MongoServer.Create(connectionString);
            //MongoDatabase db1 = server.GetDatabase(DB_NAME);
            //MongoCollection collection1 = db1.GetCollection("FA_shop_User");
            //MongoCollection<BsonDocument> employees1 = db1.GetCollection<BsonDocument>("FA_shop_User");

            ////  collection1.RemoveAll();
            //if (AddMAPResult == null)
            //{
            //    MessageBox.Show("No Data  input Sever", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //foreach (clsuserinfo item in AddMAPResult)
            //{
            //    QueryDocument query = new QueryDocument("name", item.name);
            //    var update = Update.Set("denglushijian", item.denglushijian.Trim());
            //    collection1.Update(query, update);
            //} 
            #endregion
        }
        public int create_customer_Server(List<clscustomerinfo> AddMAPResult)
        {
            string sql = "insert into JNOrder_customer(customer_name,customer_adress,customer_shuihao,customer_bank,customer_account,customer_phone,Input_Date,customer_contact) values ('" + AddMAPResult[0].customer_name + "','" + AddMAPResult[0].customer_adress + "','" + AddMAPResult[0].customer_shuihao + "','" + AddMAPResult[0].customer_bank + "','" + AddMAPResult[0].customer_account + "','" + AddMAPResult[0].customer_phone + "','" + AddMAPResult[0].Input_Date.ToString("yyyy/MM/dd") + "','" + AddMAPResult[0].customer_contact + "')";
            int isrun = MySqlHelper.ExecuteSql(sql);

            return isrun;
        }
        public int deletecustomer(string name)
        {
            string sql2 = "delete from JNOrder_customer where  customer_id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }
        public List<clscustomerinfo> findcustomer(string findtext)
        {
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);
            List<clscustomerinfo> ClaimReport_Server = new List<clscustomerinfo>();

            while (reader.Read())
            {
                clscustomerinfo item = new clscustomerinfo();

                item.customer_id = reader.GetInt32(0);
                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.customer_name = reader.GetString(1);
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.customer_adress = reader.GetString(2);
                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.customer_shuihao = reader.GetString(3);
                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")
                    item.customer_bank = reader.GetString(4);
                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")
                    item.customer_account = reader.GetString(5);
                if (reader.GetValue(6) != null && Convert.ToString(reader.GetValue(6)) != "")
                    item.customer_phone = reader.GetString(6);
                if (reader.GetValue(7) != null && Convert.ToString(reader.GetValue(7)) != "")
                    item.customer_contact = reader.GetString(7);

                if (reader.GetValue(8) != null && Convert.ToString(reader.GetValue(8)) != "")
                    item.Input_Date = Convert.ToDateTime(reader.GetString(8));



                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
        }
        public int updatecustomer_Server(string findtext)
        {
            int isrun = MySqlHelper.ExecuteSql(findtext);

            return isrun;
        }
        public int create_Product_Server(List<clsProductinfo> AddMAPResult)
        {
            string sql = "insert into JNOrder_product(Product_no,Product_name,Product_salse,Product_address,Input_Date) values ('" + AddMAPResult[0].Product_no + "','" + AddMAPResult[0].Product_name + "','" + AddMAPResult[0].Product_salse + "','" + AddMAPResult[0].Product_address + "','" + AddMAPResult[0].Input_Date.ToString("yyyy/MM/dd") + "')";
            int isrun = MySqlHelper.ExecuteSql(sql);

            return isrun;
        }
        public List<clsProductinfo> findProductr(string findtext)
        {
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);
            List<clsProductinfo> ClaimReport_Server = new List<clsProductinfo>();

            while (reader.Read())
            {
                clsProductinfo item = new clsProductinfo();

                item.Product_id = reader.GetInt32(0);
                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")
                    item.Product_no = reader.GetString(1);
                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.Product_name = reader.GetString(2);
                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")
                    item.Product_salse = reader.GetString(3);
                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")
                    item.Product_address = reader.GetString(4);

                if (reader.GetString(5) != null && reader.GetString(5) != "")
                    item.Input_Date = Convert.ToDateTime(reader.GetString(5));



                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
        }
        public int deleteProduct(string name)
        {
            string sql2 = "delete from JNOrder_product where  Product_id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }
        public int updateProduct_Server(string findtext)
        {
            int isrun = MySqlHelper.ExecuteSql(findtext);

            return isrun;
        }
        public List<clsOrderinfo> findOrder(string findtext)
        {
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);
            List<clsOrderinfo> ClaimReport_Server = new List<clsOrderinfo>();

            while (reader.Read())
            {
                clsOrderinfo item = new clsOrderinfo();

                item.order_id = reader.GetInt32(0);
                if (reader.GetValue(1) != null && Convert.ToString(reader.GetValue(1)) != "")

                    item.customer_name = reader.GetString(1);

                if (reader.GetValue(2) != null && Convert.ToString(reader.GetValue(2)) != "")
                    item.dinghuoshijian = Convert.ToDateTime(reader.GetString(2));
                if (reader.GetValue(3) != null && Convert.ToString(reader.GetValue(3)) != "")

                    item.order_no = reader.GetString(3);
                if (reader.GetValue(4) != null && Convert.ToString(reader.GetValue(4)) != "")

                    item.Product_no = reader.GetString(4);
                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")

                    item.Product_name = reader.GetString(5);
                if (reader.GetValue(6) != null && Convert.ToString(reader.GetValue(6)) != "")

                    item.shuliang = reader.GetString(6);
                if (reader.GetValue(7) != null && Convert.ToString(reader.GetValue(7)) != "")

                    item.Product_salse = reader.GetString(7);
                if (reader.GetValue(8) != null && Convert.ToString(reader.GetValue(8)) != "")

                    item.jine = reader.GetString(8);


                if (reader.GetValue(9) != null && Convert.ToString(reader.GetValue(9)) != "")

                    item.yujijiaohuoshijian = Convert.ToDateTime(reader.GetString(9));



                //if (reader.GetString(10) != null && reader.GetString(10) != "")
                if (reader.GetValue(10) != null && Convert.ToString(reader.GetValue(10)) != "")

                    item.jianhuoshijian2 = Convert.ToDateTime(reader.GetString(10));
                if (reader.GetValue(11) != null && Convert.ToString(reader.GetValue(11)) != "")

                    item.dingdanguanliyuan = reader.GetString(11);
                if (reader.GetValue(12) != null && Convert.ToString(reader.GetValue(12)) != "")

                    item.kaipiao = reader.GetString(12);
                if (reader.GetValue(13) != null && Convert.ToString(reader.GetValue(13)) != "")

                    item.shifoujiaohuo = reader.GetString(13);


                if (reader.GetString(14) != null && reader.GetString(14) != "")
                    item.fukuanriqi = Convert.ToDateTime(reader.GetString(14));
                if (reader.GetValue(15) != null && Convert.ToString(reader.GetValue(15)) != "")

                    item.beizhu = reader.GetString(15);


                //if (reader.GetString(16) != null && reader.GetString(16) != "")
                if (reader.GetValue(16) != null && Convert.ToString(reader.GetValue(16)) != "")

                    item.Input_Date = Convert.ToDateTime(reader.GetString(16));



                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
        }
        public int deleteOrder(string name)
        {
            string sql2 = "delete from JNOrder_order where  order_id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }
        public void Run(List<clsOrderinfo> FilterOrderResults)
        {

            LocalReport report = new LocalReport();
            report.ReportPath = Application.StartupPath + "\\Report1.rdlc";

            report.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", FilterOrderResults));

            Export(report);
            m_currentPageIndex = 0;

            Print(orderprint, 0, 0);
        }
        public void Export(LocalReport report)
        {

            string deviceInfo =
"<DeviceInfo>" +
"  <OutputFormat>EMF</OutputFormat>" +
"  <PageWidth>8.27in</PageWidth>" +
"  <PageHeight>11.69in</PageHeight>" +
"  <MarginTop>0.0cm</MarginTop>" +
"  <MarginLeft>0.0cm</MarginLeft>" +
"  <MarginRight>0.0cm</MarginRight>" +
"  <MarginBottom>0.0cm</MarginBottom>" +
"</DeviceInfo>";

            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo, CreateStream,
               out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        private Stream CreateStream(string name, string fileNameExtension,

     Encoding encoding, string mimeType, bool willSeek)
        {

            //如果需要将报表输出的数据保存为文件，请使用FileStream对象。

            Stream stream = new MemoryStream();

            m_streams.Add(stream);

            return stream;

        }
        public void Print(string defaultPrinterName, int lenpage, int withpage)
        {

            m_currentPageIndex = 0;
            if (m_streams == null || m_streams.Count == 0)
                return;
            //声明PrintDocument对象用于数据的打印

            PrintDocument printDoc = new PrintDocument();

            //指定需要使用的打印机的名称，使用空字符串""来指定默认打印机

            if (defaultPrinterName == "" || defaultPrinterName == null)
                defaultPrinterName = printDoc.PrinterSettings.PrinterName;

            printDoc.PrinterSettings.PrinterName = defaultPrinterName;

            //判断指定的打印机是否可用

            if (!printDoc.PrinterSettings.IsValid)
            {
                MessageBox.Show("Can't find printer");
                return;
            }
            //声明PrintDocument对象的PrintPage事件，具体的打印操作需要在这个事件中处理。

            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);

            //执行打印操作，Print方法将触发PrintPage事件。
            printDoc.DefaultPageSettings.Landscape = true;
            //大小
            if (lenpage != 0)
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("Custom", lenpage, withpage);


            printDoc.Print();

        }
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            StringFormat SF = new StringFormat();
            SF.LineAlignment = StringAlignment.Center;
            SF.Alignment = StringAlignment.Center;
            float left = ev.PageSettings.Margins.Left;//打印区域的左边界
            float top = ev.PageSettings.Margins.Top;//打印区域的上边界
            float width = ev.PageSettings.PaperSize.Width - left - ev.PageSettings.Margins.Right;//计算出有效打印区域的宽度
            float height = ev.PageSettings.PaperSize.Height - top - ev.PageSettings.Margins.Bottom;//计算出有效打印区域的高度

            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        public List<clsLog_info> findLog(string findtext)
        {
            MySql.Data.MySqlClient.MySqlDataReader reader = MySqlHelper.ExecuteReader(findtext);
            List<clsLog_info> ClaimReport_Server = new List<clsLog_info>();

            while (reader.Read())
            {
                clsLog_info item = new clsLog_info();

                item.Log_id = reader.GetInt32(0);
                item.product_no = reader.GetString(1);

                item.indent = reader.GetString(2);
                item.indent_date = reader.GetString(3);

                item.end_user = reader.GetString(4);


                if (reader.GetValue(5) != null && Convert.ToString(reader.GetValue(5)) != "")

                    item.Input_Date = Convert.ToDateTime(reader.GetString(5));
                item.vendor = reader.GetString(6);
                if (reader.GetValue(7) != null && Convert.ToString(reader.GetValue(7)) != "")
                    item.daohuoshijian = reader.GetString(7);



                ClaimReport_Server.Add(item);

                //这里做数据处理....
            }
            return ClaimReport_Server;
        }
        public int updateLog_Server(string findtext)
        {
            int isrun = MySqlHelper.ExecuteSql(findtext);

            return isrun;
        }
        public int deletelog(string name)
        {
            string sql2 = "delete from JNOrder_log where  Log_id='" + name + "'";
            int isrun = MySqlHelper.ExecuteSql(sql2);

            return isrun;

        }

        public List<string> GetBy_CategoryReportFileName(string dirPath)
        {

            List<string> FileNameList = new List<string>();
            ArrayList list = new ArrayList();

            if (Directory.Exists(dirPath))
            {
                list.AddRange(Directory.GetFiles(dirPath));
            }
            if (list.Count > 0)
            {
                foreach (object item in list)
                {
                    if (!item.ToString().Contains("~$"))
                        //FileNameList.Add(item.ToString().Replace(dirPath + "\\", ""));
                    FileNameList.Add(item.ToString());
                }
            }

            return FileNameList;
        }
    }
}
