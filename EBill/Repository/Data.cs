using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EBill.Models;
using EBill.Repository;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace EBill.Repository
{
    public class Data : IData
    {

        public string ConntionString { get; set; }
        public Data()
        {
            ConntionString = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        }

        public void SaveBillDetails(BillDetail deatails)
        {
            SqlConnection con = new SqlConnection(ConntionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("spt_saveEBillDetails", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerName", deatails.CustomerName);
                cmd.Parameters.AddWithValue("@MobileNumber", deatails.MobileNumber);
                cmd.Parameters.AddWithValue("@Address", deatails.Address);
                cmd.Parameters.AddWithValue("@TotalAmount", deatails.TotalAmount);

                SqlParameter outputPara = new SqlParameter();
                outputPara.DbType = DbType.Int32;
                outputPara.Direction = ParameterDirection.Output;
                outputPara.ParameterName = "@Id";
                cmd.Parameters.Add(outputPara);
                cmd.ExecuteNonQuery();
                int id = int.Parse(outputPara.Value.ToString());
                if (deatails.Items.Count > 0)
                {
                   SaveBillItems(deatails.Items,con,id); 
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
        }
        void SaveBillItems(List<Items> items, SqlConnection con, int id )
        {
            try
            {
                string qry = "insert into tbl_BillItems(ProductName,Price,Quantity) value";

                foreach (Items item in items)
                {
                    qry += String.Format("('{0},{1},{2},{3}),",item.ProductName,item.Price,item.Quantity,id);

                }
                qry = qry.Remove(qry.Length-1);
                SqlCommand cmd=new SqlCommand(qry, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //////////////////

      

        void IData.SaveBillItems(List<Items> items, SqlConnection con, int id)
        {
            throw new NotImplementedException();
        }
    }
        
}
