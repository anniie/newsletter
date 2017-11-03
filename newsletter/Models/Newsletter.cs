using newsletter.Architecture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace newsletter.Models
{
    public class Newsletter
    {
        public int TemplateId { get; set; }
        public int HeaderId { get; set; }
        public int FooterId { get; set; }
        public string TemplateTitle { get; set; }
        [AllowHtml]
        public string TemplateContent { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public List<Newsletter> GetAll()
        {
            List<Newsletter> lst = new List<Newsletter>();
            Connection conn = new Connection();

            Dictionary<string, string> selectData = new Dictionary<string, string>();
            selectData.Add("spName", "SP_GetAllData");
            selectData.Add("@tablename", "tbl_Newsletter");

            DataSet ds = conn.getData(selectData);

            foreach (DataRow rd in ds.Tables[0].Rows)
            {
                lst.Add(new Newsletter
                {
                    HeaderId = Convert.ToInt32(rd["HeaderId"]),
                    FooterId = Convert.ToInt32(rd["FooterId"]),
                    TemplateId = Convert.ToInt32(rd["TemplateId"]),
                    TemplateTitle = rd["TemplateTitle"].ToString(),
                    Status = rd["Status"].ToString() == "True" ? "Enable" : "Disable"
                });
            }

            return lst;
        }
        public bool AddNewsletter(Newsletter obj)
        {
            bool result = false;

            Connection conn = new Connection();

            try
            {
                Dictionary<string, string> inputData = new Dictionary<string, string>();
                inputData.Add("spName", "SP_AddNewsletter");
                inputData.Add("@HeaderId", obj.HeaderId.ToString());
                inputData.Add("@FooterId", obj.FooterId.ToString());
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

        public bool UpdateHeader(Newsletter obj)
        {
            bool result = false;

            Connection conn = new Connection();

            try
            {
                Dictionary<string, string> inputData = new Dictionary<string, string>();
                inputData.Add("spName", "SP_UpdateHeaderTemplate");
                inputData.Add("@TemplateId", obj.TemplateId.ToString());
                inputData.Add("@HeaderId", obj.HeaderId.ToString());
                inputData.Add("@FooterId", obj.FooterId.ToString());
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
    }
}