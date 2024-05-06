using BL;
using Entity;
using LIBRARY;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;

namespace ExpenseManagementAPI.Controllers
{
    public class ExpenseController : ApiController
    {
        //POST API for save expense in database
        [Route("api/Expense/SaveExpense")]
        [HttpPost]
        public HttpResponseMessage SaveExpense(SaveExpenseEntity saveEntity)
        {
            SaveExpenseBL saveExpense = new SaveExpenseBL();
            ResponseMessage response = new ResponseMessage(); 
            try
            {
                //Here validation for Expense Amount, Expense Type and Expense Email.
                if (ModelState.IsValid)
                {
                    //if(saveEntity.ExpenseAmount != 0 && saveEntity.ExpenseAmount != null)
                    //{
                        response = saveExpense.SaveExpense(saveEntity);
                    //}
                    //else
                    //{
                    //    response.Message = "Enter your Expense Amount";
                    //}
                }
                else
                {




                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    response.Message = messages;
                }
            }
            catch (Exception ex)
            {
                response.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("ExpenseController=>SaveExpense=>Exception" + ex.Message + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //POST API for get expense of user if email is given otherwise get expense of all users from database
        [Route("api/Expense/GetExpense")]
        [HttpPost]
        public HttpResponseMessage GetExpense(GetExpenseEntity objEntity)
        {
            GetExpenseBL getExpense = new GetExpenseBL();
            SerializeResponse<SaveExpenseEntity> response = new SerializeResponse<SaveExpenseEntity>(); ;
            try
            {
                response = getExpense.GetExpense(objEntity);
            }
            catch (Exception ex)
            {
                response.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("ExpenseController=>GetExpense=>Exception" + ex.Message + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //POST API for get monthly expense of user 
        [Route("api/Expense/MonthlyExpense")]
        [HttpPost]
        public HttpResponseMessage MonthlyExpense(GetExpenseEntity objEntity)
        {
            MonthlyExpenseBL MonthlyExpense = new MonthlyExpenseBL();
            SerializeResponse<MonthlyExpenseEntity> response = new SerializeResponse<MonthlyExpenseEntity>(); ;
            try
            {
                response = MonthlyExpense.MonthlyExpense(objEntity);
            }
            catch (Exception ex)
            {
                response.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("ExpenseController=>MonthlyExpense=>Exception" + ex.Message + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
