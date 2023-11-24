using DAL.Manager;
using DAL.Models;
using MainProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        ProductManager productManager = new ProductManager();

        #region Product Insert
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("InsertProduct")]
        [HttpPost]
        public string InsertProduct(Ent_Product ent_Product) 
        {
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
        public List<Ent_Product> ListProducts()
        {
            List<Ent_Product> return_List = new List<Ent_Product>();
            List<Product> table_product = productManager.List();
            if (table_product.Count != 0)
            {
                foreach (var obj in table_product)
                {
                    return_List.Add(new Ent_Product
                    {
                        ProductID = obj.ProductID,
                        Product_Name = obj.Product_Name,
                        Price = obj.Price,
                        Image = obj.Image,
                        Category_id = (int)obj.Category_id,
                        Status = obj.Status
                    });
                }
            }
            return return_List;
        }
        #endregion

        #region File Upload
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("FileUpload")]
        [HttpPost]
        public IHttpActionResult UploadFile()
        {
            try
            {
                string[] AllowedFileExt = new string[] { ".jpg", ".png" };
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest.Files.Count == 0)
                {
                    return BadRequest("No file to upload");
                }

                var postedFile = httpRequest.Files[0];
                if (!AllowedFileExt.Contains(postedFile.FileName.Substring(postedFile.FileName.LastIndexOf("."))))
                {
                    return BadRequest("Invalid File Format");
                }

                var fileName = Path.GetFileName(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/ProductImages/" + fileName);
                if (File.Exists(filePath))
                {
                    return BadRequest("A file with the same name already exists.");
                }
                postedFile.SaveAs(filePath);

                return Ok("File Uploaded Successfully");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        #endregion

        #region Product Update
        [System.Web.Http.AcceptVerbs("PUT", "GET")]
        [System.Web.Http.HttpPut]
        [Route("UpdateProduct")]
        public IHttpActionResult UpdateProduct(int id, Product product)
        {
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

        #region Product by Id
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("ProductbyId")]
        [HttpPost]
        public Ent_Product userDetailsByID(string id)
        {
            Ent_Product table_product = new Ent_Product();
            Product obj = productManager.ProductbyId(Convert.ToInt32(id));

            if (obj != null)
            {
                table_product.ProductID = Convert.ToInt32(obj.ProductID);
                table_product.Product_Name = obj.Product_Name;
                table_product.Price = obj.Price;
                table_product.Image = obj.Image;
                table_product.Category_id = (int)obj.Category_id;
                table_product.Status = obj.Status;
            }
            return table_product;
        }
        #endregion

        #region Filter Category Product
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("filtercategoryproduct")]
        [HttpPost]
        public List<Ent_Product> FilterByCategoryProduct(string name)
        {
            List<Ent_Product> productList = new List<Ent_Product>();
            List<ProductCategory> categories = productManager.FilterProduct(name);
            foreach (var category in categories)
            {
                List<Product> products = productManager.GetProductsByCategoryId(category.CategoryID);
                foreach (var product in products)
                {
                    Ent_Product table_product = new Ent_Product
                    {
                        ProductID = product.ProductID,
                        Product_Name = product.Product_Name,
                        Price = product.Price,
                        Image = product.Image,
                        Category_id = (int)product.Category_id,
                        Status = product.Status,
                    };
                    productList.Add(table_product);
                }
            }
            return productList;
        }
        #endregion
        
        #region Search by Product Name
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("searchbyproduct")]
        [HttpPost]
        public Ent_Product searchbyproduct(string name)
        {
            Ent_Product table_product = new Ent_Product();
            Product obj = productManager.Searchproduct(name);
            if (obj != null)
            {
                table_product.ProductID = obj.ProductID;
                table_product.Product_Name = obj.Product_Name;
                table_product.Price = obj.Price;
                table_product.Image = obj.Image;
                table_product.Category_id = (int)obj.Category_id;
                table_product.Status = obj.Status;
            }
            return table_product;
        }
        #endregion
    }
}