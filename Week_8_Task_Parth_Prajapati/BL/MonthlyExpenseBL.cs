using Entity;
using LIBRARY;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class MonthlyExpenseBL
    {
        //this method is give monthly expense report if email is given
        public SerializeResponse<MonthlyExpenseEntity> MonthlyExpense(GetExpenseEntity objEntity)
        {
            InsertLog.WriteErrrorLog("UserRegistrationBL=>UserRegistrationInfo=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<MonthlyExpenseEntity> objResponsemessage = new SerializeResponse<MonthlyExpenseEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "Sp_MonthlyExpense";
            try
            {
                //Make Connection With Database
                string Con_str = Connection.constrEMS;

                SqlParameter prm1 = objSDP.CreateInitializedParameter("@ExpenseEmail", DbType.String, objEntity.ExpenseEmail);

                SqlParameter[] Sqlpara = { prm1 };
                //Executing SP and get respose 
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<MonthlyExpenseEntity>(ds.Tables[0]);
                    objResponsemessage.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Code"]);
                    objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                }
            }
            catch (Exception ex)
            {
                objResponsemessage.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("UserRegistrationBL=>Exception" + ex.Message + ex.StackTrace);
            }
            return objResponsemessage;
        }
    }
}
