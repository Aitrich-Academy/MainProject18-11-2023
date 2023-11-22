using DAL.Manager;
using DAL.Models;
using MainProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;

namespace MainProject.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        #region Confirm Order
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("BookingDetail")]   // Url creation Route
        [HttpPost]
        public string BookingDetail(Ent_Order order)
        {
            OrderManager manager = new OrderManager();            
            Ent_Order order2 = order;
            Order order1 = new Order();
            order1.OrderID = order2.OrderID;
            order1.User__id = order2.Userid;
            order1.Category_id = order2.category_id;
            order1.Product_id = order2.ProductID;
            order1.Product_Name = order2.ProductName;
            int productprice=manager.GetPrice(order1);
            order1.Price = order2.Price;
            order1.Quantity = order2.Quantity;
            order1.Total_Price = order2.Quantity* productprice;
            order1.Image = order2.Image;
            order1.OrderDate = DateTime.Now;
            return manager.BookingDetail(order1);
        }
        #endregion

        #region View all Order
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("View")]
        public List<Ent_Order> ViewUser()
        {
            OrderManager manager= new OrderManager();
            List<Ent_Order> return_List = new List<Ent_Order>();
            List<Order> table_user = manager.View();
            if (table_user.Count != 0)
            {
                foreach (var obj in table_user)
                {
                    return_List.Add(new Ent_Order
                    {
                        OrderID = obj.OrderID,
                        Userid = (int)obj.User__id,
                        category_id = (int)obj.Category_id,
                        ProductID = (int)obj.Product_id,
                        ProductName = obj.Product_Name,
                        Price = obj.Price,
                        Quantity = obj.Quantity,
                        Total_Price = obj.Total_Price,
                        Image = obj.Image,
                        OrderDate = DateTime.Now,
                        Status = obj.Status                 
                    });
                }
            }
            return return_List;
        }
        #endregion

        #region Cancel Order
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("DeleteOrder")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            OrderManager orderManager = new OrderManager();
            string result = orderManager.Delete(id);
            if (result == "Success")
            {
                return Ok("Order Canceled Successfully");
            }
            else
            {
                return Ok("Error Cancel product: " + result);
            }
        }
        #endregion
    }
}