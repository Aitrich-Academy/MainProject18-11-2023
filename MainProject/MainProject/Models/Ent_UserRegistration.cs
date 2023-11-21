using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProject.Models
{
    public class Ent_UserRegistration
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; } 
        public string district {  get; set; }
        public long phonenumber {get; set; }
        public string passwordHash {  get; set; }
        public long pincode {  get; set; }
        public string status { get; set; }
    }
}