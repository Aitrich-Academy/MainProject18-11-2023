using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Manager
{
    public class CategoryManager
    {
        Model1 model = new Model1();

        public string ProductCategory(ProductCategory category)
        {
            int result = 0;
            if (category != null)
            {
                ProductCategory table_ProductCategorys = new ProductCategory();
                table_ProductCategorys.Category_Name = category.Category_Name;
                table_ProductCategorys.CategoryID = category.CategoryID;
                table_ProductCategorys.Status = "Active";
                model.ProductCategorys.Add(table_ProductCategorys);
                result = model.SaveChanges();
            }
            if (result > 0)
            {
                return "Category Inserted Successfully....";
            }
            else
            {
                return "Error";
            }
        }
        public List<ProductCategory> List()
        {
            return model.ProductCategorys.Where(e => e.Status != "Delete").ToList();
        }
        public string Update(int id, ProductCategory category)
        {
            string result = "";
            ProductCategory update = model.ProductCategorys.Where(e => e.CategoryID == id && e.Status != "D").FirstOrDefault();
            if (update != null)
            {
                update.Category_Name = category.Category_Name;
                update.CategoryID = category.CategoryID;
                update.Status = "Active";

                model.Entry(update).State = EntityState.Modified;
                result = model.SaveChanges().ToString();
                result = "Success";
            }
            else
            {
                result = "Error";
            }
            return result;
        }

        public string Delete(int id)
        {
            string result = "";
            ProductCategory delete = model.ProductCategorys.FirstOrDefault(e => e.CategoryID == id && e.Status != "Delete");
            if (delete != null)
            {
                delete.Status = "Delete";
                model.SaveChanges();
                result = "Success";
            }
            else
            {
                result = "Category not found";
            }
            return result;
        }

        public ProductCategory CategorybyId(int Id)
        {
            ProductCategory return_Obj = new ProductCategory();
            return return_Obj = model.ProductCategorys.Where(e => e.CategoryID == Id && e.Status != "D").SingleOrDefault();
        }
    }
}