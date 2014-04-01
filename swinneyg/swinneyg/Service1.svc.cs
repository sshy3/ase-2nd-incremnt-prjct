using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;


namespace swinneyg
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        [WebInvoke (Method="GET", ResponseFormat=WebMessageFormat.Json,UriTemplate="checkadmin/{id},{pwd}")]
        public string checkadmin(string id, string pwd)
        {
            String tid;
            String tpwd;
            SqlConnection conn= new SqlConnection(ConfigurationManager.ConnectionStrings["dbstrings"].ConnectionString);
            conn.Open();
            SqlCommand cmd=new SqlCommand("Select * from admincheck where adminid='"+id+"' AND adminpwd='"+pwd+"' ",conn);
            SqlDataReader reader=cmd.ExecuteReader();
           
           
                tid =reader["adminid"].ToString();
                tpwd = reader["adminpwd"].ToString();
                if (tid == null && tpwd == null)
                { return null; }
                else return tid; 
             }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
