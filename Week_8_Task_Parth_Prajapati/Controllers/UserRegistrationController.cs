using BL;
using Entity;
using LIBRARY;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExpenseManagementAPI.Controllers
{
    
    public class UserRegistrationController : ApiController
    {
        //POST API for user or admin login
        [Route("api/UserRegistration/Login")]
        [HttpPost]
        public HttpResponseMessage Login(LoginEntity loginEntity)
        {
            LoginBL login = new LoginBL();
            ResponseMessage response = new ResponseMessage();
            try
            {
                //ger 200 if login successfull
                response = login.Login(loginEntity);
            }
            catch (Exception ex)
            {
                response.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("UserRegistrationController=>Login=>Exception" + ex.Message + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //this API for User registration and get data of user
        [Route("api/UserRegistration/UserRegistration")]
        [HttpPost]
        public HttpResponseMessage UserRegistration(UserEntity userEntity)
        {
            
            UserRegistrationBL userRegistration = new UserRegistrationBL();
            SerializeResponse<UserEntity> response = new SerializeResponse<UserEntity>(); ;
            try
            {
                //Here validation for User Phone number.
                if (ModelState.IsValid)
                {
                    response = userRegistration.UserRegistrationInfo(userEntity);
                }
                else
                {
                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                    response.Message = messages;
                    //response.Message = "Please enter email.";
                }
            }
            catch (Exception ex)
            {
                response.Message = "500|Exception Occurred";
                InsertLog.WriteErrrorLog("UserRegistrationController=>UserRegistration=>Exception" + ex.Message + ex.StackTrace);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
