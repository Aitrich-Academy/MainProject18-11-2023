namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UsersRegister")]
    public partial class UsersRegister
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsersRegister()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        public long? PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string District { get; set; }

        public long Pincode { get; set; }

        [Required]
        [StringLength(25)]
        public string PasswordHash { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
