using DAL.Manager;
using DAL.Models;
using MainProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MainProject.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        #region Product Insert
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("InsertProduct")]
        [HttpPost]
        public string InsertProduct(Ent_Product ent_Product) 
        {
            ProductManager productManager = new ProductManager();
            Ent_Product obj = ent_Product;
            Product table_product = new Product();
            table_product.Product_Name = obj.Product_Name;
            table_product.Price = obj.Price;
            table_product.Image = obj.Image;
            table_product.Category_id = obj.Category_id;
            return productManager.Product(table_product);
        }
        #endregion

        #region List all Product
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("ListProduct")]
        public List<Product> ListProducts()
        {
            ProductManager productManager = new ProductManager();
            List<Product> return_List = new List<Product>();
            List<Product> table_product = productManager.List();
            if (table_product.Count != 0)
            {
                foreach (var obj in table_product)
                {
                    return_List.Add(new Product
                    {
                        ProductID = obj.ProductID,
                        Product_Name = obj.Product_Name,
                        Price = obj.Price,
                        Image = obj.Image,
                        Category_id = obj.Category_id,
                        Status = obj.Status
                    });
                }
            }
            return return_List;
        }
        #endregion

        //[Route("upload")]
        //[HttpPost]
        //public IHttpActionResult UploadFile()
        //{
        //    try
        //    {
        //        var fileData = HttpContext.Current.Request.Files["fileData"];
        //        if (fileData != null && fileData.ContentLength > 0)
        //        {
        //            string savePath = HttpContext.Current.Server.MapPath(fileData.FileName);
        //            string saveImagePath = savePath + @"\" + fileData.FileName;
        //            fileData.SaveAs(saveImagePath);
        //        }
        //        return Ok("Success");
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

        #region Product Update
        [System.Web.Http.AcceptVerbs("PUT", "GET")]
        [System.Web.Http.HttpPut]
        [Route("UpdateProduct")]
        public IHttpActionResult UpdateProduct(int id, Product product)
        {
            ProductManager productManager = new ProductManager();
            string result = productManager.Update(id, product);
            if (result == "Success")
            {
                return Ok("Product update successfully");
            }
            else
            {
                return Ok("Error updating product");
            }
        }
        #endregion

        #region Product Delete
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("DeleteProduct")]
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            ProductManager productManager = new ProductManager();
            string result = productManager.Delete(id);
            if (result == "Success")
            {
                return Ok("Product Deleted Successfully");
            }
            else
            {
                return Ok("Error deleting product: " + result);
            }
        }
        #endregion
    }
}