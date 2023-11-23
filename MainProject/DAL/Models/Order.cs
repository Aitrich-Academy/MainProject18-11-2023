namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int OrderID { get; set; }

        public int? User__id { get; set; }

        public int? Category_id { get; set; }

        public int? Product_id { get; set; }

        [Required]
        [StringLength(50)]
        public string Product_Name { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int Total_Price { get; set; }

        [Required]
        [StringLength(500)]
        public string Image { get; set; }

        public DateTime OrderDate { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

        public virtual Product Product { get; set; }

        public virtual UsersRegister UsersRegister { get; set; }
    }
}
