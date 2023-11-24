using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
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
            if (ord != null)
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
                // Order Successfully saved
                // Send email to admin
                SendEmailToAdmin("New Order Placed", "A new order has been placed. Please review.");

                return "Order Successfully Placed";
            }
            else
            {
                return "Error";
            }
        }

        private void SendEmailToAdmin(string subject, string body)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new System.Net.NetworkCredential("userindianplatter@gmail.com", "ukvnslehbqbxldmn"),
                    EnableSsl = true
                };

                MailMessage mail = new MailMessage
                {
                    From = new MailAddress("userindianplatter@gmail.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false
                };

                mail.To.Add("haiindianplatter@gmail.com"); // Replace with admin's email address

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, or perform any necessary actions
                Console.WriteLine("Error sending email to admin: " + ex.Message);
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
                result = "Order not found";
            }
            return result;
        }

        public Order OrderbyId(int Id)
        {
            Order return_Obj = new Order();
            return return_Obj = book.Orders.Where(e => e.OrderID == Id && e.Status != "Delete").SingleOrDefault();
        }

        public Order OrderbyDate(DateTime date)
        {
            Order return_Obj = new Order();
            return return_Obj = book.Orders.Where(e => e.OrderDate == date && e.Status != "Delete").SingleOrDefault();
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return book.Orders.Where(order => order.User__id == userId && order.Status != "Delete").ToList();
        }
        public UsersRegister GetUserByEmail(int id )
        {
            return book.UsersRegisters.FirstOrDefault(e => e.UserID==id && e.Status != "Delete");
        }

      
    }
}


