using newsletter.Architecture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace newsletter.Models
{
    public class Footer
    {
        public int TemplateId { get; set; }
        public string TemplateTitle { get; set; }
        [AllowHtml]
        public string TemplateContent { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public List<Footer> GetAll()
        {
            List<Footer> lst = new List<Footer>();
            Connection conn = new Connection();

            Dictionary<string, string> selectData = new Dictionary<string, string>();
            selectData.Add("spName", "SP_GetAllData");
            selectData.Add("@tablename", "tbl_FooterTemplate");

            DataSet ds = conn.getData(selectData);

            foreach (DataRow rd in ds.Tables[0].Rows)
            {
                lst.Add(new Footer
                {
                    TemplateId = Convert.ToInt32(rd["TemplateId"]),
                    TemplateTitle = rd["TemplateTitle"].ToString(),
                    Status = rd["Status"].ToString() == "True" ? "Enable" : "Disable"
                });
            }

            return lst;
        }
        public static IEnumerable<clsDropDown> GetFooterList()
        {
            DataSet ds = new DataSet();

            Dictionary<string, string> selectData = new Dictionary<string, string>();
            selectData.Add("spName", "SP_GetAllData");
            selectData.Add("@tablename", "tbl_FooterTemplate");

            Connection conn = new Connection();
            ds = conn.getData(selectData);

            var query = from _data in ds.Tables[0].AsEnumerable()
                        select _data;
            List<clsDropDown> Footers = (from q in query
                                         select new clsDropDown
                                         {
                                             value = q[0].ToString(),
                                             text = q[1].ToString()
                                         }).ToList();

            clsDropDown select = new clsDropDown();
            select.text = "Select Footer";
            select.value = "0";

            Footers.Insert(0, select);

            return Footers;
        }
        public bool AddFooter(Footer obj)
        {
            bool result = false;

            Connection conn = new Connection();

            try
            {
                Dictionary<string, string> inputData = new Dictionary<string, string>();
                inputData.Add("spName", "SP_AddFooterTemplate");
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
        public bool UpdateFooter(Footer obj)
        {
            bool result = false;

            Connection conn = new Connection();

            try
            {
                Dictionary<string, string> inputData = new Dictionary<string, string>();
                inputData.Add("spName", "SP_UpdateFooterTemplate");
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