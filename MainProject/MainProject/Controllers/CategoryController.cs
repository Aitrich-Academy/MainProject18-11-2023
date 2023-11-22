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
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        #region Category Insert
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("InsertCategory")]
        [HttpPost]
        public string InsertCategory(Ent_Category ent_Category)
        {
            CategoryManager categoryManager = new CategoryManager();
            Ent_Category obj = ent_Category;
            ProductCategory table_ProductCategorys = new ProductCategory();
            table_ProductCategorys.Category_Name = obj.Category_Name;
            table_ProductCategorys.CategoryID = obj.CategoryID;
            return categoryManager.ProductCategory(table_ProductCategorys);
        }
        #endregion

        #region List all category
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("ListCategory")]
        public List<Ent_Category> ListCategory()
        {
            CategoryManager categoryManager = new CategoryManager();
            List<Ent_Category> return_List = new List<Ent_Category>();
            List<ProductCategory> table_ProductCategorys = categoryManager.List();
            if (table_ProductCategorys.Count != 0)
            {
                foreach (var obj in table_ProductCategorys)
                {
                    return_List.Add(new Ent_Category
                    {
                        CategoryID = obj.CategoryID,
                        Category_Name = obj.Category_Name,
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

        #region Category Update
        [System.Web.Http.AcceptVerbs("PUT", "GET")]
        [System.Web.Http.HttpPut]
        [Route("UpdateCategory")]
        public IHttpActionResult UpdateCategory(int id, ProductCategory category)
        {
            CategoryManager categoryManager = new CategoryManager();
            string result = categoryManager.Update(id, category);
            if (result == "Success")
            {
                return Ok("Category update successfully");
            }
            else
            {
                return Ok("Error updating Category");
            }
        }
        #endregion

        #region Category Delete
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("DeleteCategory")]
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            CategoryManager categoryManager = new CategoryManager();
            string result = categoryManager.Delete(id);
            if (result == "Success")
            {
                return Ok("Category Deleted Successfully");
            }
            else
            {
                return Ok("Error deleting Category: " + result);
            }
        }
        #endregion


    }
}