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
    [RoutePrefix("api/category")]
    public class CategoryController : ApiController
    {
        CategoryManager categoryManager = new CategoryManager();

        #region Category Insert
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("InsertCategory")]
        [HttpPost]
        public string InsertCategory(Ent_Category ent_Category)
        {
            Ent_Category obj = ent_Category;
            ProductCategory table_ProductCategorys = new ProductCategory();
            table_ProductCategorys.CategoryID = obj.CategoryID;
            table_ProductCategorys.Category_Name = obj.Category_Name;
            return categoryManager.ProductCategory(table_ProductCategorys);
        }
        #endregion

        #region List all category
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("ListCategory")]
        public List<Ent_Category> ListCategory()
        {
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
                var filePath = HttpContext.Current.Server.MapPath("~/CategoryImage/" + fileName);
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

        #region Category Update
        [System.Web.Http.AcceptVerbs("PUT", "GET")]
        [System.Web.Http.HttpPut]
        [Route("UpdateCategory")]
        public IHttpActionResult UpdateCategory(int id, ProductCategory category)
        {
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

        #region Category by Id
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        [Route("CategorybyId")]
        [HttpPost]
        public Ent_Category categoryByID(string id)
        {
            Ent_Category table_ProductCategorys = new Ent_Category();
            ProductCategory obj = categoryManager.CategorybyId(Convert.ToInt32(id));

            if (obj != null)
            {
                table_ProductCategorys.CategoryID = obj.CategoryID;
                table_ProductCategorys.Category_Name = obj.Category_Name;
                table_ProductCategorys.Status = obj.Status;
            }
            return table_ProductCategorys;
        }
        #endregion
    }
}