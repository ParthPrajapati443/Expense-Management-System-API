using Entity;
using LIBRARY;
using System.Data.SqlClient;
using System.Data;
using System;

namespace BL
{
    public class LoginBL
    {
        //this method is used for login of user or admin
        public ResponseMessage Login(LoginEntity objEntity)
        {
            InsertLog.WriteErrrorLog("LoginBL=>Login=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            ResponseMessage responseMessage = new ResponseMessage();
            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "Sp_Login";
            try
            {
                //Make Connection With Database
                string Con_str = Connection.constrEMS;

                SqlParameter prm1 = objSDP.CreateInitializedParameter("@EmailId", DbType.String, objEntity.EmailId);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@Password", DbType.String, objEntity.Password);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@Role", DbType.String, objEntity.Role);
                

                SqlParameter[] Sqlpara = { prm1, prm2, prm3 };
                //Executing SP and get respose 
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                if (ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    responseMessage.code = Convert.ToInt32(ds.Tables[0].Rows[0]["Code"]);
                    responseMessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                    responseMessage.Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                }
            }
            catch (Exception ex)
            {
                responseMessage.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("LoginBL=>Exception" + ex.Message + ex.StackTrace);
            }
            return responseMessage;
        }
    }
}
