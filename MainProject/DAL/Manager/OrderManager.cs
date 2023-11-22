using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DAL.Models;

namespace DAL.Manager
{    
    public class OrderManager
    {
        Model1 book = new Model1();
        
        public string BookingDetail(Order ord)
        {
            int result = 0;            
            if(ord != null) 
            {
                Order order = new Order();
                order.OrderID = ord.OrderID;
                order.User__id = ord.User__id;
                order.Category_id = ord.Category_id;
                order.Product_id = ord.Product_id;
                order.Product_Name = ord.Product_Name;
                order.Price = ord.Price;
                order.Quantity = ord.Quantity;
                order.Total_Price = ord.Total_Price;
                order.Image = ord.Image;
                order.OrderDate = DateTime.Now;
                order.Status = "Active";
                book.Orders.Add(order);

                result = book.SaveChanges();
            }
        
            if (result > 0)
            {
                return "Order Successfully";
            }
            else
            {
                return "Error";
            }
        }
        public int GetPrice(Order ord) 
        {
            var product = book.Products.Find(ord.Product_id);
            if (product != null) 
            {
                return (int)product.Price;
            }
            else
            {
                return 0;
            }
        }
      
        public List<Order> View()
        {
            return book.Orders.ToList();
        }

        public string Delete(int id)
        {
            string result = "";
            Order delete = book.Orders.FirstOrDefault(e => e.OrderID == id && e.Status != "Delete");
            if (delete != null)
            {
                delete.Status = "Delete";
                book.SaveChanges();
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
