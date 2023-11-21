using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProject.Models
{
    public class Ent_Product
    {
        public int ProductID { get; set; }
        public string Product_Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Category_id { get; set; }
        public string Status { get; set; }
    }
}