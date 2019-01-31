using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Order.DB
{
    public class clsuserinfo
    {
        public string Order_id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string Btype { get; set; }
        public string denglushijian { get; set; }
        public string Createdate { get; set; }
        public string AdminIS { get; set; }
        public string jigoudaima { get; set; }
    }
    public class clscustomerinfo
    {
        public int customer_id { get; set; }
        public string customer_name { get; set; }
        public string customer_adress { get; set; }
        public string customer_shuihao { get; set; }
        public string customer_bank { get; set; }
        public string customer_account { get; set; }
        public string customer_phone { get; set; }
        public string customer_contact { get; set; }
        public DateTime Input_Date { get; set; }
    }
    public class clsProductinfo
    {
        public int Product_id { get; set; }
        public string Product_no { get; set; }
        public string Product_name { get; set; }
        public string Product_salse { get; set; }
        public string Product_address { get; set; }

        public DateTime Input_Date { get; set; }
    }
    //客户 订货时间  订单号   产品型号  名称  数量   单价  金额  预计交货时间  交货时间2  订单管理员   开票  是否交货 付款日期    备注
    public class clsOrderinfo
    {
        public int order_id { get; set; }
        public string customer_name { get; set; }
        public DateTime dinghuoshijian { get; set; }
        public string order_no { get; set; }
        public string Product_no { get; set; }
        public string Product_name { get; set; }
        public string shuliang { get; set; }
        public string Product_salse { get; set; }
        public string jine { get; set; }
        public DateTime yujijiaohuoshijian { get; set; }
        public DateTime jianhuoshijian2 { get; set; }
        public string dingdanguanliyuan { get; set; }
        public string kaipiao { get; set; }
        public string shifoujiaohuo { get; set; }
        public DateTime fukuanriqi { get; set; }
        public string beizhu { get; set; }
        public DateTime Input_Date { get; set; }
        //新增的标记
        public string xinzeng { get; set; }
     
        //
        public string Message { get; set; }
      
    }
    //产品型号	订货数量	订货日期	使用单位

    public class clsLog_info
    {
        public int Log_id { get; set; }
        public string product_no { get; set; }
        public string indent { get; set; }
        public string indent_date { get; set; }
        public string end_user { get; set; }
        public string vendor { get; set; }
        public string daohuoshijian { get; set; }
   
        public DateTime Input_Date { get; set; }
        //新增的标记
        public string xinzeng { get; set; }
   

    }
}
