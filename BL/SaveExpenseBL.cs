using Entity;
using LIBRARY;
using System;
using System.Data;
using System.Data.SqlClient;


namespace BL
{
    public class SaveExpenseBL
    {
        //this method is save expense of user in database
        public ResponseMessage SaveExpense(SaveExpenseEntity objEntity)
        {
            InsertLog.WriteErrrorLog("SaveExpenseBL=>SaveExpense=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            ResponseMessage responseMessage = new ResponseMessage();
            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "Sp_SaveExpense";
            try
            {
                //Make Connection With Database
                string Con_str = Connection.constrEMS;

                SqlParameter prm1 = objSDP.CreateInitializedParameter("@ExpenseEmail", DbType.String, objEntity.ExpenseEmail);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@ExpenseType", DbType.String, objEntity.ExpenseType);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@ExpenseAmount", DbType.String, objEntity.ExpenseAmount);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@ExpenseReason", DbType.String, objEntity.ExpenseReason);
                SqlParameter prm5 = objSDP.CreateInitializedParameter("@ExpenseDate", DbType.String, objEntity.ExpenseDate);

                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5 };
                //Executing SP and get respose 
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    responseMessage.code = Convert.ToInt32(ds.Tables[0].Rows[0]["Code"]);
                    responseMessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                    
                }
            }
            catch (Exception ex)
            {
                responseMessage.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("SaveExpenseBL=>Exception" + ex.Message + ex.StackTrace);
            }
            return responseMessage;
        }
    }
}
