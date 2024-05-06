using Entity;
using LIBRARY;
using System.Data.SqlClient;
using System.Data;
using System;

namespace BL
{
    public class GetExpenseBL
    {
        //this method is give transaction  of user's expense
        public SerializeResponse<SaveExpenseEntity> GetExpense(GetExpenseEntity objEntity)
        {
            InsertLog.WriteErrrorLog("GetExpenseBL=>GetExpense=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<SaveExpenseEntity> objResponsemessage = new SerializeResponse<SaveExpenseEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "Sp_GetExpense";
            try
            {
                //Make Connection With Database
                string Con_str = Connection.constrEMS;

                SqlParameter prm1 = objSDP.CreateInitializedParameter("@ExpenseEmail", DbType.String, objEntity.ExpenseEmail);
                
                SqlParameter[] Sqlpara = { prm1};
                //Executing SP and get respose 
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<SaveExpenseEntity>(ds.Tables[0]);
                    objResponsemessage.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Code"]);
                    objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                }
            }
            catch (Exception ex)
            {
                objResponsemessage.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("GetExpenseBL=>Exception" + ex.Message + ex.StackTrace);
            }
            return objResponsemessage;
        }
    }
}
