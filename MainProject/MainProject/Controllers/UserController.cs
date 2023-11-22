using DAL.Manager;
using DAL.Models;
using MainProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MainProject.Controllers
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        // GET api/<controller>
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("UserRegister")]   // Url creation Route
        [HttpPost]
        public string UserRegistration(Ent_UserRegistration user)
        {
            UserManager mng = new UserManager();
          
            Ent_UserRegistration ent = user;
            UsersRegister rej = new UsersRegister();
            rej.Name=ent.name; 
            rej.Email=ent.email;
            rej.PhoneNumber = ent.phonenumber;
            rej.District=ent.district;
            rej.Pincode=ent.pincode;
            rej.PasswordHash=ent.passwordHash;
            return mng.UserRegister(rej);
        }


        #region View all Users
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("ViewUser")]
        public List<Ent_UserRegistration> ViewUser()
        {
            UserManager userManager = new UserManager();
            List<Ent_UserRegistration> return_List = new List<Ent_UserRegistration>();
            List<UsersRegister> table_user = userManager.View();
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
            UserManager userManager = new UserManager();
            string result = userManager.Update(id, register);
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
            UserManager userManager = new UserManager();
            string result = userManager.Delete(id);
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