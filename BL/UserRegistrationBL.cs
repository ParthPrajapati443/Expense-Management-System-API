using Entity;
using LIBRARY;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BL
{
    public class UserRegistrationBL
    {
        //this method is User registration and get data of user
        public SerializeResponse<UserEntity> UserRegistrationInfo(UserEntity objEntity)
        {
            InsertLog.WriteErrrorLog("UserRegistrationBL=>UserRegistrationInfo=>Started");
            ConvertDataTable bl = new ConvertDataTable();
            SerializeResponse<UserEntity> objResponsemessage = new SerializeResponse<UserEntity>();

            DataSet ds = new DataSet();
            SqlDataProvider objSDP = new SqlDataProvider();
            string query = "Sp_UserRegistration";
            try
            {
                //Make Connection With Database
                string Con_str = Connection.constrEMS;                          

                SqlParameter prm1 = objSDP.CreateInitializedParameter("@Flag", DbType.String, objEntity.Flag);
                SqlParameter prm2 = objSDP.CreateInitializedParameter("@UserName", DbType.String, objEntity.UserName);
                SqlParameter prm3 = objSDP.CreateInitializedParameter("@UserEmail", DbType.String, objEntity.UserEmail);
                SqlParameter prm4 = objSDP.CreateInitializedParameter("@UserPhone", DbType.String, objEntity.UserPhone);
                SqlParameter prm5 = objSDP.CreateInitializedParameter("@UserPhone", DbType.String, objEntity.UserAddress);
                SqlParameter prm6 = objSDP.CreateInitializedParameter("@UserPassword", DbType.String, objEntity.UserPassword);
                SqlParameter[] Sqlpara = { prm1, prm2, prm3, prm4, prm5, prm6};
                //Executing SP and get respose 
                ds = SqlHelper.ExecuteDataset(Con_str, query, Sqlpara);
                if (objEntity.Flag == "INSERT" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    objResponsemessage.ID = Convert.ToInt32(ds.Tables[0].Rows[0]["Code"]);
                    objResponsemessage.Message = Convert.ToString(ds.Tables[0].Rows[0]["Message"]);
                }
                else if (objEntity.Flag == "GET" && ds?.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    objResponsemessage.ArrayOfResponse = bl.ListConvertDataTable<UserEntity>(ds.Tables[0]); 
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
