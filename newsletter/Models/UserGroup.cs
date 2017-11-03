using newsletter.Architecture;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace newsletter.Models
{
    public class UserGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string EmailIds { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        public bool AddUserGroup(UserGroup obj)
        {
            bool result = false;

            Connection conn = new Connection();

            try
            {
                Dictionary<string, string> inputData = new Dictionary<string, string>();
                inputData.Add("spName", "SP_AddNewsletterUserGroup");
                inputData.Add("@GroupName", obj.GroupName);
                inputData.Add("@EmailIds", obj.EmailIds);
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

        public bool UpdateHeader(UserGroup obj)
        {
            bool result = false;

            Connection conn = new Connection();

            try
            {
                Dictionary<string, string> inputData = new Dictionary<string, string>();
                inputData.Add("spName", "SP_UpdateNewsletterUserGroup");
                inputData.Add("@GroupId", obj.GroupId.ToString());
                inputData.Add("@GroupName", obj.GroupName);
                inputData.Add("@EmailIds", obj.EmailIds);
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