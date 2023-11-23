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
    public class ProductManager
    {
        Model1 model = new Model1();

        public string Product(Product product)
        {
            int result = 0;
            if (product != null)
            {
                Product table_product = new Product();
                table_product.Product_Name = product.Product_Name;
                table_product.Price = product.Price;
                table_product.Image = product.Image;
                table_product.Category_id = product.Category_id;
                table_product.Status = "Active"; 
                model.Products.Add(table_product);
                result = model.SaveChanges();
            }

            if (result > 0)
            {
                return "Product Inserted Successfully....";
            }
            else
            {
                return "Error";
            }
        }

        public List<Product> List()
        {
            return model.Products.Where(e => e.Status != "Delete").ToList();
        }

        public string Update(int id, Product product)
        {
            string result = "";
            Product update = model.Products.Where(e => e.ProductID == id && e.Status != "D").FirstOrDefault();
            if (update != null)
            {
                update.Product_Name = product.Product_Name;
                update.Price = product.Price;
                update.Image = product.Image;
                update.Category_id = product.Category_id;
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
            Product delete = model.Products.FirstOrDefault(e => e.ProductID == id && e.Status != "Delete");
            if (delete != null)
            {
                delete.Status = "Delete";
                model.SaveChanges();
                result = "Success";
            }
            else
            {
                result = "Product not found";
            }
            return result;
        }

        public Product ProductbyId(int Id)
        {
            Product return_Obj = new Product();
            return return_Obj = model.Products.Where(e => e.ProductID == Id && e.Status != "D").SingleOrDefault();
        }

        public List<ProductCategory> FilterProduct(string name)
        {
            return model.ProductCategorys.Where(e => e.Category_Name == name && e.Status != "Delete").ToList();
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            return model.Products.Where(p => p.Category_id == categoryId && p.Status != "Delete").ToList();
        }

        public Product Searchproduct(string name)
        {
            Product filterProduct = new Product();
            return filterProduct = model.Products.Where(e => e.Product_Name == name && e.Status != "Delete").SingleOrDefault();
        }
    }
}
