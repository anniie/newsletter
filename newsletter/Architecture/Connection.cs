using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Collections.Generic;

namespace newsletter.Architecture
{
    public class Connection
    {
        private SqlConnection conn = null;

        public DataSet getData(Dictionary<string, string> data)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = data["spName"];
                    data.Remove("spName");

                    foreach (string key in data.Keys)
                    {
                        cmd.Parameters.AddWithValue(key, data[key]);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
            }
            return ds;
        }

        public int insertUpdateData(Dictionary<string, string> data)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = data["spName"];
                    data.Remove("spName");

                    foreach (string key in data.Keys)
                    {
                        cmd.Parameters.AddWithValue(key, data[key]);
                    }

                    result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return result;
        }

        public int insertUpdateReturnData(Dictionary<string, string> data)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connString"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = data["spName"];
                    data.Remove("spName");

                    foreach (string key in data.Keys)
                    {
                        cmd.Parameters.AddWithValue(key, data[key]);
                    }

                    SqlParameter returnParameter = cmd.Parameters.Add("status", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.ReturnValue;

                    string id = cmd.ExecuteNonQuery().ToString();

                    result = (int)returnParameter.Value;
                    conn.Close();
                }
            }
            return result;
        }
    }
}