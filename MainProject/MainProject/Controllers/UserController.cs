using DAL.Manager;
using DAL.Models;
using MainProject.Models;
using MainProject.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace MainProject.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        UserManager mng = new UserManager();

        #region User Register
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("UserRegister")] 
        [HttpPost]
        public string UserRegistration(Ent_UserRegistration user)
        {                  
            Ent_UserRegistration ent = user;
            UsersRegister rej = new UsersRegister();
            rej.Name = ent.name; 
            rej.Email = ent.email;
            rej.PhoneNumber = ent.phonenumber;
            rej.District = ent.district;
            rej.Pincode = ent.pincode;
            rej.Profile_Image = ent.profile_image;
            rej.PasswordHash = ent.passwordHash;
            return mng.UserRegister(rej);
        }
        #endregion

        #region Login
        [Route("Login")]
        [HttpPost]
        public HttpResponseMessage Login(Ent_UserRegistration user)
        {
            if (user != null && ModelState.IsValid)
            {
                Ent_UserRegistration ent = user;
                UsersRegister usr = new UsersRegister();

                usr.Email = ent.email;
                usr.PasswordHash = ent.passwordHash;

                UsersRegister result = mng.UserLogin(usr);

                if (result != null)
                {
                    String token = TokenManager.GenerateToken(result);
                    LoginResponseDTO loginResponseDTO = new LoginResponseDTO();
                    loginResponseDTO.Token = token;
                    loginResponseDTO.email = result.Email;
                    loginResponseDTO.user_id = result.UserID;
                    loginResponseDTO.district = result.District;
                    loginResponseDTO.phone = (long)result.PhoneNumber;
                    loginResponseDTO.role = result.Roll;
                    loginResponseDTO.name = result.Name;

                    ResponseDataDTO response = new ResponseDataDTO(true, "Success", loginResponseDTO);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                    //return Request.CreateErrorResponse(HttpStatusCode.OK, result);

                    //return Request.CreateErrorResponse(HttpStatusCode.OK, "Success");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid User name and password !");
                }
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    Errors = UtilsConfig.GetErrorListFromModelState(ModelState)
                });
            }
        }
        #endregion

        #region View all Users
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("ViewUser")]
        public List<Ent_UserRegistration> ViewUser()
        {
           
            List<Ent_UserRegistration> return_List = new List<Ent_UserRegistration>();
            List<UsersRegister> table_user = mng.View();
            if (table_user.Count != 0)
            {
                foreach (var obj in table_user)
                {
                    return_List.Add(new Ent_UserRegistration
                    {
                        id = obj.UserID,
                        name = obj.Name,
                        email = obj.Email,
                        phonenumber = (long)obj.PhoneNumber,
                        district = obj.District,
                        pincode = obj.Pincode,
                        roll = obj.Roll,
                        profile_image = obj.Profile_Image,
                        status = obj.Status
                    });
                }
            }
            return return_List;
        }
        #endregion

        #region User Update
        [System.Web.Http.AcceptVerbs("PUT", "GET")]
        [System.Web.Http.HttpPut]
        [Route("UpdateUser")]
        public IHttpActionResult UpdateUser(int id, UsersRegister register)
        {
            string result = mng.Update(id, register);
            if (result == "Success")
            {
                return Ok("User update successfully");
            }
            else
            {
                return Ok("Error updating user");
            }
        }
        #endregion

        #region User Delete
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("DeleteUser")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            
            string result = mng.Delete(id);
            if (result == "Success")
            {
                return Ok("user Deleted Successfully");
            }
            else
            {
                return Ok("Error deleting user: " + result);
            }
        }
        #endregion
    }
}