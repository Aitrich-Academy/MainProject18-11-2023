using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProject.Models
{
    public class Ent_Order
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Userid { get; set; }
        public int category_id {  get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }    
        public string Image { get; set; }
        public DateTime Order_Date { get; set; }
        public string Status { get; set; }
        public int Total_Price { get; set; }
    }
}