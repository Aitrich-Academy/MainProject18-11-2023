﻿using DAL.Manager;
using DAL.Models;
using MainProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace MainProject.Controllers
{
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        OrderManager orderManager = new OrderManager();

        #region Confirm Order
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("BookingDetail")]
        [HttpPost]
        public string BookingDetail(Ent_Order order)
        {
            Ent_Order order2 = order;
            Order order1 = new Order();
            order1.OrderID = order2.OrderID;
            order1.User__id = order2.Userid;
            order1.Category_id = order2.category_id;
            order1.Product_id = order2.ProductID;
            order1.Product_Name = order2.ProductName;
            int productprice = orderManager.GetPrice(order1);
            order1.Price = order2.Price;
            order1.Quantity = order2.Quantity;
            order1.Total_Price = order2.Quantity * productprice;
            order1.Image = order2.Image;
            order1.OrderDate = DateTime.Now;
            return orderManager.BookingDetail(order1);

        }
        #endregion

        #region View all Order
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("View")]
        public List<Ent_Order> ViewUser()
        {
            List<Ent_Order> return_List = new List<Ent_Order>();
            List<Order> table_user = orderManager.View();
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
                        Order_Date = DateTime.Now,
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
        [Route("CancelOrder")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
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

        #region Order by Id
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("OrderbyId")]
        [HttpPost]
        public Ent_Order orderByID(string id)
        {
            Ent_Order table_order = new Ent_Order();
            Order obj = orderManager.OrderbyId(Convert.ToInt32(id));

            if (obj != null)
            {
                table_order.OrderID = obj.OrderID;
                table_order.ProductID = (int)obj.Product_id;
                table_order.Userid = (int)obj.User__id;
                table_order.category_id = (int)obj.Category_id;
                table_order.ProductName = obj.Product_Name;
                table_order.Price = obj.Price;
                table_order.Quantity = obj.Quantity;
                table_order.Image = obj.Image;
                table_order.Order_Date = obj.OrderDate;
                table_order.Status = obj.Status;
                table_order.Total_Price = obj.Total_Price;
            }
            return table_order;
        }
        #endregion

        #region Order by Date
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("Orderbydate")]
        [HttpPost]
        public Ent_Order orderByDate(DateTime date)
        {
            Ent_Order table_order = new Ent_Order();
            Order obj = orderManager.OrderbyDate(date);

            if (obj != null)
            {
                table_order.OrderID = obj.OrderID;
                table_order.ProductID = (int)obj.Product_id;
                table_order.Userid = (int)obj.User__id;
                table_order.category_id = (int)obj.Category_id;
                table_order.ProductName = obj.Product_Name;
                table_order.Price = obj.Price;
                table_order.Quantity = obj.Quantity;
                table_order.Image = obj.Image;
                table_order.Order_Date = obj.OrderDate;
                table_order.Status = obj.Status;
                table_order.Total_Price = obj.Total_Price;
            }
            return table_order;
        }
        #endregion
        [HttpGet]
        [Route("GetUserOrderById")]
        public IHttpActionResult GetOrdersByEmail(int id)
        {
            UsersRegister user = orderManager.GetUserByEmail(id);

            if (user == null)
            {
                return NotFound();
            }

            List<Ent_Order> orderList = orderManager.GetOrdersByUserId(user.UserID)
                .Select(order => new Ent_Order
                {
                    OrderID = order.OrderID,
                    Userid = (int)order.User__id,
                    category_id = (int)order.Category_id,
                    ProductID = (int)order.Product_id,
                    ProductName = order.Product_Name,
                    Price = order.Price,
                    Quantity = order.Quantity,
                    Total_Price = order.Total_Price,
                    Image = order.Image,
                    Order_Date = order.OrderDate,
                    Status = order.Status
                })
                .ToList();

            Ent_UserRegistration userDetails = new Ent_UserRegistration
            {
                id = user.UserID,
                name = user.Name,
                email = user.Email,
                phonenumber = (long)user.PhoneNumber,
                district = user.District,
                pincode = user.Pincode,
                roll = user.Roll,
                profile_image = user.Profile_Image,
                passwordHash = user.PasswordHash,
                status = user.Status,
            };

            return Ok(new { userDetails, orderList });
        }




    }


}
