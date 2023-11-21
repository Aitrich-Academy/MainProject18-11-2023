using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DAL.Model;

namespace DAL.Manager
{
 
    public class UserManager
    {
        Model1 User_db = new Model1();

        public string UserRegister(UsersRegister rej)
        {
            int result = 0;
            if (rej != null) 
            {
                UsersRegister objct=new UsersRegister();

                objct.Name = rej.Name;
                objct.Email = rej.Email;
            
                objct.PhoneNumber = rej.PhoneNumber;
                objct.District = rej.District;
                objct.Pincode = rej.Pincode;
                objct.PasswordHash = rej.PasswordHash;
                objct.Status = "A";
                User_db.UsersRegisters.Add(objct);

                result = User_db.SaveChanges();

            }
            if(result>0)
            {
                return "Successfully Registered";

            }
            else
            {
                return "Error";
            }
        }

    }
}
