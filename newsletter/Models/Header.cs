using newsletter.Architecture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace newsletter.Models
{
    public class Header
    {
        public int TemplateId { get; set; }
        public string TemplateTitle { get; set; }
        [AllowHtml]
        public string TemplateContent { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public List<Header> GetAll()
        {
            List<Header> lst = new List<Header>();
            Connection conn = new Connection();

            Dictionary<string, string> selectData = new Dictionary<string, string>();
            selectData.Add("spName", "SP_GetAllData");
            selectData.Add("@tablename", "tbl_HeaderTemplate");

            DataSet ds = conn.getData(selectData);

            foreach (DataRow rd in ds.Tables[0].Rows)
            {
                lst.Add(new Header
                {
                    TemplateId = Convert.ToInt32(rd["TemplateId"]),
                    TemplateTitle = rd["TemplateTitle"].ToString(),
                    Status = rd["Status"].ToString() == "True" ? "Enable" : "Disable"
                });
            }

            return lst;
        }
        public static IEnumerable<clsDropDown> GetHeaderList()
        {
            DataSet ds = new DataSet();

            Dictionary<string, string> selectData = new Dictionary<string, string>();
            selectData.Add("spName", "SP_GetAllData");
            selectData.Add("@tablename", "tbl_HeaderTemplate");

            Connection conn = new Connection();
            ds = conn.getData(selectData);

            var query = from _data in ds.Tables[0].AsEnumerable()
                        select _data;
            List<clsDropDown> Headers = (from q in query
                                        select new clsDropDown
                                        {
                                            value = q[0].ToString(),
                                            text = q[1].ToString()
                                        }).ToList();

            clsDropDown select = new clsDropDown();
            select.text = "Select Header";
            select.value = "0";

            Headers.Insert(0, select);

            return Headers;
        }
        public bool AddHeader(Header obj)
        {
            bool result = false;

            Connection conn = new Connection();

            try
            {
                Dictionary<string, string> inputData = new Dictionary<string, string>();
                inputData.Add("spName", "SP_AddHeaderTemplate");
                inputData.Add("@TemplateTitle", obj.TemplateTitle);
                inputData.Add("@TemplateContent", obj.TemplateContent);
                inputData.Add("@Status", obj.Status);
                inputData.Add("@CreatedOn", obj.CreatedOn.ToString());
                inputData.Add("@ModifiedOn", obj.ModifiedOn.ToString());
                
                if (conn.insertUpdateData(inputData) > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        public bool UpdateHeader(Header obj)
        {
            bool result = false;

            Connection conn = new Connection();

            try
            {
                Dictionary<string, string> inputData = new Dictionary<string, string>();
                inputData.Add("spName", "SP_UpdateHeaderTemplate");
                inputData.Add("@TemplateId", obj.TemplateId.ToString());
                inputData.Add("@TemplateTitle", obj.TemplateTitle);
                inputData.Add("@TemplateContent", obj.TemplateContent);
                inputData.Add("@Status", obj.Status);
                inputData.Add("@ModifiedOn", obj.ModifiedOn.ToString());

                if (conn.insertUpdateData(inputData) > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
        public class clsDropDown
        {
            public string value { get; set; }
            public string text { get; set; }
        }

    }
}