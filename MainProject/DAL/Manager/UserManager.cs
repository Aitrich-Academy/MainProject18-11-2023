using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

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
                UsersRegister objct = new UsersRegister();

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
            if (result > 0)
            {
                return "Successfully Registered";
            }
            else
            {
                return "Error";
            }
        }
        public UsersRegister UserLogin(UsersRegister usr)
        {
            var log =User_db.UsersRegisters.Where(e=>e.Email.Equals(usr.Email)&&e.PasswordHash.Equals(usr.PasswordHash)).FirstOrDefault();
            return log;
        }

        public List<UsersRegister> View()
        {
            return User_db.UsersRegisters.ToList();
        }

        public string Update(int id, UsersRegister register)
        {
            string result = "";
            UsersRegister update = User_db.UsersRegisters.Where(e => e.UserID == id && e.Status != "Delete").FirstOrDefault();
            if (update != null)
            {
                update.Name = register.Name;
                update.Email = register.Email;
                update.PhoneNumber = register.PhoneNumber;
                update.District = register.District;
                update.Pincode = register.Pincode;
                update.PasswordHash = register.PasswordHash;
                update.Status = "Active";

                User_db.Entry(update).State = EntityState.Modified;
                result = User_db.SaveChanges().ToString();
                result = "Success";
            }
            else
            {
                result = "Error";
            }
            return result;
        }

        public string Delete(int id)
        {
            string result = "";
            UsersRegister delete = User_db.UsersRegisters.FirstOrDefault(e => e.UserID == id && e.Status != "Delete");
            if (delete != null)
            {
                delete.Status = "Delete";
                User_db.SaveChanges();
                result = "Success";
            }
            else
            {
                result = "Product not found";
            }
            return result;
        }     
    }
}
