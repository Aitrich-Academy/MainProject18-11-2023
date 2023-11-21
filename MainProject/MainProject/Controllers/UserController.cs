using DAL.Manager;
using DAL.Model;
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

    }
}